using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider, musicVolumeSlider, SFXVolumeSlider;
    public GameObject settingsMenuUI;
    public GameObject menuButtonContainer;

    /// <summary>
    /// The name of the exposed property of the master mixer
    /// </summary>
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    private const string SFXVolume = "SFXVolume";

    private void Start()
    {
        // Restore the volumes from global settings
        audioMixer.SetFloat(MasterVolume, AudioPreferences.MasterVolume);
        masterVolumeSlider.SetValueWithoutNotify(AudioPreferences.MasterVolume);

        audioMixer.SetFloat(MusicVolume, AudioPreferences.MusicVolume);
        musicVolumeSlider.SetValueWithoutNotify(AudioPreferences.MusicVolume);

        audioMixer.SetFloat(SFXVolume, AudioPreferences.SFXVolume);
        SFXVolumeSlider.SetValueWithoutNotify(AudioPreferences.SFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(MasterVolume, volume);
        AudioPreferences.MasterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MusicVolume, volume);
        AudioPreferences.MusicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFXVolume, volume);
        AudioPreferences.SFXVolume = volume;
    }

    public void LoadMenu()
    {
        settingsMenuUI.SetActive(false);
        menuButtonContainer.SetActive(true);
    }
}
