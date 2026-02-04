using System.Linq;
using InventoryGame.Events;
using InventoryGame.ExtensionMethods;
using InventoryGame.FSM;
using InventoryGame.GameLoop;
using InventoryGame.Inventory;
using InventoryGame.Items;
using InventoryGame.Shop;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.CoreGameplay
{
    // TODO: extract AI logic to a separate class
    public class ItemsSelectionState : StateBase
    {
        [SerializeField] private FiniteStateMachine fsm;
        [SerializeField] private StateId nextStateId;

        [SerializeField] private GameContext gameContext;
        [SerializeField] private InventoryItemEvent playerSelectedItemEvent;
        [SerializeField] private EffectItemInfo dynamiteBoosterItem;
        [SerializeField] private BasicItemInfo dynamiteItem;
        [SerializeField] private GameLoopConfig config;

        [SerializeField] private FormattedIntTextSetter roundText;
        [SerializeField] private FormattedFloatTextSetter playerDynamiteChancesText;
        [SerializeField] private FormattedFloatTextSetter aiDynamiteChancesText;

        public override void OnEnter()
        {
            SetNextRound();
            SetInitialDynamiteChances();
            ExecuteAiMove();
            playerSelectedItemEvent.AddListener(OnPlayerSelectedItem);
        }

        public override void OnExit()
        {
            playerSelectedItemEvent.RemoveListener(OnPlayerSelectedItem);
            TryConvertSelectedItemsToDynamites();
        }

        private void OnPlayerSelectedItem(InventoryItem inventoryItem)
        {
            var usedItem = new InventoryItem(inventoryItem.ItemInfo);

            if (usedItem.ItemInfo is BasicItemInfo)
            {
                gameContext.RealPlayer.GameState.SelectedBasicItem = usedItem.ItemInfo;
                gameContext.RealPlayer.Inventory.RemoveItem(usedItem);
                fsm.SwitchTo(nextStateId);
                return;
            }

            if (usedItem.ItemInfo is EffectItemInfo effectItem &&
                effectItem.Effect.TryApply(gameContext.RealPlayer, gameContext.AIPlayer, gameContext))
            {
                gameContext.RealPlayer.Inventory.RemoveItem(usedItem);
                if (usedItem.ItemInfo == dynamiteBoosterItem)
                {
                    UpdateDynamiteChancesTexts();
                }
            }
        }

        private void ExecuteAiMove()
        {
            var aiInventory = gameContext.AIPlayer.Inventory;
            TryToUseDynamiteBoosterByAi(aiInventory);

            var baseItems = aiInventory.Items
                .Where(item => item.ItemInfo is BasicItemInfo)
                .ToList();

            var selectedItem = baseItems.GetRandomElement();
            var usedItem = new InventoryItem(selectedItem.ItemInfo);
            gameContext.AIPlayer.GameState.SelectedBasicItem = usedItem.ItemInfo;
            aiInventory.RemoveItem(usedItem);
        }

        private void TryToUseDynamiteBoosterByAi(InventorySO inventory)
        {
            var boosterInventoryItem = inventory.Items.FirstOrDefault(item => item.ItemInfo == dynamiteBoosterItem);
            if (boosterInventoryItem == null)
            {
                return;
            }

            var shouldUseBooster =
                // if AI has it for more than 1 round
                boosterInventoryItem.Quantity > 1 ||
                // or it is a final round
                gameContext.Round == config.RoundsPerGame;

            if (!shouldUseBooster)
            {
                return;
            }

            var usedItem = new InventoryItem(boosterInventoryItem.ItemInfo);
            if (dynamiteBoosterItem.Effect.TryApply(gameContext.AIPlayer, gameContext.RealPlayer, gameContext))
            {
                inventory.RemoveItem(usedItem);
                UpdateDynamiteChancesTexts();
            }
        }

        private void SetNextRound()
        {
            ++gameContext.Round;
            roundText.SetValue(gameContext.Round);
        }

        private void SetInitialDynamiteChances()
        {
            gameContext.RealPlayer.GameState.DynamiteChanceNormalized = config.InitialDynamiteChance;
            gameContext.AIPlayer.GameState.DynamiteChanceNormalized = config.InitialDynamiteChance;
            UpdateDynamiteChancesTexts();
        }

        private void UpdateDynamiteChancesTexts()
        {
            playerDynamiteChancesText.SetValue(gameContext.RealPlayer.GameState.DynamiteChanceNormalized);
            aiDynamiteChancesText.SetValue(gameContext.AIPlayer.GameState.DynamiteChanceNormalized);
        }

        private void TryConvertSelectedItemsToDynamites()
        {
            TryConvertToDynamite(gameContext.RealPlayer.GameState);
            TryConvertToDynamite(gameContext.AIPlayer.GameState);
        }

        private void TryConvertToDynamite(PlayerGameState playerGameState)
        {
            if (Random.Range(0f, 1f) <= playerGameState.DynamiteChanceNormalized)
            {
                playerGameState.SelectedBasicItem = dynamiteItem;
            }
        }
    }
}
