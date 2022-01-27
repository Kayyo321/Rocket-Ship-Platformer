using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    [SerializeField] float mainThrust = 2.35f;
    [SerializeField] float rotationalThrust = 0.75f;

    Rigidbody myRigidBody;
    AudioSource myAudioSource;

    public GameObject TeleportTo;
    public GameObject StartTeleporter;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided!");

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("I'm Okay!");
                break;
            case "Finish":
                print("Success!");
                break;
            case "Fuel":
                print("Fueled Up!");
                break;
            case "Destructable":
                print("Destroyed Something!");

                // Disable Rocket, and Object collided with

                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);

                break;
            case "Teleporter":
                print("Teleporting... Keep your arms inside the attraction please");

                transform.position = TeleportTo.transform.position;
                TeleportTo.gameObject.SetActive(false);

                break;
            default:
                print("Died!");

                // Only disable Rocket

                gameObject.SetActive(false);

                break;
        }
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }

    private void RocketMovement()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!myAudioSource.isPlaying)
                myAudioSource.Play();

            MovementControls("up");
        }
        else
        {
            if (myAudioSource.isPlaying)
                myAudioSource.Stop();
        }

        if (Input.GetKey(KeyCode.D))
        {
            MovementControls("right");
        }

        if (Input.GetKey(KeyCode.A))
        {
            MovementControls("left");
        }
    }

    private void MovementControls(string direction)
    {
        myRigidBody.freezeRotation = true;

        float rotationSpeed = rotationalThrust * Time.deltaTime;
        float thrustScale = mainThrust * Time.deltaTime;

        if (direction == "left")
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (direction == "right")
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        else if (direction == "up")
        {
            myRigidBody.AddRelativeForce(Vector3.up * thrustScale);
        }

        myRigidBody.freezeRotation = false;
    }
}
