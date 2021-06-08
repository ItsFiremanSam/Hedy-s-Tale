using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenuUI;
    public GameObject menuButtonContainer;
    public GameObject confirmationContainer;
    public SceneChanger sceneChanger;
    public Button continueButton;

    private const string CurrentMaxLevelPref = "currentMaxLevel";
    private const string OptionNewGame = "NewGame";
    private const string OptionQuitGame = "QuitGame";

    private void Start()
    {
        if (PlayerPrefs.HasKey(CurrentMaxLevelPref) && PlayerPrefs.GetInt(CurrentMaxLevelPref) > 0)
        {
            continueButton.interactable = true;
        }
    }

    public void LoadSettingsMenu()
    {
        menuButtonContainer.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void OpenConfirmationMenu(string option)
    {
        menuButtonContainer.SetActive(false);
        confirmationContainer.SetActive(true);

        GameObject yesButton = confirmationContainer.transform.GetChild(1).gameObject;

        switch (option)
        {
            case OptionNewGame:
                yesButton.GetComponent<Button>().onClick.AddListener(RestartGameAtAnimation);
                yesButton.GetComponent<Button>().onClick.AddListener(ResetCurrentMaxLevel);
                break;
            case OptionQuitGame:
                yesButton.GetComponent<Button>().onClick.AddListener(QuitGame);
                break;
            default:
                Debug.LogError("Your entered option does not match any of the available options!");
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGameAtAnimation()
    {
        sceneChanger.ChangeScene("FirstCutscene-Fireworks");
    }

    public void ResetCurrentMaxLevel()
    {
        PlayerPrefs.SetInt(CurrentMaxLevelPref, 0);
        LevelSelectHandler.ShowAnimation = false;
    }

    public void No()
    {
        GameObject yesButton = confirmationContainer.transform.GetChild(1).gameObject;
        yesButton.GetComponent<Button>().onClick.RemoveAllListeners();

        confirmationContainer.SetActive(false);
        menuButtonContainer.SetActive(true);
    }
}
