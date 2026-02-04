using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace InventoryGame.Events
{
    [CreateAssetMenu(fileName = "ScriptableEvent", menuName = "Inventory Game/Events/Simple Event (no params)")]
    public class ScriptableEvent : ScriptableObject
    {
        private List<Action> _listeners = new ();

        private void OnDisable()
        {
            _listeners.Clear();
        }

        public void Invoke()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i]();
            }
        }

        public void AddListener(Action listener)
        {
            Assert.IsNotNull(listener);
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(Action listener)
        {
            Assert.IsNotNull(listener);
            _listeners.Remove(listener);
        }
    }

    public abstract class ScriptableEvent<T> : ScriptableObject
    {
        private List<Action<T>> _listeners = new ();

        private void OnDisable()
        {
            _listeners.Clear();
        }

        public void Invoke(T value)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i](value);
            }
        }

        public void AddListener(Action<T> listener)
        {
            Assert.IsNotNull(listener);
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(Action<T> listener)
        {
            Assert.IsNotNull(listener);
            _listeners.Remove(listener);
        }
    }
}

