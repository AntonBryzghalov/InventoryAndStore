using InventoryGame.FSM;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private GameLoopConfig config;
        
        [Tooltip("Player's references")]
        [SerializeField] private Player realPlayer;
        [SerializeField] private Player aiPlayer;

        [Tooltip("States")]
        [SerializeField] private FiniteStateMachine stateMachine;
        [SerializeField] private StateId initialState;

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
