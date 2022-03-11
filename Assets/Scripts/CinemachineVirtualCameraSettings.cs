using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineVirtualCameraSettings : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        GameObject rocketNose = GameObject.FindGameObjectWithTag("CameraFollow");

        virtualCamera.Follow = rocketNose.transform;
        virtualCamera.LookAt = rocketNose.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
