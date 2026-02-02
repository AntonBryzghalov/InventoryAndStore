using UnityEngine;

namespace InventoryGame.Items
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "Inventory Game/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int basePrice;

        public string Name => name;
        public string Description => description;
        public Sprite Sprite => sprite;
        public int BasePrice => basePrice;
    }
}
