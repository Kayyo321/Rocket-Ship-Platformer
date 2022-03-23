using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes sure that the rocket lands on it's legs when finishing a level
/// </summary>
public class LegCollision : MonoBehaviour
{
    [SerializeField] RocketShip rocketShip; 

    private BoxCollider boxCollider;

    /// <summary>
    /// Initializes the colider for the legs
    /// </summary>
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// Handles collision for rocket when it lands on it's legs
    /// </summary>
    /// <param name="collision"></param>
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
