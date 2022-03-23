using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class handles all of the buttons on the pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField] Slider slider;

    public static bool paused = false;
    public GameObject pauseMenuUi;

    private void Start()
    {
        slider.value = AudioListener.volume;   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    #region Pause Menu Buttons
    public void ResumeGame()
    {
        pauseMenuUi.SetActive(false);

        Time.timeScale = 1f; // Un-freeze the game

        paused = false;
    }

    public void PauseGame()
    {
        pauseMenuUi.SetActive(true);

        Time.timeScale = 0f; // Freeze the game

        paused = true;
    }

    public void FullScreenSetting()
    {
        Screen.fullScreen = !Screen.fullScreen;

        print($"Changing FullScreen Settings To : {Screen.fullScreen}");
    }

    public void VolumeRocker()
    {
        AudioListener.volume = slider.value;
    }
    public void ExitToMenu()
    {
        print("Going back to the main menu!");

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
    #endregion
}
