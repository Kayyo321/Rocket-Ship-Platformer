using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void RocketDestroyed()
    {
        print("GameController.RocketDestroyed Executing...");
        print(FindObjectOfType<RocketShip>());

        if (FindObjectOfType<RocketShip>() == null)
        {
            print("Reseting The Game...");
            ResetGame();
        }
    }

    public void NextLevel()
    {
        var i_sceneToLoad = (SceneManager.GetActiveScene().buildIndex + 1);

        if (i_sceneToLoad != SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            print("Game Over!");

            return;

        }
    }
}
