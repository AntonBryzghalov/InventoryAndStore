using System;
using InventoryGame.Items;
using UnityEngine;
using UnityEngine.Assertions;

namespace InventoryGame.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        [SerializeField] private ItemInfo _itemInfo;
        [SerializeField] private int _quantity;
        public ItemInfo ItemInfo => _itemInfo;

        public int Quantity
        {
            get => _quantity;
            set => SetQuantity(value);
        }

        public InventoryItem(ItemInfo itemInfo, int quantity = 1)
        {
            _itemInfo = itemInfo;
            SetQuantity(quantity);
        }

        private void SetQuantity(int quantity)
        {
            Assert.IsTrue(quantity > 0, "Quantity must be greater than zero.");
            _quantity = quantity;
        }
    }
}