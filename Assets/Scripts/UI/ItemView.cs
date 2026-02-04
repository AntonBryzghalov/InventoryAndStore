using System;
using InventoryGame.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryGame.UI
{
    public class ItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemDescription;
        [SerializeField] private TMP_Text itemPrice;
        private ItemInfo _item;
    
        public event Action<ItemInfo> OnItemSelected;

        public void Bind(ItemInfo itemInfo)
        {
            Assert.IsNotNull(itemInfo);
            _item = itemInfo;

            icon.sprite = _item.Sprite;
            itemName.text = _item.ItemName;
            if (itemDescription != null) itemDescription.text = _item.Description;
            if (itemPrice != null) itemPrice.text = _item.BasePrice.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_item == null) return;
            OnItemSelected?.Invoke(_item);
        }
    }
}
