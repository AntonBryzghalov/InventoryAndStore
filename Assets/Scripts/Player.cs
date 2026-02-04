using InventoryGame.CoreGameplay;
using InventoryGame.Inventory;
using UnityEngine;

namespace InventoryGame
{
    [CreateAssetMenu(fileName = "Player", menuName = "Inventory Game/Player")]
    public class Player : ScriptableObject
    {
        public int WinsCount { get; set; }

        [SerializeField] private Wallet wallet;
        [SerializeField] private InventorySO inventory;
        [SerializeField] private PlayerGameState gameState;

        public Wallet Wallet => wallet;
        public InventorySO Inventory => inventory;
        public PlayerGameState GameState => gameState;
    }
}
