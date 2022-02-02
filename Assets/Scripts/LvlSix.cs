using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSix : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject main;

    public GameController gameController;

    public float wait = 0f;

    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        if (wait < 2f)
        {
            if (!a.activeSelf && wait == 0f) 
            {
                a.SetActive(true); 
                wait = 1f; 
            }
            
            if (!b.activeSelf && wait == 1f) 
            {
                b.SetActive(true); 
                wait = 3f; 
            }
        }

        if (!main.activeSelf) { gameController.ResetGame(); }

    }
}
