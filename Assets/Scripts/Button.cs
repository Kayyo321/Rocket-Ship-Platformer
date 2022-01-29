using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float time = 5.0f;
    public float maxInterval = 1.2f;
    public float minInterval = 0.2f;

    private float interval = 1.0f;
    private float timer = 30.0f;

    public GameObject teleportA;
    public GameObject teleportB;

    public bool buttonIsPressed;

    void Start()
    {
        timer = time;

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
        else if (!button)
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

        }
    }
}
