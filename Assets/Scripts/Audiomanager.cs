using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public RocketShip rocketShip, r2, r3;
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
        if (audio == "Explosion") 
        {
            if (rocketShip.currentPerspective || r2.currentPerspective || r3.currentPerspective)
                AudioSource.PlayClipAtPoint(explosion, rocketShip.cameraPosition.transform.position); 
            else { AudioSource.PlayClipAtPoint(explosion, rocketShip.cameraPositionTwo.transform.position); }
        }
        else if (audio == "Success") { AudioSource.PlayClipAtPoint(success, Camera.main.transform.position); }
    }
}
