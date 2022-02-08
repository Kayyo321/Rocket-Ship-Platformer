using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public RocketShip rocketShip;
    public LvlSix levelSix;

    [SerializeField] ShakeCam shakeCamera;

    public void ResetGame()
    {
        StopAllCoroutines();

        StartCoroutine(LoadFirstLevel());
    }

    public void RocketDestroyed()
    {
        print("GameController.RocketDestroyed Executing...");
        print(FindObjectOfType<RocketShip>());

        shakeCamera.ShakeCamera(CamShakeType.ROCKET_EXPLODE);

        if (FindObjectOfType<RocketShip>() == null)
        {
            print("Reseting The Game...");
            ResetGame();
        }
    }

    public IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    public IEnumerator LoadNextLevel()
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

    public IEnumerator LoadLastLevel()
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

    public void NextLevel()
    {
        StopAllCoroutines();
        StartCoroutine(LoadNextLevel());
    }

    public void LastLevel()
    {
        StopAllCoroutines();
        StartCoroutine(LoadLastLevel());
    }

    private void Update()
    {
        if (!levelSix.main.activeSelf)
        {
            
        }
        else { return; }
    }
}
