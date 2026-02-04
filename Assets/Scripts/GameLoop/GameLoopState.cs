using InventoryGame.FSM;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.GameLoop
{
    public abstract class GameLoopState : UIState
    {
        [SerializeField] private GameLoopStateId stateId;

        public override StateId StateId => stateId;
    }
}
