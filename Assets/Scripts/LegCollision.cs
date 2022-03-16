using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegCollision : MonoBehaviour
{
    [SerializeField] RocketShip rocketShip; 

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":

                    print("RocketShip Landed On Legs");

                rocketShip.LandedOnLegs();

                break;
        }
    }
}
