using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelSignScript : MonoBehaviour
{
    private LevelSelectHandler _levelSelect;
    private Button _button;
    public float CloudRadius = 100;

    private void Awake()
    {
        _levelSelect = GetComponentInParent<LevelSelectHandler>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GoToLevel);
        // TODO: Add hover on buttons / more obvious level selection
    }

    private void OnDrawGizmos()
    {
        GizmosCircle.Draw(transform.position, CloudRadius, Color.green);
    }

    public void GoToLevel()
    {
        if (_levelSelect.curMaxLevel < transform.GetSiblingIndex()) return;
        // TODO: Do level transition
        Debug.Log($"Going to level: {name}");
    }

    public bool IsLastChild()
    {
        return transform.GetSiblingIndex() == transform.parent.childCount - 1;
    }
}