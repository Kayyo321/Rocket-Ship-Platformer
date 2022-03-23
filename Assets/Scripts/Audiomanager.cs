using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles sound output
/// </summary>
public class Audiomanager : MonoBehaviour
{
    public RocketShip rocketShip;

    [SerializeField] AudioClip success, explosion;
    [SerializeField] AudioSource myAudioSource;

    /// <summary>
    /// Handles sound output:
    ///     * Rocket explosion sound
    ///     * Level succession sound
    /// </summary>
    /// <param name="audio"></param>
    public void PlaySound(string audio)
    {
        if (audio == "Explosion") { myAudioSource.PlayOneShot(explosion); }
        else if (audio == "Success") { myAudioSource.PlayOneShot(success); }
    }
}
