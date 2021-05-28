using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsMenuUI;
    public GameObject pauseMenuButtonContainer;

    private const string MasterVolume = "MasterVolume";

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(MasterVolume, volume);
    }

    public void LoadPauseMenu()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuButtonContainer.SetActive(true);
    }
}
