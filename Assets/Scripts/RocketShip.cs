using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{

    #region Parameters
    #region Serialized Fields
    [SerializeField] float mainThrust = 2.35f;
    [SerializeField] float rotationalThrust = 0.75f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] GameObject TeleportTo;
    [SerializeField] GameObject secondTeleportTo;
    [SerializeField] GameObject button;
    [SerializeField] GameObject buttonReaction;
    [SerializeField] GameObject myCamera;
    [SerializeField] GameObject r1, r2, r3;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem flames;
    [SerializeField] HealthBar myHealthBar;
    #endregion
    
    ShakeCam shakeCamera;

    #region Booleans
    public bool turnButtonOn;
    public bool moving = true;
    public bool debug = false;
    public bool currentPerspective = false; // false = first person, true = shelf perspective
    #endregion

    #region Cameras
    public Camera cameraPosition;
    public Camera cameraPositionTwo;
    #endregion

    #region other
    int currentHealth;

    Rigidbody myRigidBody;

    AudioSource myAudioSource;
    Audiomanager audioManager;

    GameController gameController;
    #endregion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        shakeCamera = FindObjectOfType<ShakeCam>();

        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();

        audioManager = FindObjectOfType<Audiomanager>();

        gameController = FindObjectOfType<GameController>();

        cameraPosition.enabled = false;
        cameraPositionTwo.enabled = true;

        currentHealth = maxHealth;
        myHealthBar.SetMaxHealth(maxHealth);
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

        if (Input.GetKeyDown(KeyCode.M) && debug)
        {
            TakeDamage(30);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        myHealthBar.SetHealth(currentHealth);

        shakeCamera.ShakeCamera(CamShakeType.ROCKET_DAMAGE);
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

                ParticleSystem TempExplosion = Instantiate(explosion);
                TempExplosion.transform.position = gameObject.transform.position;
                TempExplosion.Play();

                audioManager.PlaySound("Explosion");

                TakeDamage(30);

                collision.gameObject.SetActive(false);

                if (currentHealth > 0) { break; }
                if (!debug) { gameObject.SetActive(false); }

                gameController.RocketDestroyed();

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

                // change camera location

                //mycamera.transform.position = camerapositiontwo.transform.position;
                //mycamera.transform.rotation = camerapositiontwo.transform.rotation;

                break;
            default:
                print("Died!");

                // Only disable Rocket

                if (debug) { return; } // ignore if in debug mode

                if (moving) // make sure that we haven't reached the end of the level yet, or if we're not in debug mode
                {
                    ContactPoint contact = collision.GetContact(0);
                    Vector3 pos = contact.point;

                    ParticleSystem newExplosion = Instantiate(explosion);
                    newExplosion.transform.position = pos;
                    newExplosion.Play();

                    audioManager.PlaySound("Explosion");

                    TakeDamage(30);

                    if (currentHealth > 0) { break; }

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
            if (!myAudioSource.isPlaying) { myAudioSource.Play(); }

            flames.Play();
            MovementControls("up");
        }
        else
        {
            flames.Stop();
            if (myAudioSource.isPlaying) { myAudioSource.Stop(); }
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

    private void changePerspective()
    {
        cameraPosition.enabled = !cameraPosition.enabled;
        cameraPositionTwo.enabled = !cameraPositionTwo.enabled;
    }
}
