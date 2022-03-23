using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is designed for gameobjects that wait a few seconds before being destroyed.
/// </summary>
public class KillAfterLifetime : MonoBehaviour
{
    [SerializeField] float LifeSeconds;

    /// <summary>
    /// Destroys the gameobject after a set amout of seconds.
    /// </summary>
    void Update()
    {
        LifeSeconds -= Time.deltaTime;

        if (LifeSeconds <= 0)
        {
            Destroy(gameObject);
        }
    }
}
