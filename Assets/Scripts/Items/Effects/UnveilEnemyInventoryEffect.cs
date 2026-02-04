using InventoryGame.Events;
using UnityEngine;

namespace InventoryGame.Items.Effects
{
    [CreateAssetMenu(fileName = "UnveilEnemyInventoryEffect", menuName = "Inventory Game/Item Effects/Unveil Enemy Inventory")]
    public class UnveilEnemyInventoryEffect : ItemEffect
    {
        [SerializeField] private ScriptableEvent enemyInventoryUnveiledEvent;

        public override bool TryApply(Player owner, Player target, GameContext gameContext)
        {
            // TODO: rework it to use some inventory visibility "manager"
            var playerGameState = owner.GameState;
            if (playerGameState.EnemyInventoryUnveiled)
            {
                return false;
            }

            enemyInventoryUnveiledEvent.Invoke();
            playerGameState.EnemyInventoryUnveiled = true;
            return true;
        }
    }

}
