using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void LoadPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
}