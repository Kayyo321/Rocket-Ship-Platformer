using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


/// <summary>
/// This class controls the flow of the game based on user input and the 
/// state of the rocket.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] ShakeCam   shakeCamera;

    public bool debug;

    RocketShip rocketShip;

    /// <summary>
    /// Initializing properties;
    /// Requires that an instance of the rocket-ship is in your scene.
    /// </summary>
    private void Start()
    {
        debug = false;

        rocketShip = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<RocketShip>();

        Debug.Assert(rocketShip != null);
    }

    /// <summary>
    /// Stops all remaining coroutines and loads the loads the main menu.
    /// </summary>
    public void ResetGame()
    {
        StopAllCoroutines();

        StartCoroutine(LoadFirstLevel());
    }

    /// <summary>
    /// This handles the camera shake, when a rocket is destroyed,
    /// and resets the game if the middle rocket is no longer alive.
    /// </summary>
    public void RocketDestroyed()
    {
        print("GameController.RocketDestroyed Executing...");
        print(FindObjectOfType<RocketShip>());

        shakeCamera.ShakeCamera(CamShakeType.ROCKET_DAMAGE);

        if (!rocketShip.mainRocketAlive)
        {
            print("Reseting The Game...");
            ResetGame();
        }
    }

    /// <summary>
    /// This handles level progression whenever the rocket lands 
    /// on the landing pad.
    /// </summary>
    public void RocketLandedOnLandingPad()
    {
        NextLevel();
    }
    
    // Waits for 2 seconds, then loads the main menu.
    private IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    // Waits for 2 seconds, then loads the next level, if there is one.
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2f);

        var i_sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1);

        if (i_sceneToLoad != SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(i_sceneToLoad);
            print(SceneManager.sceneCountInBuildSettings);
        }
        else
        {
            print("Game Over!");
            ResetGame();
        }
    }

    // Waits for 2 seconds, then loads the previous level.
    private IEnumerator LoadLastLevel()
    {
        yield return new WaitForSeconds(2f);

        var i_sceneToLoad = (SceneManager.GetActiveScene().buildIndex - 1);

        if (i_sceneToLoad != -1)
        {
            SceneManager.LoadScene(i_sceneToLoad);
            print(SceneManager.sceneCountInBuildSettings);
        }
        else
        {
            print("Hold it right there!");
            ResetGame();
        }
    }

    // Loads the next level in the build index.
    private void NextLevel()
    {
        StopAllCoroutines();
        StartCoroutine(LoadNextLevel());
    }

    // Loads the previous.
    private void LastLevel()
    {
        StopAllCoroutines();
        StartCoroutine(LoadLastLevel());
    }

    /// <summary>
    /// Handles all keyboard input related to debugging:
    ///     * "\" Enables Debuging 
    ///     * "[" Loads previous level
    ///     * "]" Loads next level
    /// </summary>
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            debug = !debug;
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (!debug) { return; }

            print("Skiping...");
            NextLevel();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (!debug) { return; }

            print("Skiping...");
            LastLevel();
        }
    }
}
