using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes the healthbar attached to the rocket
/// stay dormant (invisible) until in use. (i. e. When damaged)
/// </summary>
public class CanvasFade : MonoBehaviour
{
    [SerializeField] float fadeDuration;

    /// <summary>
    /// Gets the canvas group of the healthbar, (used to get the transparancy variable) then calls the FadeCanvas function.
    /// </summary>
    public void Fade()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(FadeCanvas(canvasGroup));
    }

    /// <summary>
    /// Fades the healthbar canvas whenever called.
    /// </summary>
    /// <param name="cG"></param>
    /// <returns></returns>
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
