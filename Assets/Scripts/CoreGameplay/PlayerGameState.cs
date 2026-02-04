using InventoryGame.Items;
using UnityEngine;

namespace InventoryGame.CoreGameplay
{
    [CreateAssetMenu(fileName = "PlayerGameState", menuName = "Inventory Game/Player Game State")]
    public class PlayerGameState : ScriptableObject
    {
        public BasicItemInfo SelectedBasicItem;
        public int Score;
        public float DynamiteChanceNormalized;
        public bool EnemyInventoryUnveiled;

        private void OnDisable()
        {
            SelectedBasicItem = null;
            Score = 0;
            DynamiteChanceNormalized = 0;
            EnemyInventoryUnveiled = false;
        }
    }
}
