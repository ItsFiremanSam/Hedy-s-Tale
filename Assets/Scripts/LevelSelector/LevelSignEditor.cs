using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelSignScript))]
public class LevelSignEditor : Editor
{

    private void OnSceneGUI()
    {
        Draw((LevelSignScript)target);
    }

    private void Draw(LevelSignScript levelSign)
    {
        LevelSelectHandler levelSelect = levelSign.GetComponentInParent<LevelSelectHandler>();
        Transform cloudContainer = levelSelect.CloudContainer;
        if (!cloudContainer.gameObject.activeSelf) cloudContainer.gameObject.SetActive(true);

        int curLevel = levelSign.transform.GetSiblingIndex() + 1;
        foreach (CloudScript cloud in cloudContainer.GetComponentsInChildren<CloudScript>())
        {
            CanvasGroup cloudCanvasGroup = cloud.GetComponent<CanvasGroup>();
            if (cloud.dissapearLevel < curLevel)
            {
                cloudCanvasGroup.alpha = levelSelect.LowerLevelTransparency;
                continue;
            }
            if (cloud.dissapearLevel > curLevel)
            {
                cloudCanvasGroup.alpha = levelSelect.HigherLevelTransparency;
                Handles.color = Color.green;
                if (Handles.Button(cloud.transform.position, Quaternion.identity, 20, 20, Handles.RectangleHandleCap))
                {
                    Undo.RecordObject(target, "Set cloud level");
                    cloud.dissapearLevel = curLevel;
                }
            }
            else
            {
                cloudCanvasGroup.alpha = levelSelect.SameLevelTransparency;
                Handles.color = Color.red;
                if (Handles.Button(cloud.transform.position, Quaternion.identity, 20, 20, Handles.RectangleHandleCap))
                {
                    Undo.RecordObject(target, "Reset cloud level");
                    cloud.dissapearLevel = int.MaxValue;
                }
            }
        }
    }
}