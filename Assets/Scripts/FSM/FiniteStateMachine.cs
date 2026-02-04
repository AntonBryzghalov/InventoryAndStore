using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace InventoryGame.FSM
{
    public abstract class FiniteStateMachine<TStateId, TState> : MonoBehaviour
        where TStateId : StateId
        where TState : StateBase<TStateId>
    {
        [SerializeField] private TStateId startStateId;
        [SerializeField] private List<TState> states;

        private readonly Dictionary<StateId, TState> _stateMap = new();
        private StateBase<TStateId> _current;

        private void Awake()
        {
            foreach (var state in states)
            {
                Assert.IsFalse(_stateMap.ContainsKey(state.StateId), $"State with key {state.StateId} is already defined.");
                _stateMap[state.StateId] = state;
            }
        }

        private void Start()
        {
            if (startStateId != null)
            {
                SwitchTo(startStateId);
            }
        }

        public void SwitchTo(StateId newStateId)
        {
            Assert.IsNotNull(newStateId, "State type cannot be null.");
            Assert.IsTrue(_stateMap.ContainsKey(newStateId), $"There is no state with key {newStateId.name}");

            if (_current != null && _current.StateId == newStateId)
            {
                return;
            }

#if UNITY_EDITOR
            if (_current != null)
            {
                Debug.Log($"Exiting form {_current.StateId.name}");
            }
#endif
            _current?.OnExit();

            _current = _stateMap[newStateId];
#if UNITY_EDITOR
            Debug.Log($"Entering {_current.StateId.name}");
#endif
            _current.OnEnter();
        }
    }
}