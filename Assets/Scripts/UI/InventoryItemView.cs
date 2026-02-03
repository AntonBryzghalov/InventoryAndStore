using InventoryGame.UI;
using TMPro;
using UnityEngine;

namespace InventoryGame.Inventory
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private ItemView itemView;
        [SerializeField] private TMP_Text quantity;

        public void Bind(InventoryItem item)
        {
            itemView.SetItem(item.ItemInfo);
            quantity.text = item.Quantity.ToString();
        }
    }
}
