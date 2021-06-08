using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenuUI;
    public GameObject menuButtonContainer;
    public Button continueButton;

    private const string CurrentMaxLevelPref = "currentMaxLevel";

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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetCurrentMaxLevel()
    {
        PlayerPrefs.SetInt(CurrentMaxLevelPref, 0);
        LevelSelectHandler.ShowAnimation = false;
    }
}
