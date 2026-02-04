using System.Collections;
using TMPro;
using UnityEngine;

namespace InventoryGame.UI
{
    public class FullscreenMessage : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text messageText;

        [SerializeField] private float fadeDuration = 0.3f;
        [SerializeField] private float defaultVisibleDuration = 1.5f;

        private Coroutine _routine;

        public bool IsDisplayed => _routine != null;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show(string message, float duration = 0f)
        {
            if (_routine != null)
                StopCoroutine(_routine);

            messageText.text = message;
            _routine = StartCoroutine(ShowRoutine(duration > 0f ? duration : defaultVisibleDuration));
        }

        private IEnumerator ShowRoutine(float visibleDuration)
        {
            yield return Fade(0f, 1f);
            yield return new WaitForSecondsRealtime(visibleDuration);
            yield return Fade(1f, 0f);

            _routine = null;
        }

        private IEnumerator Fade(float from, float to)
        {
            var time = 0f;
            canvasGroup.alpha = from;

            while (time < fadeDuration)
            {
                time += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(from, to, time / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = to;
        }
    }
}
