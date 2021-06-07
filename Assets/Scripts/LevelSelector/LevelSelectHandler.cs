using System;
using System.Linq;
using UnityEngine;

public class LevelSelectHandler : MonoBehaviour
{
    public int CurrentMaxLevel = 0;
    public Transform CloudContainer;

    [Range(0, 1)]
    public float HigherLevelTransparency = 0.5f;
    [Range(0, 1)]
    public float SameLevelTransparency = 0.2f;
    [Range(0, 1)]
    public float LowerLevelTransparency = 0;

    private const string CurrentMaxLevelPref = "currentMaxLevel";
    private const string ShowAnimationPref = "showAnimation";

    public static bool ShowAnimation
    {
        get => Convert.ToBoolean(PlayerPrefs.GetInt(ShowAnimationPref, 0));
        set => PlayerPrefs.SetInt(ShowAnimationPref, Convert.ToInt32(value));
    }

    private void Start()
    {
        if (!CloudContainer.gameObject.activeSelf)
            CloudContainer.gameObject.SetActive(true);

        CurrentMaxLevel = PlayerPrefs.GetInt(CurrentMaxLevelPref, 0);

        if (ShowAnimation)
        {
            HandleActiveClouds(CurrentMaxLevel - 1);
            GoToNextLevel();
        }
        else
        {
            HandleActiveClouds(CurrentMaxLevel);
        }
    }

    public void HandleActiveClouds(int showLevel)
    {
        LevelSignScript[] activeLevelSigns = GetComponentsInChildren<LevelSignScript>()
            .Where(item => item.transform.GetSiblingIndex() <= showLevel - 1)
            .ToArray();

        foreach (CloudScript cloud in CloudContainer.GetComponentsInChildren<CloudScript>())
        {
            if (CheckIfCloudInRadiusOfPreviousLevelSigns(cloud, activeLevelSigns))
            {
                cloud.gameObject.SetActive(false);
            }
            else
            {
                cloud.gameObject.SetActive(true);
                cloud.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        LevelSignScript[] levelSigns = GetComponentsInChildren<LevelSignScript>();
        for (int i = 0; i < levelSigns.Length; i++)
        {
            LevelSignScript levelSign = levelSigns[i];
            if (levelSign.IsLastChild()) 
                return;

            GizmosArrow.Draw(levelSign.transform.position, levelSigns[i + 1].transform.position, Color.red, 30, 20);
        }
    }

    public void GoToNextLevel()
    {
        ShowAnimation = false;

        if (CurrentMaxLevel - 1 >= transform.childCount)
            return;

        LevelSignScript currentLevelSign = GetComponentsInChildren<LevelSignScript>()[CurrentMaxLevel - 1];

        var disappearingClouds = CloudContainer.GetComponentsInChildren<CloudScript>()
            .Where(c => CheckIfCloudInRadiusOfLevelSign(c, currentLevelSign));

        foreach (CloudScript cloud in disappearingClouds)
            cloud.StartDisappearingAnimation(0.5f, UnityEngine.Random.Range(0, 360), 100);
    }

    private void OnValidate()
    {
        if (CurrentMaxLevel < 1) 
            CurrentMaxLevel = 1;

        if (CurrentMaxLevel > transform.childCount + 1) 
            CurrentMaxLevel = transform.childCount + 1;
    }

    public static void ShowNextLevelAnimation(int currentLevel)
    {
        if (PlayerPrefs.GetInt(CurrentMaxLevelPref, 1) <= currentLevel)
        {
            PlayerPrefs.SetInt(CurrentMaxLevelPref, currentLevel + 1);
            ShowAnimation = true;
        }
    }

    private static bool CheckIfCloudInRadiusOfPreviousLevelSigns(CloudScript cloud, LevelSignScript[] levelSigns)
    {
        foreach (LevelSignScript levelSign in levelSigns)
        {
            if (CheckIfCloudInRadiusOfLevelSign(cloud, levelSign))
                return true;
        }
        return false;
    }

    private static bool CheckIfCloudInRadiusOfLevelSign(CloudScript cloud, LevelSignScript levelSign)
    {
        return (cloud.GetComponent<RectTransform>().position - levelSign.GetComponent<RectTransform>().position).sqrMagnitude
            <= levelSign.RelativeCloudRadius * levelSign.RelativeCloudRadius;
    }
}
