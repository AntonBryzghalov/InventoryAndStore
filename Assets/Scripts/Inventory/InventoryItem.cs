using System;
using InventoryGame.Items;
using UnityEngine.Assertions;

namespace InventoryGame.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public readonly ItemInfo ItemInfo;
        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set => SetQuantity(value);
        }
        public InventoryItem(ItemInfo itemInfo, int quantity = 1)
        {
            ItemInfo = itemInfo;
            SetQuantity(quantity);
        }
        
        private void SetQuantity(int quantity)
        {
            Assert.IsTrue(quantity > 0);
            _quantity = quantity;
        }
    }
}