using System.Collections;
using InventoryGame.FSM;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.CoreGameplay
{
    public class RevealingItemsState : StateBase
    {
        [SerializeField] private FiniteStateMachine fsm;
        [SerializeField] private StateId nextStateId;

        [SerializeField] private FullscreenMessage message;
        [SerializeField] private GameContext gameContext;
        [SerializeField] private float revealingDuration = 1f;

        [SerializeField] private ItemView playerSelectedItemView;
        [SerializeField] private ItemView aiSelectedItemView;
        [SerializeField] private HiddenObject hiddenObjectPlayer;
        [SerializeField] private HiddenObject hiddenObjectAi;

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

            hiddenObjectPlayer.SetHidden(false);
            hiddenObjectAi.SetHidden(false);
            playerSelectedItemView.Bind(gameContext.RealPlayer.GameState.SelectedBasicItem);
            aiSelectedItemView.Bind(gameContext.AIPlayer.GameState.SelectedBasicItem);

            yield return new WaitForSecondsRealtime(revealingDuration);
            fsm.SwitchTo(nextStateId);
        }
    }
}
