using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    [SerializeField] AudioClip success;

    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string audio)
    {
        if (audio == "Explosion") { myAudioSource.Play(); }
        else if (audio == "Success") { myAudioSource.PlayOneShot(success); }
    }
}
