using System.Collections;
using UnityEditor;
using UnityEngine;

public class LevelSignButton : MonoBehaviour
{
    [HideInInspector]
    public Path Path;

    public void CreatePath()
    {
        Path = new Path(transform.position);
    }
}