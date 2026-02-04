using InventoryGame.FSM;
using UnityEngine;

namespace InventoryGame.GameLoop.States
{
    public abstract class GameLoopState : StateBase<GameLoopStateId>
    {
        [SerializeField] private GameLoopStateId stateId;
        [SerializeField] private GameObject[] associatedObjects;

        public override GameLoopStateId StateId => stateId;

        public override void OnEnter()
        {
            SetAssociatedObjectsOn(true);
        }

        public override void OnExit()
        {
            SetAssociatedObjectsOn(false);
        }

        private void SetAssociatedObjectsOn(bool on)
        {
            foreach (var go in associatedObjects)
            {
                go.SetActive(on);
            }
        }
    }
}
