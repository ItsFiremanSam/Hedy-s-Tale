using UnityEditor;
using UnityEngine;

public static class GizmosCircle
{
    public static void Draw(Vector2 center, float radius, Color color, int segments = 32)
    {
        Gizmos.color = color;
        float angle = 0;
        Vector2[] points = new Vector2[segments + 1];
        points[0] = center + new Vector2(0, radius);
        for (int i = 1; i < segments + 2; i++)
        {
            if (i < segments + 1)
                points[i] = center + new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle) * radius, Mathf.Cos(Mathf.Deg2Rad * angle) * radius);
            Gizmos.DrawLine(points[i - 1], points[i % segments]);

            angle += (360f / (segments - 1));
        }
    }
}