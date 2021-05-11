using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelSelectHandler))]
public class LevelSelectHandlerEditor : Editor
{
    /// <summary>
    /// For debugging only
    /// </summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Go to next Level"))
        {
            ((LevelSelectHandler)target).GoToNextLevel();
        }
    }
}