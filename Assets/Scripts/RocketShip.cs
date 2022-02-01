using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    [SerializeField] float mainThrust = 2.35f;
    [SerializeField] float rotationalThrust = 0.75f;
    [SerializeField] GameObject TeleportTo;
    [SerializeField] GameObject secondTeleportTo;
    [SerializeField] GameObject button;
    [SerializeField] GameObject buttonReaction;
    [SerializeField] GameObject myCamera;
    [SerializeField] GameObject cameraPositionTwo;
    [SerializeField] ParticleSystem explosion;

    Rigidbody myRigidBody;

    AudioSource myAudioSource;
    Audiomanager audioManager;

    GameController gameController;

    public bool turnButtonOn;
    public bool moving = true;
    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();

        audioManager = FindObjectOfType<Audiomanager>();

        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            RocketMovement();
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            debug = !debug; // turns debug on to off, and off to on
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket)) // Skip Level
        {
            if (!debug) { return; }

            print("Skiping...");

            myRigidBody.isKinematic = true;
            moving = false; // make the player invincible

            gameController.NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (!debug) { return; }

            print("Skiping...");

            myRigidBody.isKinematic = true;
            moving = false; // make the player invincible

            gameController.LastLevel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!moving) { return; } // turn off any of the following collisions if we reached the end

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("I'm Okay!");
                break;
            case "Finish":
                print("Success!");
                myRigidBody.isKinematic = true;
                moving = false; // make the player invincible

                audioManager.PlaySound("Success");

                gameController.NextLevel();

                break;
            case "Destructable":
                print("Destroyed Something!");

                // Disable Rocket, and Object collided with

                ParticleSystem TempExplosion = explosion;
                TempExplosion.transform.position = gameObject.transform.position;
                TempExplosion.Play();

                audioManager.PlaySound("Explosion");

                if (!debug) { gameObject.SetActive(false); }
                
                gameController.RocketDestroyed();
                collision.gameObject.SetActive(false);

                break;
            case "Teleporter":
                print("Teleporting... Keep your arms inside the attraction please");

                transform.position = TeleportTo.transform.position;
                TeleportTo.gameObject.SetActive(false);

                break;
            case "SecondTeleporter":
                
                if (!turnButtonOn)
                { 
                    button.SetActive(true);
                    turnButtonOn = !turnButtonOn;
                }

                transform.position = secondTeleportTo.transform.position;
                secondTeleportTo.gameObject.SetActive(false);

                break;
            case "Button":

                button.SetActive(false);
                buttonReaction.SetActive(false);

                myCamera.transform.position = cameraPositionTwo.transform.position;
                myCamera.transform.rotation = cameraPositionTwo.transform.rotation;

                break;
            default:
                print("Died!");

                // Only disable Rocket

                if (debug) { return; } // ignore if in debug mode

                if (moving) // make sure that we haven't reached the end of the level yet, or if we're not in debug mode
                {
                    ParticleSystem newExplosion = explosion;
                    newExplosion.transform.position = gameObject.transform.position;
                    newExplosion.Play();

                    audioManager.PlaySound("Explosion");

                    gameObject.SetActive(false);
                    gameController.RocketDestroyed();
                }

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
