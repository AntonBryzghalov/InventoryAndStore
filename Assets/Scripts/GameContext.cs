using UnityEngine;

namespace InventoryGame
{
    [CreateAssetMenu(fileName = "GameContext", menuName = "Inventory Game/Game GameContext", order = 0)]
    public class GameContext : ScriptableObject
    {
        public int Round = 0;

        [SerializeField] private Player realPlayer;
        [SerializeField] private Player aiPlayer;

        public Player RealPlayer => realPlayer;
        public Player AIPlayer => aiPlayer;

        private void OnDisable()
        {
            ResetTemporaryValues(true);
        }

        public void ResetTemporaryValues(bool fullCleanup)
        {
            Round = 0;
            ResetPlayerTempValues(realPlayer, fullCleanup);
            ResetPlayerTempValues(aiPlayer, fullCleanup);
        }

        private void ResetPlayerTempValues(Player player, bool fullCleanup)
        {
            player.GameState.Score = 0;
            player.GameState.SelectedBasicItem = null;

            if (fullCleanup)
            {
                player.WinsCount = 0;
            }
        }
    }
}