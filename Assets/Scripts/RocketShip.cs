using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    public float mainThrust = 2.35f;
    public float rotationalThrust = 0.75f;

    Rigidbody myRigidBody;
    AudioSource myAudioSource;

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
