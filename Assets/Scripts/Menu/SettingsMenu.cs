using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsMenuUI;
    public GameObject pauseMenuButtonContainer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void LoadPauseMenu()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuButtonContainer.SetActive(true);
    }
}