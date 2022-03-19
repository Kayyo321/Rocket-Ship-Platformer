using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject pos1;
    [SerializeField] GameObject pos2;

    [SerializeField] CanvasGroup mainMenuCanvas;
    [SerializeField] CanvasGroup optionsMenuCanvas;

    [SerializeField] TextMeshProUGUI soundText;

    public float volume;

    private void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        optionsMenuCanvas.gameObject.SetActive(false);

        volume = 2;
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

    public void SoundSettingUp()
    {
        if (volume < 2)
        {
            volume += 0.5f;

            soundText.text = $"Volume : {volume * 4}0%";
            AudioListener.volume = volume;
        }
    }

    public void SoundSettingDown()
    {
        if (volume > 0)
        {
            volume -= 0.5f;

            soundText.text = $"Volume : {volume * 4}0%";
            AudioListener.volume = volume;
        }
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
