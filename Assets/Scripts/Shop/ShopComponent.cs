using InventoryGame.Items;
using InventoryGame.Sets;
using InventoryGame.UI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace InventoryGame.Shop
{
    public class ShopComponent : MonoBehaviour
    {
        [SerializeField] private ItemView itemViewPrefab;
        [SerializeField] private ItemsSet itemsSet;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private GameObject purchaseConfirmationPopup;
        [SerializeField] private ItemView selectedItemView;
        [SerializeField] private Slider itemsAmountSlider;

        private ItemInfo _selectedItem;

        private void Start()
        {
            SpawnItems();
        }

        private void OnEnable()
        {
            purchaseConfirmationPopup.SetActive(false);
            itemsParent.gameObject.SetActive(true);
        }

        public void SetItemsLimit(int amount)
        {
            itemsAmountSlider.maxValue = amount;
            itemsAmountSlider.value = 1;
            itemsAmountSlider.onValueChanged.Invoke(itemsAmountSlider.value);
        }

        [UsedImplicitly]
        public void ConfirmPurchase()
        {
            Assert.IsNotNull(_selectedItem);
            // TODO: initiate purchase here
            Debug.Log($"Purchase of x{itemsAmountSlider.value} {_selectedItem.ItemName} item confirmed");
        }

        private void SpawnItems()
        {
            foreach (var item in itemsSet.List)
            {
                var itemView = Instantiate(itemViewPrefab, itemsParent);
                itemView.SetItem(item);
                itemView.OnItemSelected += OnItemSelected;
            }
        }

        private void OnItemSelected(ItemInfo item)
        {
            _selectedItem = item;
            selectedItemView.SetItem(item);
            purchaseConfirmationPopup.SetActive(true);
        }
    }
}
