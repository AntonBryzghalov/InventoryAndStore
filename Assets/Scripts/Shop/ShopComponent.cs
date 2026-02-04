using System.Collections.Generic;
using InventoryGame.Events;
using InventoryGame.Inventory;
using InventoryGame.Items;
using InventoryGame.Sets;
using InventoryGame.UI;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace InventoryGame.Shop
{
    public class ShopComponent : MonoBehaviour
    {
        [Header("Inventory UI References")]
        [SerializeField] private ItemView itemViewPrefab;
        [SerializeField] private Transform itemsParent;

        [Header("Purchase Confirmation Popup")]
        // TODO: make it a separate entity
        [SerializeField] private GameObject purchaseConfirmationPopup;
        [SerializeField] private ItemView selectedItemView;
        [SerializeField] private int defaultItemsLimit = 10;
        [SerializeField] private Slider itemsAmountSlider;

        [Header("Scriptable References")]
        [SerializeField] private ItemsSet itemsSet;
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private InventorySO playerInventory;
        [SerializeField] private InventoryItemEvent itemPurchasedEvent;

        private ItemInfo _selectedItem;
        private int _itemsLimit;
        private List<ItemView> _spawnedItemViews = new ();

        public ItemsSet ItemsSet => itemsSet;

        private void Start()
        {
            SetItemsLimit(defaultItemsLimit);
            SpawnItems();
        }

        private void OnDestroy()
        {
            foreach (var itemView in _spawnedItemViews)
            {
                if (itemView != null) itemView.OnItemSelected -= OnItemSelected;
            }
        }

        private void OnEnable()
        {
            purchaseConfirmationPopup.SetActive(false);
            itemsParent.gameObject.SetActive(true);
        }

        public void SetItemsLimit(int amount)
        {
            _itemsLimit = amount;
        }

        [UsedImplicitly]
        public void ConfirmPurchase()
        {
            Assert.IsNotNull(_selectedItem);
            Debug.Log($"Purchase of x{itemsAmountSlider.value} {_selectedItem.ItemName} item confirmed");
            
            var itemQuantity = Mathf.RoundToInt(itemsAmountSlider.value);
            var totalPrice = _selectedItem.BasePrice * itemQuantity;
            Assert.IsTrue(playerWallet.GoldAmount >= totalPrice, "Player has no enough gold");

            playerWallet.GoldAmount -= totalPrice;

            var purchasedInventoryItem = new InventoryItem(_selectedItem, itemQuantity);
            playerInventory.AddItem(purchasedInventoryItem);

            purchaseConfirmationPopup.SetActive(false);
            itemPurchasedEvent.Invoke(purchasedInventoryItem);
        }

        private void SpawnItems()
        {
            foreach (var item in itemsSet.List)
            {
                var itemView = Instantiate(itemViewPrefab, itemsParent);
                itemView.Bind(item);
                itemView.OnItemSelected += OnItemSelected;
                _spawnedItemViews.Add(itemView);
            }
        }

        private void OnItemSelected(ItemInfo item)
        {
            _selectedItem = item;
            selectedItemView.Bind(item);
            purchaseConfirmationPopup.SetActive(true);
            UpdateSlider(Mathf.Min(_itemsLimit, playerWallet.GoldAmount / item.BasePrice));
        }

        private void UpdateSlider(int maxItems)
        {
            if (maxItems < 2)
            {
                itemsAmountSlider.gameObject.SetActive(false);
            }
            else
            {
                itemsAmountSlider.maxValue = maxItems;
                itemsAmountSlider.gameObject.SetActive(true);
            }

            itemsAmountSlider.value = 1;
        }
    }
}
