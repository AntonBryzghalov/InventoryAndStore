using System;
using System.Collections.Generic;
using System.Linq;
using InventoryGame.Items;
using UnityEngine;
using UnityEngine.Assertions;

namespace InventoryGame.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory Game/Inventory")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> _items = new();

        public event Action ItemsUpdated;

        public List<InventoryItem> Items => _items;

        public void AddItem(InventoryItem item)
        {
            Assert.IsTrue(item.Quantity >= 1, $"Item '{item.ItemInfo.ItemName}' quantity is less than 1");

            var sameItemInInventory = _items.FirstOrDefault(i => i.ItemInfo == item.ItemInfo);
            if (sameItemInInventory != null)
            {
                sameItemInInventory.Quantity += item.Quantity;
                ItemsUpdated?.Invoke();
                return;
            }

            _items.Add(item);
            ItemsUpdated?.Invoke();
        }

        public void RemoveItem(InventoryItem item)
        {
            Assert.IsTrue(item.Quantity >= 1, $"Attempt to remove less than 1 of '{item.ItemInfo.ItemName}' item");

            var index = _items.FindIndex(i => i.ItemInfo == item.ItemInfo);
            Assert.IsTrue(index >= 0, $"Item '{item.ItemInfo.ItemName}' does not exist in the inventory '{name}'");
            Assert.IsTrue(
                _items[index].Quantity >= item.Quantity,
                $"There is not enough of {item.ItemInfo.ItemName} items in the inventory ({_items[index].Quantity}/{item.Quantity})");

            if (_items[index].Quantity == item.Quantity)
            {
                _items.RemoveAt(index);
            }
            else
            {
                _items[index].Quantity -= item.Quantity;
            }

            ItemsUpdated?.Invoke();
        }

        public void RemoveAllBasicItems()
        {
            var itemsRemoved = 0;
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                if (_items[i].ItemInfo is BasicItemInfo)
                {
                    _items.RemoveAt(i);
                    itemsRemoved++;
                }
            }

            if (itemsRemoved > 0) ItemsUpdated?.Invoke();
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
