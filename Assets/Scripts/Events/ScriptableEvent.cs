using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace InventoryGame.Events
{
    public abstract class ScriptableEvent<T> : ScriptableObject
    {
        private List<Action<T>> _listeners = new ();

        public void Raise(T value)
        {
            foreach (var listener in _listeners)
            {
                listener(value);
            }
        }

        public void Register(Action<T> listener)
        {
            Assert.IsNotNull(listener);
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void Unregister(Action<T> listener)
        {
            Assert.IsNotNull(listener);
            _listeners.Remove(listener);
        }
    }
}

