using System.Collections.Generic;
using UnityEngine;

namespace InventoryGame.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static T GetRandomElement<T>(this IReadOnlyList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}
