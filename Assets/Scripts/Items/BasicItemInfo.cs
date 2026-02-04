using UnityEngine;

namespace InventoryGame.Items
{
    [CreateAssetMenu(fileName = "BasicItemInfo", menuName = "Inventory Game/Items/Basic Item Info")]
    public class BasicItemInfo : ItemInfo
    {
        [SerializeField] private BasicItemType itemType;

        public BasicItemType ItemType => itemType;
    }
}
