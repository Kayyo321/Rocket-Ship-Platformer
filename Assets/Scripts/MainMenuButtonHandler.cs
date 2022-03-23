using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class holds all of the functions that are called whenever you press a button on the main menu
/// </summary>
public class MainMenuButtonHandler : MonoBehaviour
{
    #region SerializedFields
    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject pos1;
    [SerializeField] GameObject pos2;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject optionsMenuCanvas;

    [SerializeField] Slider slider;
    #endregion

    /// <summary>
    /// Resets the canvas's and volume to their default settings. (i.e. main menu buttons, 50%)
    /// </summary>
    private void Start()
    {
        slider.value = AudioListener.volume;

        mainMenuCanvas.gameObject.SetActive(true);
        optionsMenuCanvas.gameObject.SetActive(false);
    }

    #region MainMenu
    /// <summary>
    /// When called, (pressing the 'OPTIONS' button) it switches the canvas's to make the option-buttons apear.
    /// </summary>
    public void ToOptions()
    {
        print("Going To Options!");

        mainCamera.gameObject.transform.position = pos2.transform.position;
        mainCamera.gameObject.transform.rotation = Quaternion.Euler(90f, 21f, 0f);

        mainMenuCanvas.gameObject.SetActive(false);
        optionsMenuCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// When called, (pressing the 'PLAY' button) you enter the first level.
    /// </summary>
    public void Play()
    {
        print("Playing!");

        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// When called, (pressing the 'EXIT' button) the game exit's.  
    /// </summary>
    public void Exit()
    {
        print("Exiting...");

        Application.Quit();
    }
    #endregion

    #region OptionsMenu
    /// <summary>
    /// When called, (pressing the 'FULLSCREEN' button) it switches the games fullscreen option on and off.
    /// </summary>
    public void FullScreenSetting()
    {
        Screen.fullScreen = !Screen.fullScreen;

        print($"Changing FullScreen Settings To : {Screen.fullScreen}");
    }

    /// <summary>
    /// When called, (sliding the 'VOLUME' button) it changes the game's volume to the slider value. 
    /// </summary>
    public void SoundSetting()
    {
        AudioListener.volume = slider.value;
    }

    /// <summary>
    /// When called, (pressing the 'BACK' button) it switches the canvas's to make the main menu buttons apear.
    /// </summary>
    public void BackToMainMenu()
    {
        print("Going Back To Main Menu...");

        mainCamera.gameObject.transform.position = pos1.transform.position;
        mainCamera.gameObject.transform.rotation = Quaternion.Euler(11f, 21f, 0f);

        mainMenuCanvas.gameObject.SetActive(true);
        optionsMenuCanvas.gameObject.SetActive(false);
    }
    #endregion
}
