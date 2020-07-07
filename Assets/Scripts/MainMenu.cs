using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public Text[] ControllerText;
    public EventSystem MMEventSystem;
    public Toggle FullScreenToggle;
    public static bool IsGameOnePlayer = false;

    private GameObject storedSelcted;
    private string[] controllerNames;

    void Start()
    {
        Screen.fullScreen = true;
        storedSelcted = MMEventSystem.currentSelectedGameObject;
    }

    void Update()
    {
        controllerDetection();
        if (MMEventSystem.currentSelectedGameObject == null)
            MMEventSystem.SetSelectedGameObject(storedSelcted);
        else
            storedSelcted = MMEventSystem.currentSelectedGameObject;
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
        MMEventSystem.SetSelectedGameObject(button);
        storedSelcted = button;
    }

    public void OnePlayerPlay()
    {
        Score.ResetScores();
        IsGameOnePlayer = true;
        SceneManager.LoadScene("OnePlayerGame");
    }

    public void TwoPlayerPlay()
    {
        Score.ResetScores();
        IsGameOnePlayer = false;
        SceneManager.LoadScene("TwoPlayerGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonPushSound()
    {
        AudioManager.PlaySoundEffect("ButtonPush");
    }

    public void SetFullScreen()
    {
        if (FullScreenToggle.isOn)
            Screen.fullScreen = true;
        else 
            Screen.fullScreen = false;
    }

}
