using System.Collections.Generic;
using System.Linq;
using InventoryGame.FSM;
using InventoryGame.Inventory;
using InventoryGame.Items;
using InventoryGame.Shop;
using InventoryGame.UI;
using TMPro;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class BasicItemsShopState : UIState
    {
        [SerializeField] private FiniteStateMachine fsm;
        [SerializeField] private StateId nextState;

        [SerializeField] private TMP_Text itemsLeftToBuyText;

        [SerializeField] private GameLoopConfig config;
        [SerializeField] private ShopComponent basicItemsShop;
        [SerializeField] private InventorySO playerInventory;
        [SerializeField] private InventorySO aiInventory;
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private Wallet aiWallet;
        
        private IReadOnlyList<ItemInfo> ShopItems => basicItemsShop.ItemsSet.List;

        public override void OnEnter()
        {
            base.OnEnter();
            playerWallet.GoldAmount += config.GoldGivenEachCycle;
            aiWallet.GoldAmount += config.GoldGivenEachCycle;
            playerInventory.ItemsUpdated += OnPlayerInventoryItemsUpdated;
            UpdateItemsLeftToBuyText(0);
            basicItemsShop.SetItemsLimit(config.BasicItemsToBuy);
        }

        public override void OnExit()
        {
            base.OnExit();
            playerInventory.ItemsUpdated -= OnPlayerInventoryItemsUpdated;
            BuyBasicItemsForAI();
        }

        private void BuyBasicItemsForAI()
        {
            var itemsAmount = config.BasicItemsToBuy;
            for (int i = 0; i < itemsAmount; i++)
            {
                var itemType = ShopItems[Random.Range(0, ShopItems.Count)];
                aiWallet.GoldAmount -= itemType.BasePrice;
                aiInventory.AddItem(new InventoryItem(itemType));
            }
        }

        private void OnPlayerInventoryItemsUpdated()
        {
            var basicItemsBought = playerInventory.Items
                .Where(item => ShopItems.Contains(item.ItemInfo))
                .Sum(item => item.Quantity);

            var itemsLeftToBuy = config.BasicItemsToBuy - basicItemsBought;
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
