using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] ShakeCam   shakeCamera;

    public bool debug;

    RocketShip rocketShip;

    private void Start()
    {
        debug = false;

        rocketShip = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<RocketShip>();

        Debug.Assert(rocketShip != null);
    }

    public void ResetGame()
    {
        StopAllCoroutines();

        StartCoroutine(LoadFirstLevel());
    }

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

    public void RocketLandedOnLandingPad()
    {
        NextLevel();
    }

    private IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

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

    private void NextLevel()
    {
        StopAllCoroutines();
        StartCoroutine(LoadNextLevel());
    }

    private void LastLevel()
    {
        StopAllCoroutines();
        StartCoroutine(LoadLastLevel());
    }

    private void Update()
    {
        if (!rocketShip.isActiveAndEnabled)
        {
            StartCoroutine(LoadFirstLevel());
            
            return;
        }

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
