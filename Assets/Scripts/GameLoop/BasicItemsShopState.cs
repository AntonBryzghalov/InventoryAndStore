using System.Collections.Generic;
using System.Linq;
using InventoryGame.Events;
using InventoryGame.Inventory;
using InventoryGame.Items;
using InventoryGame.Shop;
using TMPro;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class BasicItemsShopState : GameLoopState
    {
        [SerializeField] private GameLoopFiniteStateMachine fsm;
        [SerializeField] private GameLoopStateId nextState;

        [SerializeField] private TMP_Text itemsLeftToBuyText;

        [SerializeField] private GameLoopConfig config;
        [SerializeField] private ShopComponent basicItemsShop;
        [SerializeField] private InventorySO playerInventory;
        [SerializeField] private InventorySO aiInventory;
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private Wallet aiWallet;
        [SerializeField] private InventoryItemEvent itemPurchasedEvent;
        
        private IReadOnlyList<ItemInfo> ShopItems => basicItemsShop.ItemsSet.List;

        public override void OnEnter()
        {
            base.OnEnter();
            playerWallet.GoldAmount += config.GoldGivenEachCycle;
            aiWallet.GoldAmount += config.GoldGivenEachCycle;
            UpdateItemsLeftToBuyText(0);
            basicItemsShop.SetItemsLimit(config.RoundsPerGame);

            itemPurchasedEvent.AddListener(OnItemPurchased);
        }

        public override void OnExit()
        {
            itemPurchasedEvent.RemoveListener(OnItemPurchased);

            base.OnExit();
            BuyBasicItemsForAI();
        }

        private void BuyBasicItemsForAI()
        {
            var itemsAmount = config.RoundsPerGame;
            for (int i = 0; i < itemsAmount; i++)
            {
                var itemType = ShopItems[Random.Range(0, ShopItems.Count)];
                aiWallet.GoldAmount -= itemType.BasePrice;
                aiInventory.AddItem(new InventoryItem(itemType));
            }
        }

        private void OnItemPurchased(InventoryItem _)
        {
            var basicItemsBought = playerInventory.Items
                .Where(item => ShopItems.Contains(item.ItemInfo))
                .Sum(item => item.Quantity);

            var itemsLeftToBuy = config.RoundsPerGame - basicItemsBought;
            if (itemsLeftToBuy <= 0)
            {
                fsm.SwitchTo(nextState);
            }
            else
            {
                UpdateItemsLeftToBuyText(itemsLeftToBuy);
            }

            basicItemsShop.SetItemsLimit(itemsLeftToBuy);
        }

        private void UpdateItemsLeftToBuyText(int itemsLeftToBuy)
        {
            itemsLeftToBuyText.text =
                $"Need to buy {itemsLeftToBuy} more item{(itemsLeftToBuy > 1 ? "s" : string.Empty)}";
        }
    }
}
