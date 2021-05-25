using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        if (!CloudContainer.gameObject.activeSelf)
            CloudContainer.gameObject.SetActive(true);

        CurrentMaxLevel = PlayerPrefs.GetInt("currentMaxLevel", 0);

        bool showAnimation = Convert.ToBoolean(PlayerPrefs.GetInt("showAnimation", 0));
        if (showAnimation)
        {
            HandleActiveClouds(CurrentMaxLevel - 1);
            GoToNextLevel();
        } else
        {
            HandleActiveClouds(CurrentMaxLevel);
        }
    }

    public void HandleActiveClouds(int showLevel)
    {
        int[] temp =
            GetComponentsInChildren<LevelSignScript>().Select(item => item.transform.GetSiblingIndex()).ToArray();

        LevelSignScript[] activeLevelSigns =
            GetComponentsInChildren<LevelSignScript>()
            .Where(item => item.transform.GetSiblingIndex() <= showLevel - 1).ToArray();
        foreach (CloudScript cloud in CloudContainer.GetComponentsInChildren<CloudScript>())
        {
            if (LevelSignEditor.CheckIfCloudInRadiusOfPreviousLevelSigns(cloud, activeLevelSigns))
            {
                cloud.gameObject.SetActive(false);
            } else
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
            if (levelSign.IsLastChild()) return;

            GizmosArrow.Draw(levelSign.transform.position, levelSigns[i + 1].transform.position, Color.red, 30, 20);
        }
    }

    public void GoToNextLevel()
    {
        PlayerPrefs.SetInt("showAnimation", 0);
        if (CurrentMaxLevel - 1 >= transform.childCount)
            return;

        LevelSignScript currentLevelSign = GetComponentsInChildren<LevelSignScript>()[CurrentMaxLevel - 1];

        foreach (CloudScript cloud in CloudContainer.GetComponentsInChildren<CloudScript>()
            .Where(c => LevelSignEditor.CheckIfCloudInRadiusOfLevelSign(c, currentLevelSign)))
        {
            cloud.StartDissapearingAnimation(0.5f, UnityEngine.Random.Range(0, 360), 100);
        }
    }

    private void OnValidate()
    {
        if (CurrentMaxLevel < 1) CurrentMaxLevel = 1;
        if (CurrentMaxLevel > transform.childCount + 1) CurrentMaxLevel = transform.childCount + 1;
    }

    public static void ShowNextLevelAnimation(int currentLevel)
    {
        if (PlayerPrefs.GetInt("currentMaxLevel", 1) <= currentLevel)
        {
            PlayerPrefs.SetInt("currentMaxLevel", currentLevel + 1);
            PlayerPrefs.SetInt("showAnimation", 1);
        }
    }
}
