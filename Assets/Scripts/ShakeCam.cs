using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CamShakeType
{
    ROCKET_EXPLODE = 0,
    ROCKET_DAMAGE = 1
}

public class ShakeCam : MonoBehaviour
{
    [SerializeField] float intensity;

    private CinemachineVirtualCamera myVirtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        myVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(CamShakeType shakeType)
    {
        StopAllCoroutines();

        StartCoroutine(ScreenShake(shakeType));
    }

    private IEnumerator ScreenShake(CamShakeType shakeType)
    {
        CinemachineBasicMultiChannelPerlin noise = myVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (shakeType == CamShakeType.ROCKET_EXPLODE)
        {
            noise.m_AmplitudeGain = intensity;
            noise.m_FrequencyGain = intensity;
        }
        else if (shakeType == CamShakeType.ROCKET_DAMAGE)
        {
            noise.m_AmplitudeGain = intensity / 10.0f;
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
