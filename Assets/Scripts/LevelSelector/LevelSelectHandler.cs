using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if (!CloudContainer.gameObject.activeSelf)
            CloudContainer.gameObject.SetActive(true);

        foreach (CloudScript cloudScript in CloudContainer.GetComponentsInChildren<CloudScript>())
        {
            if (cloudScript.dissapearLevel <= curMaxLevel) cloudScript.gameObject.SetActive(false);
            else cloudScript.gameObject.SetActive(true);

            cloudScript.GetComponent<CanvasGroup>().alpha = 1;
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

    // TODO: Turn level selecting off during animation
    public void GoToNextLevel()
    {
        if (curMaxLevel > transform.childCount - 1) return;

        curMaxLevel++;
        CloudScript[] cloudsToAnimate = 
            CloudContainer.GetComponentsInChildren<CloudScript>().Where(c => c.dissapearLevel == curMaxLevel).ToArray();

        foreach (CloudScript cloudScript in cloudsToAnimate)
            cloudScript.StartDissapearingAnimation(0.5f, Random.Range(0, 360), 100);
    }

    private void OnValidate()
    {
        if (curMaxLevel < 0) curMaxLevel = 0;
        if (curMaxLevel > transform.childCount) curMaxLevel = transform.childCount;
    }
}
