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
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem flames;
    [SerializeField] HealthBar myHealthBar;
    #endregion

    #region Booleans
    public bool moving = true;
    #endregion
    
    ShakeCam shakeCamera;

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

        if (Input.GetKeyDown(KeyCode.RightBracket)) // Skip Level
        {
            if (!gameController.debug) { return; }

            myRigidBody.isKinematic = true;
            moving = false; // make the player invincible
        }
       
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (!gameController.debug) { return; }

            myRigidBody.isKinematic = true;
            moving = false; // make the player invincible
        }
        if (Input.GetKeyDown(KeyCode.M) && gameController.debug)
        {
            TakeDamage(30);
        }
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
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
            case "Player":
                print("I'm Okay!");
                
                break;
            case "Destructable":
                print("Destroyed Something!");

                collisionsHandeling(collision);
                collision.gameObject.SetActive(false);

                break;
            case "Teleporter":
                print("Teleporting... Keep your arms inside the attraction please");

                Teleporter t = collision.gameObject.GetComponent<Teleporter>();

                if (t != null)
                {
                    transform.position = t.GetTeleportDestination();
                }


                break;
            case "Button":

                ButtonWall btn = collision.gameObject.GetComponent<ButtonWall>();

                if (btn != null)
                {
                    btn.Pressed();
                }

                break;
            default:
                print("Hit something!");

                if (moving) // make sure that we haven't reached the end of the level yet, or if we're not in debug mode
                {
                    collisionsHandeling(collision);
                }

                break;
        }
    }

    private void collisionsHandeling(Collision collision)
    {
        ParticleSystem TempExplosion = Instantiate(explosion);
        TempExplosion.transform.position = gameObject.transform.position;
        TempExplosion.Play();

        audioManager.PlaySound("Explosion");

        if (!gameController.debug)
            TakeDamage(30);

        if (currentHealth > 0) { return; }

        gameObject.SetActive(false);
        gameController.RocketDestroyed();

    }

    public void LandedOnLegs()
    {
        print("Success!");
        myRigidBody.isKinematic = true;
        moving = false; // make the player invincible

        audioManager.PlaySound("Success");

        gameController.RocketLandedOnLandingPad();
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
}
