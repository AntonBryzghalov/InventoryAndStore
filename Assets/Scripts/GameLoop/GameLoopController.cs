using InventoryGame.GameLoop.States;
using InventoryGame.Inventory;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private GameLoopConfig config;
        
        [Header("Scriptable references")]
        [SerializeField] private GameContext context;

        [Header("States")]
        [SerializeField] private GameLoopFiniteStateMachine stateMachine;
        [SerializeField] private GameLoopStateId initialState;

        private Wallet PlayerWallet => context.RealPlayer.Wallet;
        private Wallet AIWallet => context.AIPlayer.Wallet;
        private InventorySO PlayerInventory => context.RealPlayer.Inventory;
        private InventorySO AIInventory => context.AIPlayer.Inventory;

        private void Start()
        {
            PlayerInventory.Clear();
            AIInventory.Clear();
            PlayerWallet.GoldAmount = config.InitialGoldAmount;
            AIWallet.GoldAmount = config.InitialGoldAmount;

            stateMachine.SwitchTo(initialState);
        }
    }
}
