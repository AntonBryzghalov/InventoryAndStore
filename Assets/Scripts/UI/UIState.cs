using InventoryGame.FSM;
using UnityEngine;

namespace InventoryGame.UI
{
    public abstract class UIState : StateBase
    {
        [SerializeField] private GameObject[] associatedObjects;

        public override void OnEnter()
        {
            foreach (var go in associatedObjects)
            {
                go.SetActive(true);
            }
        }

        public override void OnExit()
        {
            foreach (var go in associatedObjects)
            {
                go.SetActive(false);
            }
        }
    }
}
