using UnityEngine;

namespace InventoryGame.Items.Effects
{
    [CreateAssetMenu(fileName = "DynamiteChanceBoostEffect", menuName = "Inventory Game/Item Effects/Dynamite Chance Boost")]
    public class DynamiteChanceBoostEffect : ItemEffect
    {
        [Range(0f, 1f)]
        [SerializeField] private float amount;

        public override bool TryApply(Player owner, Player target, GameContext context)
        {
            var playerGameState = owner.GameState;
            if (playerGameState.DynamiteChanceNormalized >= 1f)
            {
                return false;
            }

            playerGameState.DynamiteChanceNormalized = Mathf.Clamp01(playerGameState.DynamiteChanceNormalized + amount);
            return true;
        }
    }
}
