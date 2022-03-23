using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles handles code that disables some features in level 7, that are in the rest of the game
/// </summary>
public class TakeInputLvl7 : MonoBehaviour
{
    [SerializeField] Camera camMove;

    private float fov;

    /// <summary>
    /// Initializes the cameras field of view
    /// </summary>
    void Start()
    {
        fov = 70;
    }

    /// <summary>
    /// Handles input to rotate the camera:
    ///     * "UpArrow" Rotates the camera upwards
    ///     * "DownArrow" Rotates the camera downwards
    /// </summary>
    void Update()
    { 
        if (Input.GetKey(KeyCode.UpArrow) && !(fov >= 90))
        {
            fov++;

            camMove.transform.Rotate(-1f, 0f, 0f, Space.Self);
        }   
        else if (Input.GetKey(KeyCode.DownArrow) && !(fov <= -90))
        {
            fov--;

            camMove.transform.Rotate(1f, 0f, 0f, Space.Self);
        }
    }
}
