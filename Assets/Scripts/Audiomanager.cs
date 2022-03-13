using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public RocketShip rocketShip;

    [SerializeField] AudioClip success, explosion;
    [SerializeField] AudioSource myAudioSource;

    public void PlaySound(string audio)
    {
        if (audio == "Explosion") { myAudioSource.PlayOneShot(explosion); }
        else if (audio == "Success") { myAudioSource.PlayOneShot(success); }
    }
}
