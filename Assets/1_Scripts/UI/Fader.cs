using System.Collections;
using UnityEngine;

namespace LD47.UI
{
    public class Fader : MonoBehaviour
    {
        Coroutine currentlyActiveFade = null;
        CanvasGroup cmp_canvasGroup;

        private void Awake()
        {
            cmp_canvasGroup = GetComponent<CanvasGroup>();
        }

        IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(cmp_canvasGroup.alpha, target))
            {
                cmp_canvasGroup.alpha = Mathf.MoveTowards(cmp_canvasGroup.alpha,
                                                            target, Time.deltaTime / time);
                yield return null;
            }
        }

        public Coroutine FadeToAlpha(float targetAlpha, float time)
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }

            currentlyActiveFade = StartCoroutine(FadeRoutine(targetAlpha, time));
            return currentlyActiveFade;
        }

        public void FadeOutImmediate()
        {
            cmp_canvasGroup.alpha = 1;
        }
    }
}
