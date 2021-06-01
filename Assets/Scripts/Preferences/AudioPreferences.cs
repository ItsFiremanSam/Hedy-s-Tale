using UnityEngine;

/// <summary>
/// Global preferences for audio volume, setting the properties will automatically persist
/// </summary>
public static class AudioPreferences
{
    private const string MasterVolumePrefKey = "MasterVolume";
    private const string MusicVolumePrefKey = "MusicVolume";
    private const string SFXVolumePrefKey = "SFXVolume";

    public static float MasterVolume
    {
        get => PlayerPrefs.GetFloat(MasterVolumePrefKey, -10);
        set => PlayerPrefs.SetFloat(MasterVolumePrefKey, value);
    }

    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat(MusicVolumePrefKey, -10);
        set => PlayerPrefs.SetFloat(MusicVolumePrefKey, value);
    }

    public static float SFXVolume
    {
        get => PlayerPrefs.GetFloat(SFXVolumePrefKey, -10);
        set => PlayerPrefs.SetFloat(SFXVolumePrefKey, value);
    }
}
