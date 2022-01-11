using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    public float mainThrust = 2.35f; 

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
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidBody.AddRelativeForce(Vector3.up * mainThrust);
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("Turning Right");
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("Turning Left");
        }
    }
}
