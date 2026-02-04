using InventoryGame.FSM;
using UnityEngine;

namespace InventoryGame.CoreGameplay.States
{
    public abstract class CoreGameplayState : StateBase<CoreGameplayStateId>
    {
        [SerializeField] private CoreGameplayStateId stateId;

        public override CoreGameplayStateId StateId => stateId;
    }
}
