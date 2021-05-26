using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSignScript : MonoBehaviour
{
    private LevelSelectHandler _levelSelect;
    private Button _button;
    private Canvas _canvas;

    [SerializeField]
    private float _cloudRadius = 100;
    public float RelativeCloudRadius
    {
        get
        {
            return _cloudRadius * _canvas.scaleFactor;
        }
    }

    private void Awake()
    {
        _levelSelect = GetComponentInParent<LevelSelectHandler>();
        _button = GetComponent<Button>();
        _canvas = GetComponentInParent<Canvas>();
    }

    private void OnDrawGizmos()
    {
        if (!_canvas)
        {
            _canvas = GetComponentInParent<Canvas>();
        }
        GizmosCircle.Draw(transform.position, RelativeCloudRadius, Color.green);
    }

    public bool IsLastChild()
    {
        return transform.GetSiblingIndex() == transform.parent.childCount - 1;
    }

    public void OnTransitionLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}