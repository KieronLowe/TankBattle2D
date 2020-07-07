using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public Text[] ControllerText;
    public EventSystem PauseEventSystem;
    public Toggle FullScreenToggle;
    public GameObject PauseMenuUI;
    public static bool IsGamePaused = false;
    public float PauseMenuInput = 0;

    private GameObject storedSelcted;
    private string[] controllerNames;

    void Start()
    {
        storedSelcted = PauseEventSystem.currentSelectedGameObject;
        if (Screen.fullScreen)
            FullScreenToggle.isOn = true;
        else
            FullScreenToggle.isOn = false;
    }

    void Update()
    {
        getPlayerInput();
        if (PauseMenuInput == 1 && !IsGamePaused)
            Paused();

        if (PauseEventSystem.currentSelectedGameObject == null)
            PauseEventSystem.SetSelectedGameObject(storedSelcted);
        else
            storedSelcted = PauseEventSystem.currentSelectedGameObject;

        controllerDetection();
    }

    private void controllerDetection()
    {
        controllerNames = Input.GetJoystickNames();
        if (controllerNames.Length > 0)
        {
            for (int i = 0; i < controllerNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(controllerNames[i]))
                    ControllerText[i].text = "P" + (i + 1).ToString() + ": " + controllerNames[i];
                else
                    ControllerText[i].text = "P" + (i + 1).ToString() + ": controller not detected";
            }
        }
    }

    public void SetSelectedGameObject(GameObject button)
    {
        PauseEventSystem.SetSelectedGameObject(button);
        storedSelcted = button;
    }

    private void getPlayerInput()
    {
        PauseMenuInput = Input.GetAxisRaw("PauseMenu");
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public void Paused()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void QuitGameToMenu()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGameToDesktop()
    {
        Application.Quit();
    }

    public void SetFullScreen()
    {
        if (FullScreenToggle.isOn)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }

    public void ButtonPushSound()
    {
        AudioManager.PlaySoundEffect("ButtonPush");
    }
}
