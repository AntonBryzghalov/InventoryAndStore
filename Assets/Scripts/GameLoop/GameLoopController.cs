using InventoryGame.Sets;
using InventoryGame.Shop;
using InventoryGame.UI.UIFSM;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private ShopComponent basicItemsShop;
        [SerializeField] private ShopComponent bonusItemsShop;
        [SerializeField] private ItemsSet bonusItemsSet;
        [SerializeField] private GameLoopConfig config;
        
        [Tooltip("Player's references")]
        [SerializeField] private Wallet playerWallet;
        [SerializeField] private Wallet aiWallet;
        [SerializeField] private Inventory.InventorySO playerInventory;
        [SerializeField] private Inventory.InventorySO aiInventory;

        [Tooltip("States")]
        [SerializeField] private UIStateMachine uiStateMachine;
        [SerializeField] private UIStateType basicItemsUIState;
        [SerializeField] private UIStateType bonusItemsUIState;
        [SerializeField] private UIStateType gameplayUIState;

        private void Start()
        {
            uiStateMachine.SwitchTo(basicItemsUIState);
        }
    }
}
