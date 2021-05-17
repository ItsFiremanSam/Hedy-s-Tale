using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelSelectHandler : MonoBehaviour
{
    public int curMaxLevel = 0;
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

        HandleActiveClouds();
    }

    public void HandleActiveClouds()
    {
        int[] temp =
            GetComponentsInChildren<LevelSignScript>().Select(item => item.transform.GetSiblingIndex()).ToArray();
        LevelSignScript[] activeLevelSigns =
            GetComponentsInChildren<LevelSignScript>()
            .Where(item => item.transform.GetSiblingIndex() <= curMaxLevel).ToArray();
        foreach (CloudScript cloud in CloudContainer.GetComponentsInChildren<CloudScript>())
        {
            if (LevelSignEditor.CheckIfCloudInRadiusOfPreviousLevelSigns(cloud, activeLevelSigns))
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
            if (levelSign.IsLastChild()) return;

            GizmosArrow.Draw(levelSign.transform.position, levelSigns[i + 1].transform.position, Color.red, 30, 20);
        }
    }

    public void GoToNextLevel()
    {
        if (curMaxLevel >= transform.childCount) return;

        curMaxLevel++;
        LevelSignScript currentLevelSign = GetComponentsInChildren<LevelSignScript>()[curMaxLevel];

        foreach (CloudScript cloud in CloudContainer.GetComponentsInChildren<CloudScript>()
            .Where(c => LevelSignEditor.CheckIfCloudInRadiusOfLevelSign(c, currentLevelSign)))
        {
            cloud.StartDissapearingAnimation(0.5f, Random.Range(0, 360), 100);
        }
    }

    private void OnValidate()
    {
        if (curMaxLevel < 0) curMaxLevel = 0;
        if (curMaxLevel > transform.childCount) curMaxLevel = transform.childCount;
    }
}
