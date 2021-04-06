using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelSignButton))]
public class PathEditor : Editor
{
    private LevelSignButton _levelSign;
    private Path _path;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add new point"))
        {
            Undo.RecordObject(_levelSign, "Add segment");
            _path.AddSegment(_path[_path.NumPoints - 1] + new Vector2(20, 0));
            Draw();
        }
        if (GUILayout.Button("Delete last point"))
        {
            Undo.RecordObject(_levelSign, "Delete last segment");
            _path.DeleteLastSegment();
            Draw();
        }
    }

    private void OnSceneGUI()
    {
        if (_levelSign == null) InitializePath();
        Draw();
    }

    private void Draw()
    {
        for (int i = 0; i < _path.NumSegments; i++)
        {
            Vector2[] points = _path.GetPointsInSegment(i);
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 5);
        }

        Handles.color = Color.red;
        for (int i = 0; i < _path.NumPoints; i++)
        {
            Vector2 newPos = Handles.FreeMoveHandle(_path[i], Quaternion.identity, 20, Vector2.zero, Handles.CylinderHandleCap);
            if (_path[i] != newPos)
            {
                Undo.RecordObject(_levelSign, "Move Point");
                _path.MovePoint(i, newPos);
            }
        }
    }

    private void InitializePath()
    {
        _levelSign = (LevelSignButton)target;
        if (_path != null) _levelSign.CreatePath();
        _path = _levelSign.Path;
    }

}