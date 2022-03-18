using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTeleport : MonoBehaviour
{
    public GameObject teleportA;
    public GameObject teleportB;

    public bool buttonIsPressed;

    void Start()
    {
        try
        {
            teleportA.SetActive(false);
            teleportB.SetActive(false);
        }
        catch
        {
            if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                Debug.LogError("Could not find teleporters to turn off!");
            }
            else
            {
                return; // It won't matter
            }
        }
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
