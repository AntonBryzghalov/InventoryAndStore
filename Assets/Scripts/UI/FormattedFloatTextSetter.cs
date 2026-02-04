using TMPro;
using UnityEngine;

namespace InventoryGame.UI
{
    public class FormattedFloatTextSetter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format = "{0:P0}";

        public void SetValue(float value) => text.text = string.Format(format, value);
    }
}