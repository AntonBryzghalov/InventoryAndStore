using InventoryGame.FSM;
using UnityEngine;

namespace InventoryGame.CoreGameplay.States
{
    public abstract class CoreGameplayState : StateBase
    {
        [SerializeField] private CoreGameplayStateId stateId;

        public override StateId StateId => stateId;
    }
}
