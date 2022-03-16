using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overides : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera rocketCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled   = false;
        rocketCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
