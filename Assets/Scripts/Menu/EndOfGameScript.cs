using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGameScript : MonoBehaviour
{
    public GameObject menuButtonContainer;
    public GameObject confirmationContainer;

    private const string OptionMainMenu = "MainMenu";
    private const string OptionQuitGame = "QuitGame";

    public void OpenConfirmationMenu(string option)
    {
        menuButtonContainer.SetActive(false);
        confirmationContainer.SetActive(true);

    GameObject yesButton = confirmationContainer.transform.GetChild(1).gameObject;

        switch (option)
        {
            case OptionMainMenu:
                yesButton.GetComponent<Button>().onClick.AddListener(LoadMainMenu);
                break;
            case OptionQuitGame:
                yesButton.GetComponent<Button>().onClick.AddListener(QuitGame);
                break;
            default:
                Debug.LogError("Your entered option does not match any of the available options!");
                break;
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void No()
    {
        GameObject yesButton = confirmationContainer.transform.GetChild(1).gameObject;
        yesButton.GetComponent<Button>().onClick.RemoveAllListeners();

        confirmationContainer.SetActive(false);
        menuButtonContainer.SetActive(true);
    }

}
