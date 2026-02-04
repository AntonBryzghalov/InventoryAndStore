using System;
using InventoryGame.Items;
using InventoryGame.UI;
using TMPro;
using UnityEngine;

namespace InventoryGame.Inventory
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private ItemView itemView;
        [SerializeField] private TMP_Text quantity;
        private InventoryItem _item;
        
        public event Action<InventoryItem> OnItemSelected;

        private void Start()
        {
            itemView.OnItemSelected += PropagateOnItemSelected;
        }

        private void PropagateOnItemSelected(ItemInfo _)
        {
            OnItemSelected?.Invoke(_item);
        }

        public void Bind(InventoryItem item)
        {
            _item = item;
            itemView.Bind(item.ItemInfo);
            quantity.text = item.Quantity.ToString();
        }
    }
}
