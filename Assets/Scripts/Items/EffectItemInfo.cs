using InventoryGame.Items.Effects;
using UnityEngine;

namespace InventoryGame.Items
{
    [CreateAssetMenu(fileName = "EffectItemInfo", menuName = "Inventory Game/Items/Effect Item Info")]
    public class EffectItemInfo : ItemInfo
    {
        [SerializeField] private ItemEffect effect;

        public ItemEffect Effect => effect;
    }
}
