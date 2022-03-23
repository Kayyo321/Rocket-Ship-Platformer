using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class handles all of the buttons on the pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField] Slider slider;

    public static bool paused = false;
    public GameObject pauseMenuUi;

    /// <summary>
    /// Makes sure that the slider holds the same value.
    /// </summary>
    private void Start()
    {
        slider.value = AudioListener.volume;   
    }

    /// <summary>
    /// Checks for keyboard input.
    /// </summary>
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

    /// <summary>
    /// Leaves the pause state.
    /// </summary>
    #region Pause Menu Buttons
    public void ResumeGame()
    {
        pauseMenuUi.SetActive(false);

        Time.timeScale = 1f; // Un-freeze the game.

        paused = false;
    }

    /// <summary>
    /// Enters the pause state.
    /// </summary>
    public void PauseGame()
    {
        pauseMenuUi.SetActive(true);

        Time.timeScale = 0f; // Freeze the game.

        paused = true;
    }

    /// <summary>
    /// Switches fullscreen on and off.
    /// </summary>
    public void FullScreenSetting()
    {
        Screen.fullScreen = !Screen.fullScreen;

        print($"Changing FullScreen Settings To : {Screen.fullScreen}");
    }

    /// <summary>
    /// Changes the game's volume based on the slider value.
    /// </summary>
    public void VolumeRocker()
    {
        AudioListener.volume = slider.value;
    }

    /// <summary>
    /// Leaves the level and goes to the main menu.
    /// </summary>
    public void ExitToMenu()
    {
        print("Going back to the main menu!");

        SceneManager.LoadScene(0);
    }
    #endregion
}
