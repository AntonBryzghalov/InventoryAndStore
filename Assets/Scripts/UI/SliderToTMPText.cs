using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryGame.UI
{
    public class SliderToTMPText : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format = "{0:0}";

        private void OnEnable()
        {
            OnSliderValueChanged(slider.value);
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            if (slider != null)
            {
                slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            }
        }

        public void OnSliderValueChanged(float value)
        {
            text.text = string.Format(format, value);
        }
    }
}
