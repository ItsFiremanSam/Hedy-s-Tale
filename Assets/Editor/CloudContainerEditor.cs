using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CloudContainerScript))]
public class CloudContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Show all clouds"))
        {
            foreach (CloudScript cloudScript in ((CloudContainerScript) target).GetComponentsInChildren<CloudScript>())
            {
                cloudScript.gameObject.SetActive(true);
                cloudScript.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
    }
}