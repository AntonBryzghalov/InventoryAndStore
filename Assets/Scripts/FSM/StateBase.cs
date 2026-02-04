using UnityEngine;

namespace InventoryGame.FSM
{
    public abstract class StateBase : MonoBehaviour
    {
        public abstract StateId StateId { get; }

        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
