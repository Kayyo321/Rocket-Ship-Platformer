using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterLifetime : MonoBehaviour
{
    [SerializeField] float LifeSeconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LifeSeconds -= Time.deltaTime;

        if (LifeSeconds <= 0)
        {
            Destroy(gameObject);
        }
    }
}
