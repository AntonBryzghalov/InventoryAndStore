using UnityEngine;

namespace InventoryGame.UI
{
    public class HiddenObject : MonoBehaviour
    {
        [SerializeField] private GameObject hidingObject;
        [SerializeField] private GameObject hiddenObject;
        [SerializeField] private bool startHidden = true;

        private bool _initialized;

        public bool IsHidden { get; private set; }

        private void Start()
        {
            SetHidden(startHidden);
            _initialized = true;
        }

        public void SetHidden(bool isHidden)
        {
            if (_initialized)
            {
                if (IsHidden == isHidden) return;
            }
            else
            {
                IsHidden = isHidden;
            }

            hidingObject.SetActive(isHidden);
            hiddenObject.SetActive(!isHidden);
        }
    }
}
