using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    [SerializeField] float fadeDuration;

    public void Fade()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(FadeCanvas(canvasGroup));
    }

    private IEnumerator FadeCanvas(CanvasGroup cG)
    {
        float counter = 0f;
        
        cG.alpha = 1f;

        yield return new WaitForSeconds(fadeDuration / 2);

        while (counter < fadeDuration / 2)
        {
            counter += Time.deltaTime;
            cG.alpha = Mathf.Lerp(1f, 0f, counter / (fadeDuration / 2));
            
            yield return null;
        }
    }
}
