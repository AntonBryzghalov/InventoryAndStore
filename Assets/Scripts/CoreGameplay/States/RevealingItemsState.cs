using System.Collections;
using InventoryGame.FSM;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.CoreGameplay.States
{
    public class RevealingItemsState : CoreGameplayState
    {
        [Header("FSM")]
        [SerializeField] private FiniteStateMachine fsm;
        [SerializeField] private CoreGameplayStateId nextStateId;

        [Header("Scriptable References")]
        [SerializeField] private FullscreenMessage message;
        [SerializeField] private GameContext gameContext;

        [Header("UI References")]
        [SerializeField] private ItemView playerSelectedItemView;
        [SerializeField] private ItemView aiSelectedItemView;
        [SerializeField] private HiddenObject hiddenObjectPlayer;
        [SerializeField] private HiddenObject hiddenObjectAi;

        [Header("Configuration")]
        [SerializeField] private float displayDurationAfterRevealing = 2f;

        public override void OnEnter()
        {
            StartCoroutine(RevealingCoroutine());
        }

        public override void OnExit()
        {
        }

        private IEnumerator RevealingCoroutine()
        {
            message.Show("Revealing");
            while (message.IsDisplayed)
            {
                yield return null;
            }

            RevealItems();
            yield return new WaitForSecondsRealtime(displayDurationAfterRevealing);

            fsm.SwitchTo(nextStateId);
        }

        private void RevealItems()
        {
            hiddenObjectPlayer.SetHidden(false);
            hiddenObjectAi.SetHidden(false);
            playerSelectedItemView.Bind(gameContext.RealPlayer.GameState.SelectedBasicItem);
            aiSelectedItemView.Bind(gameContext.AIPlayer.GameState.SelectedBasicItem);
        }
    }
}
