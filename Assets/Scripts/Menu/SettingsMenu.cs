using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject settingsMenuUI;
    public GameObject pauseMenuButtonContainer;

    /// <summary>
    /// The name of the exposed property of the master mixer
    /// </summary>
    private const string MasterVolume = "MasterVolume";

    private void Start()
    {
        // Restore the volume from global settings
        audioMixer.SetFloat(MasterVolume, AudioPreferences.MasterVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(MasterVolume, volume);
        AudioPreferences.MasterVolume = volume;
    }

    public void LoadPauseMenu()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuButtonContainer.SetActive(true);
    }
}
