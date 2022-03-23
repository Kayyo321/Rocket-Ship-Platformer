using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes the camera rotate on the main menu
/// </summary>
public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] float speed;

    /// <summary>
    /// Changes the cameras position, very slowly, each fram 
    /// </summary>
    void Update()
    {
        transform.Rotate(0f, -speed * Time.deltaTime, 0f);        
    }
}
