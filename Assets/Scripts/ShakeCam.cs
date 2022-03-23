using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// This enum holds the different explosion types.
/// </summary>
public enum CamShakeType
{
    ROCKET_EXPLODE = 0,
    ROCKET_DAMAGE = 1
}

/// <summary>
/// This class handles camera shaking whenever rocket is damaged.
/// </summary>
public class ShakeCam : MonoBehaviour
{
    [SerializeField] float intensity;

    private CinemachineVirtualCamera myVirtualCamera;

    /// <summary>
    /// Initializes the camera because it is a private variable.
    /// </summary>
    void Start()
    {
        myVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    /// <summary>
    /// Calls the ScreenShake function which shakes the screen with different types of explosion.
    /// </summary>
    /// <param name="shakeType"></param>
    public void ShakeCamera(CamShakeType shakeType)
    {
        StopAllCoroutines();

        StartCoroutine(ScreenShake(shakeType));
    }

    /// <summary>
    /// Shakes the screen by different amounts based on the parameter given.
    /// </summary>
    /// <param name="shakeType"></param>
    /// <returns>Waits for a 10th of a second</returns>
    private IEnumerator ScreenShake(CamShakeType shakeType)
    {
        CinemachineBasicMultiChannelPerlin noise = myVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (shakeType == CamShakeType.ROCKET_EXPLODE)
        {
            noise.m_AmplitudeGain = intensity * 10f;
            noise.m_FrequencyGain = intensity * 10f;
        }
        else if (shakeType == CamShakeType.ROCKET_DAMAGE)
        {
            noise.m_AmplitudeGain = intensity / 10f;
            noise.m_FrequencyGain = intensity / 10;
        }

        while (noise.m_AmplitudeGain > 0.01f)
        {
            noise.m_AmplitudeGain *= 0.5f;
            noise.m_FrequencyGain *= 0.5f;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
