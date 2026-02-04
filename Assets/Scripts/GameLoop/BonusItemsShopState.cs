using System.Collections.Generic;
using InventoryGame.Events;
using InventoryGame.FSM;
using InventoryGame.Inventory;
using InventoryGame.Items;
using InventoryGame.Shop;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class BonusItemsShopState : UIState
    {
        [SerializeField] private FiniteStateMachine fsm;
        [SerializeField] private StateId nextState;

        [SerializeField] private ShopComponent bonusItemsShop;
        [SerializeField] private InventorySO playerInventory;
        [SerializeField] private InventorySO aiInventory;
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private Wallet aiWallet;
        [SerializeField] private ItemInfo dynamiteChanceIncreaseItem;
        [SerializeField] private InventoryItemEvent itemPurchasedEvent;

        private IReadOnlyList<ItemInfo> ShopItems => bonusItemsShop.ItemsSet.List;

        public override void OnEnter()
        {
            base.OnEnter();
            itemPurchasedEvent.AddListener(OnItemPurchased);
            CheckForTransitionToNextState();
        }

        public override void OnExit()
        {
            base.OnExit();
            itemPurchasedEvent.RemoveListener(OnItemPurchased);
            BuyItemsForAI();
        }

        private void BuyItemsForAI()
        {
            var affordableDynamiteQuantity = aiWallet.GoldAmount / dynamiteChanceIncreaseItem.BasePrice;
            if (affordableDynamiteQuantity == 0)
            {
                return;
            }

            var inventoryItem = new InventoryItem(dynamiteChanceIncreaseItem, affordableDynamiteQuantity);
            aiInventory.AddItem(inventoryItem);
            aiWallet.GoldAmount -= dynamiteChanceIncreaseItem.BasePrice * affordableDynamiteQuantity;
        }

        private void OnItemPurchased(InventoryItem _)
        {
            CheckForTransitionToNextState();
        }

        private void CheckForTransitionToNextState()
        {
            if (!PlayerCanAffordAnything())
            {
                fsm.SwitchTo(nextState);
            }
        }

        private bool PlayerCanAffordAnything()
        {
            foreach (var itemInfo in ShopItems)
            {
                if (playerWallet.GoldAmount >= itemInfo.BasePrice)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
