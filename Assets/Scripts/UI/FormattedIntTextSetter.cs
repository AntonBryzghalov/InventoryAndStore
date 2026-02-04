using TMPro;
using UnityEngine;

namespace InventoryGame.UI
{
    public class FormattedIntTextSetter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format = "{0:D0}";

        public void SetValue(int value) => text.text = string.Format(format, value);

        private void OnValidate()
        {
            if (text == null) text = GetComponent<TMP_Text>();
        }
    }
}
