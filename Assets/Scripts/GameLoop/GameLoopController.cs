using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private GameLoopConfig config;
        
        [Header("Player's references")]
        [SerializeField] private Player realPlayer;
        [SerializeField] private Player aiPlayer;

        [Header("States")]
        [SerializeField] private GameLoopFiniteStateMachine stateMachine;
        [SerializeField] private GameLoopStateId initialState;

        private void Start()
        {
            realPlayer.Inventory.Clear();
            aiPlayer.Inventory.Clear();
            realPlayer.Wallet.GoldAmount = config.InitialGoldAmount;
            aiPlayer.Wallet.GoldAmount = config.InitialGoldAmount;

            stateMachine.SwitchTo(initialState);
        }
    }
}
