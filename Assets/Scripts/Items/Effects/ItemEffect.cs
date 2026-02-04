using UnityEngine;

namespace InventoryGame.Items.Effects
{
    public abstract class ItemEffect : ScriptableObject
    {
        public abstract bool TryApply(Player owner, Player target, GameContext context);
    }
}
