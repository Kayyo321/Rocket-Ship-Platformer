using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTeleport : MonoBehaviour
{
    public GameObject teleportA;
    public GameObject teleportB;

    public bool buttonIsPressed;

    void Start()
    {
        teleportA.SetActive(false);
        teleportB.SetActive(false);
    }

    public void SwitchTeleportState(bool button)
    {
        if (button)
        {
            teleportA.SetActive(true);
            teleportB.SetActive(true);
        }
        else
        {
            teleportA.SetActive(false);
            teleportB.SetActive(false);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<RocketShip>())
        {

            print("Rocket is on the button!");
            buttonIsPressed = true;
            SwitchTeleportState(buttonIsPressed);

            gameObject.SetActive(false);

        }
    }
}
