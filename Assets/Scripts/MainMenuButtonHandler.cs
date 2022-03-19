using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject pos1;
    [SerializeField] GameObject pos2;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject optionsMenuCanvas;

    [SerializeField] Slider slider;

    private void Start()
    {
        AudioListener.volume = slider.value;

        mainMenuCanvas.gameObject.SetActive(true);
        optionsMenuCanvas.gameObject.SetActive(false);
    }

    #region MainMenu

    public void ToOptions()
    {
        print("Going To Options!");

        mainCamera.gameObject.transform.position = pos2.transform.position;
        mainCamera.gameObject.transform.rotation = Quaternion.Euler(90f, 21f, 0f);

        mainMenuCanvas.gameObject.SetActive(false);
        optionsMenuCanvas.gameObject.SetActive(true);
    }

    public void Play()
    {
        print("Playing!");

        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        print("Exiting...");

        Application.Quit();
    }

    #endregion

    #region OptionsMenu

    public void FullScreenSetting()
    {
        Screen.fullScreen = !Screen.fullScreen;

        print($"Changing FullScreen Settings To : {Screen.fullScreen}");
    }

    public void SoundSetting()
    {
        AudioListener.volume = slider.value;
    }

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
