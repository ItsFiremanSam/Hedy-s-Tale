using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelSignScript))]
public class LevelSignEditor : Editor
{

    private void OnSceneGUI()
    {
        if (!Application.isPlaying)
        {
            Draw();
        }
    }

    /// <summary>
    /// This function is used for debuggin which clouds will dissapear for every levelsign when selected
    /// </summary>
    private void Draw()
    {
        LevelSignScript levelSign = (LevelSignScript)target;
        LevelSelectHandler levelSelect = levelSign.GetComponentInParent<LevelSelectHandler>();
        if (!levelSelect) return;
        
        Transform cloudContainer = levelSelect.CloudContainer;
        if (!cloudContainer.gameObject.activeSelf) cloudContainer.gameObject.SetActive(true);

        int curSiblingIndex = levelSign.transform.GetSiblingIndex();
        LevelSignScript[] previousLevelSigns =
            levelSelect.GetComponentsInChildren<LevelSignScript>()
            .Where(item => item.transform.GetSiblingIndex() < curSiblingIndex)
            .ToArray();

        foreach (CloudScript cloud in cloudContainer.GetComponentsInChildren<CloudScript>())
        {
            CanvasGroup cloudCanvasGroup = cloud.GetComponent<CanvasGroup>();
            if (CheckIfCloudInRadiusOfPreviousLevelSigns(cloud, previousLevelSigns))
            {
                cloudCanvasGroup.alpha = levelSelect.LowerLevelTransparency;
            }
            else if (CheckIfCloudInRadiusOfLevelSign(cloud, levelSign))
            {
                cloudCanvasGroup.alpha = levelSelect.SameLevelTransparency;
            }
            else
            {
                cloudCanvasGroup.alpha = levelSelect.HigherLevelTransparency;
            }
        }
    }

    public static bool CheckIfCloudInRadiusOfPreviousLevelSigns(CloudScript cloud, LevelSignScript[] levelSigns) 
    {
        foreach (LevelSignScript levelSign in levelSigns)
        {
            if (CheckIfCloudInRadiusOfLevelSign(cloud, levelSign)) 
                return true;
        }
        return false;
    }

    public static bool CheckIfCloudInRadiusOfLevelSign(CloudScript cloud, LevelSignScript levelSign)
    {
        return (cloud.GetComponent<RectTransform>().position - levelSign.GetComponent<RectTransform>().position).sqrMagnitude 
            <= levelSign.CloudRadius * levelSign.CloudRadius;
    }
}