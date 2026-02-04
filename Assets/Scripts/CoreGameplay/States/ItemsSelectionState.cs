using System.Linq;
using InventoryGame.Events;
using InventoryGame.ExtensionMethods;
using InventoryGame.GameLoop;
using InventoryGame.Inventory;
using InventoryGame.Items;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.CoreGameplay.States
{
    // TODO: extract AI logic to a separate class
    public class ItemsSelectionState : CoreGameplayState
    {
        [Header("FSM")]
        [SerializeField] private CoreGameplayFiniteStateMachine fsm;
        [SerializeField] private CoreGameplayStateId nextStateId;

        [Header("Scriptable References")]
        [SerializeField] private GameContext gameContext;
        [SerializeField] private InventoryItemEvent playerSelectedItemEvent;
        [SerializeField] private EffectItemInfo dynamiteBoosterItem;
        [SerializeField] private BasicItemInfo dynamiteItem;
        [SerializeField] private GameLoopConfig config;

        [Header("UI References")]
        [SerializeField] private HiddenObject hiddenObjectPlayer;
        [SerializeField] private HiddenObject hiddenObjectAi;
        [SerializeField] private FormattedIntTextSetter roundText;
        [SerializeField] private FormattedFloatTextSetter playerDynamiteChancesText;
        [SerializeField] private FormattedFloatTextSetter aiDynamiteChancesText;

        public override void OnEnter()
        {
            HideSelectedItemsSlots();
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

        private void HideSelectedItemsSlots()
        {
            hiddenObjectPlayer.SetHidden(true);
            hiddenObjectAi.SetHidden(true);
        }

        private void OnPlayerSelectedItem(InventoryItem inventoryItem)
        {
            var usedItem = new InventoryItem(inventoryItem.ItemInfo);

            if (usedItem.ItemInfo is BasicItemInfo basicItemInfo)
            {
                gameContext.RealPlayer.GameState.SelectedBasicItem = basicItemInfo;
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
            gameContext.AIPlayer.GameState.SelectedBasicItem = usedItem.ItemInfo as BasicItemInfo;
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
