using System.Collections.Generic;
using System.Linq;
using InventoryGame.Events;
using InventoryGame.Inventory;
using InventoryGame.Items;
using InventoryGame.Shop;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class BonusItemsShopState : GameLoopState
    {
        [SerializeField] private GameLoopFiniteStateMachine fsm;
        [SerializeField] private GameLoopStateId nextState;

        [SerializeField] private ShopComponent bonusItemsShop;

        [SerializeField] private GameContext context;
        [SerializeField] private EffectItemInfo dynamiteChanceIncreaseItem;
        [SerializeField] private InventoryItemEvent itemPurchasedEvent;

        private IReadOnlyList<ItemInfo> ShopItems => bonusItemsShop.ItemsSet.List;
        private Wallet PlayerWallet => context.RealPlayer.Wallet;
        private Wallet AIWallet => context.AIPlayer.Wallet;
        private InventorySO PlayerInventory => context.RealPlayer.Inventory;
        private InventorySO AIInventory => context.AIPlayer.Inventory;

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
            var affordableDynamiteQuantity = AIWallet.GoldAmount / dynamiteChanceIncreaseItem.BasePrice;
            if (affordableDynamiteQuantity == 0)
            {
                return;
            }

            var inventoryItem = new InventoryItem(dynamiteChanceIncreaseItem, affordableDynamiteQuantity);
            AIInventory.AddItem(inventoryItem);
            AIWallet.GoldAmount -= dynamiteChanceIncreaseItem.BasePrice * affordableDynamiteQuantity;
        }

        private void OnItemPurchased(InventoryItem _)
        {
            CheckForTransitionToNextState();
        }

        private void CheckForTransitionToNextState()
        {
            var playerCanAffordAnything = ShopItems.Any(item => PlayerWallet.GoldAmount >= item.BasePrice);
            if (!playerCanAffordAnything)
            {
                fsm.SwitchTo(nextState);
            }
        }
    }
}
