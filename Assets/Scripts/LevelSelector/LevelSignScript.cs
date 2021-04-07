using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelSignScript : MonoBehaviour
{
    private LevelSelectHandler _levelSelect;
    private Button _button;

    private void Awake()
    {
        _levelSelect = GetComponentInParent<LevelSelectHandler>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GoToLevel);
        // TODO: Add hover on buttons / more obvious level selection
    }

    public void GoToLevel()
    {
        if (_levelSelect.curMaxLevel < transform.GetSiblingIndex() + 1) return;
        // TODO: Do level transition
        Debug.Log($"Going to level: {name}");
    }

    public bool IsLastChild()
    {
        return transform.GetSiblingIndex() == transform.parent.childCount - 1;
    }
}