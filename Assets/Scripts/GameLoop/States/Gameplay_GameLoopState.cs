using InventoryGame.CoreGameplay.States;
using InventoryGame.Events;
using UnityEngine;

namespace InventoryGame.GameLoop.States
{
    public sealed class Gameplay_GameLoopState : GameLoopState
    {
        [Header("Game Loop FSM")]
        [SerializeField] private GameLoopFiniteStateMachine gameLoopFsm;
        [SerializeField] private GameLoopStateId nextState;

        [Header("Core Game FSM")]
        [SerializeField] private CoreGameplayFiniteStateMachine coreFsm;
        [SerializeField] private CoreGameplayStateId initialCoreState;

        [Header("Scriptables References")]
        [SerializeField] private ScriptableEvent gameOverEvent;
        
        public override void OnEnter()
        {
            base.OnEnter();
            gameOverEvent.AddListener(OnGameOver);
            coreFsm.SwitchTo(initialCoreState);
        }

        public override void OnExit()
        {
            gameOverEvent.RemoveListener(OnGameOver);
            base.OnExit();
        }

        private void OnGameOver()
        {
            gameLoopFsm.SwitchTo(nextState);
        }
    }
}
