using UnityEngine;

namespace InventoryGame.Items
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "Inventory Game/ItemInfo")]
    public abstract class ItemInfo : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int basePrice;

        public string ItemName => itemName;
        public string Description => description;
        public Sprite Sprite => sprite;
        public int BasePrice => basePrice;
    }
}
