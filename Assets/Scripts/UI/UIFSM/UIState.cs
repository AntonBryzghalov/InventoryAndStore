using UnityEngine;

namespace InventoryGame.UI.UIFSM
{
    public class UIState : MonoBehaviour
    {
        [SerializeField] private UIStateType stateType;
        [SerializeField] private GameObject[] associatedObjects;

        public UIStateType StateType => stateType;

        public void Enter()
        {
            foreach (var go in associatedObjects)
            {
                go.SetActive(true);
            }
        }
        
        public void Exit()
        {
            foreach (var go in associatedObjects)
            {
                go.SetActive(false);
            }
        }
    }
}
