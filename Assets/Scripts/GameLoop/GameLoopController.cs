using InventoryGame.FSM;
using InventoryGame.Inventory;
using InventoryGame.Sets;
using InventoryGame.Shop;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private ShopComponent basicItemsShop;
        [SerializeField] private ShopComponent bonusItemsShop;
        [SerializeField] private ItemsSet bonusItemsSet;
        [SerializeField] private GameLoopConfig config;
        [SerializeField] private ItemPurchasedEvent itemPurchasedEvent;
        
        [Tooltip("Player's references")]
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private Wallet aiWallet;
        [SerializeField] private InventorySO playerInventory;
        [SerializeField] private InventorySO aiInventory;

        [Tooltip("States")]
        [SerializeField] private FiniteStateMachine stateMachine;
        [SerializeField] private StateId basicItemsState;
        [SerializeField] private StateId bonusItemsState;
        [SerializeField] private StateId gameplayState;

        private void Start()
        {
            playerInventory.Clear();
            aiInventory.Clear();
            playerWallet.GoldAmount = config.InitialGoldAmount;
            aiWallet.GoldAmount = config.InitialGoldAmount;

            stateMachine.SwitchTo(basicItemsState);
        }
    }
}
