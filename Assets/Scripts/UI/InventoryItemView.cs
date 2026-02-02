using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryGame.Inventory
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text quantity;

        public void Bind(InventoryItem item)
        {
            icon.sprite = item.ItemInfo.Sprite;
            itemName.text = item.ItemInfo.ItemName;
            quantity.text = item.Quantity.ToString();
        }
    }
}
