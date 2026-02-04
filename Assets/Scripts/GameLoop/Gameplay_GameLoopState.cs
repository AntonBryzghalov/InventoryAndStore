using InventoryGame.Events;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public sealed class Gameplay_GameLoopState : GameLoopState
    {
        [SerializeField] private GameLoopFiniteStateMachine fsm;
        [SerializeField] private GameLoopStateId nextState;

        [SerializeField] private ScriptableEvent gameOverEvent;
        
        public override void OnEnter()
        {
            base.OnEnter();
            gameOverEvent.AddListener(OnGameOver);
        }

        public override void OnExit()
        {
            gameOverEvent.RemoveListener(OnGameOver);
            base.OnExit();
        }

        private void OnGameOver()
        {
            fsm.SwitchTo(nextState);
        }
    }
}
