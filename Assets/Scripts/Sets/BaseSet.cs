using System.Collections.Generic;
using UnityEngine;

namespace InventoryGame.Sets
{
    public abstract class BaseSet<T> : ScriptableObject
    {
        [SerializeField] private List<T> list;
        public IReadOnlyList<T> List => list;

        public void Add(T item)
        {
            if (!list.Contains(item)) list.Add(item);
        }

        public void Remove(T item) => list.Remove(item);
    }
}
