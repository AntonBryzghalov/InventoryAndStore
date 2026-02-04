using UnityEngine;

namespace InventoryGame.FSM
{
    public abstract class StateBase<T> : MonoBehaviour
        where T : StateId
    {
        public abstract T StateId { get; }

        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
