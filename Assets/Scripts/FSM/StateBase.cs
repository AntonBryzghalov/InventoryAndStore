using UnityEngine;

namespace InventoryGame.FSM
{
    public abstract class StateBase : MonoBehaviour
    {
        [SerializeField] private StateId stateId;

        public StateId StateId => stateId;

        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
