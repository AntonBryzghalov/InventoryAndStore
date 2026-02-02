using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace InventoryGame.UI.UIFSM
{
    public class UIStateMachine : MonoBehaviour
    {
        [SerializeField] private UIStateType starUIStateType;
        [SerializeField] private List<UIState> states;

        private readonly Dictionary<UIStateType, UIState> _stateMap = new();
        private UIState _current;

        private void Awake()
        {
            foreach (var state in states)
            {
                _stateMap[state.StateType] = state;
                state.Exit(); // Turn off all gameObjects of all states
            }
        }

        private void Start()
        {
            if (starUIStateType != null)
            {
                SwitchTo(starUIStateType);
            }
        }

        public void SwitchTo(UIStateType newState)
        {
            Assert.IsNotNull(newState, "State type cannot be null.");
            Assert.IsTrue(_stateMap.ContainsKey(newState), $"There is no state with key {newState}");

            if (_current != null && _current.StateType == newState)
            {
                return;
            }

            _current?.Exit();

            _current = _stateMap[newState];
            _current.Enter();
        }
    }
}