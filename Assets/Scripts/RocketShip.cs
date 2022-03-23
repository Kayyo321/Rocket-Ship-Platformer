using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class holds the code for all of the rocketships, and their functionalities.
/// </summary>
public class RocketShip : MonoBehaviour
{

    #region Parameters
    #region Serialized Fields
    [SerializeField] float mainThrust = 2.35f;
    [SerializeField] float rotationalThrust = 0.75f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem flames;
    [SerializeField] GameObject mainRocket;
    #endregion

    #region Booleans
    public bool moving = true;
    public bool mainRocketAlive;
    #endregion

    ShakeCam shakeCamera;

    #region other
    int currentHealth;

    HealthBar myHealthBar;
    CanvasFade canvasFade;

    Rigidbody myRigidBody;

    AudioSource myAudioSource;
    Audiomanager audioManager;

    GameController gameController;
    #endregion
    #endregion

    /// <summary>
    /// Initializes all of the private functions.
    /// </summary>
    void Start()
    {
        mainRocketAlive = true;

        shakeCamera = FindObjectOfType<ShakeCam>();

        myRigidBody = GetComponent<Rigidbody>();

        myAudioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<Audiomanager>();

        gameController = FindObjectOfType<GameController>();

        myHealthBar = gameObject.GetComponentInChildren<HealthBar>();
        canvasFade = gameObject.GetComponentInChildren<CanvasFade>();
        currentHealth = maxHealth;
        myHealthBar.SetMaxHealth(maxHealth);
    }
    
    /// <summary>
    /// Handles rocket movement:
    ///     * Rotation
    ///     * Rocket thrust
    /// 
    /// Handles Debug input that can't be done in the GameController:
    ///     * Turning rigidbody to kinematic
    ///     * Making the rocket invinsible
    ///     * Taking Damage
    /// </summary>
    void Update()
    {
        if (moving)
        {
            RocketMovement();
        }

        if (Input.GetKeyDown(KeyCode.RightBracket)) // Skip the level
        {
            if (!gameController.debug) { return; }

            myRigidBody.isKinematic = true;
            moving = false; // Make the player invincible
        }
       
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (!gameController.debug) { return; }

            myRigidBody.isKinematic = true;
            moving = false; // Make the player invincible.
        }
        if (Input.GetKeyDown(KeyCode.M) && gameController.debug)
        {
            TakeDamage(30);
        }
    }

    /// <summary>
    /// Handles physical rotation.
    /// </summary>
    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }

    /// <summary>
    /// Handles damage to the rocket:
    ///     * Decreasing the rockets health
    ///     * Fading the healthbar
    ///     * Shaking the camera
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        myHealthBar.SetHealth(currentHealth);

        canvasFade.Fade();

        shakeCamera.ShakeCamera(CamShakeType.ROCKET_DAMAGE);
    }

    /// <summary>
    /// Handles rocketship collisions:
    ///     * Landing pad
    ///     * Destructable walls:
    ///     * Teleporters
    ///     * Buttons
    ///     * Everything else (just takes damage)
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// Handles effects of bad collision:
    ///     * Creates an explosion effect
    ///     * Plays a explosion sfx
    ///     * Decreases rocket's health
    ///     * Destroys the rocket if health is >= 0
    ///     * Changes camera shake edition (i. e. Rocket Died, or Rocket Damaged)
    /// </summary>
    /// <param name="collision"></param>
    private void collisionsHandeling(Collision collision)
    {
        ParticleSystem TempExplosion     = Instantiate(explosion);
        TempExplosion.transform.position = gameObject.transform.position;
        TempExplosion.Play();

        if (TempExplosion.isStopped) { Destroy(TempExplosion); }

        audioManager.PlaySound("Explosion");

        if (!gameController.debug)
            TakeDamage(30);

        if (currentHealth > 0) { return; }
        
        if (gameObject != mainRocket)
        {
            gameObject.SetActive(false);
            gameController.RocketDestroyed();
        }
        else
        {
            mainRocketAlive = false;
            gameController.RocketDestroyed();

            ParticleSystem tmpExplosion     = Instantiate(explosion);
            tmpExplosion.transform.position = gameObject.transform.position;
            tmpExplosion.Play();

            shakeCamera.ShakeCamera(CamShakeType.ROCKET_EXPLODE);
        }
    }

    /// <summary>
    /// Forces the player to land the rocket on it's legs
    /// to finish the level.
    /// </summary>
    public void LandedOnLegs()
    {
        print("Success!");
        myRigidBody.isKinematic = true;
        moving = false; // make the player invincible

        audioManager.PlaySound("Success");

        gameController.RocketLandedOnLandingPad();
    }

    /// <summary>
    /// Takes input for movement handling.
    /// </summary>
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

    /// <summary>
    /// Uses input to change rotation and thrust
    /// of the rocket.
    /// </summary>
    /// <param name="direction"></param>
    private void MovementControls(string direction)
    {
        myRigidBody.freezeRotation = true;

        float rotationSpeed = rotationalThrust * Time.deltaTime;
        float thrustScale = mainThrust * Time.deltaTime;

        if (mainRocketAlive)
        {
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
}
