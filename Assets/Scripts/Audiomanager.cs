using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    [SerializeField] AudioClip success, explosion;

    public AudioSource myAudioSource;

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
        if (audio == "Explosion") { AudioSource.PlayClipAtPoint(explosion, Camera.main.transform.position); }
        else if (audio == "Success") { AudioSource.PlayClipAtPoint(success, Camera.main.transform.position); }
    }
}
