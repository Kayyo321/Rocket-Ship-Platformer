using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    public float mainThrust = 2.35f;
    public float rotationalThrust = 0.75f;

    Rigidbody myRigidBody; 

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketMovement();
    }

    private void RocketMovement()
    {
        float rotationSpeed = rotationalThrust * Time.deltaTime;
        float thrustScale = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        { 
            myRigidBody.AddRelativeForce(Vector3.up * thrustScale);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
    }
}
