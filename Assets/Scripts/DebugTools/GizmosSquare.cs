using System.Collections;
using UnityEngine;

public static class GizmosSquare
{
    public static void Draw(float x0, float y0, float x1, float y1, Color color)
    {
        Gizmos.color = color;

        Vector2 c00 = new Vector2(x0, y0);
        Vector2 c01 = new Vector2(x0, y1);
        Vector2 c10 = new Vector2(x1, y0);
        Vector2 c11 = new Vector2(x1, y1);
        Gizmos.DrawLine(c00, c01);
        Gizmos.DrawLine(c01, c11);
        Gizmos.DrawLine(c11, c10);
        Gizmos.DrawLine(c10, c00);
    }

    public static void Draw(Rect rect, Color color)
    {
        Draw(rect.x, rect.y, rect.x + rect.width, rect.height, color);
    }

    public static void Draw(Transform transform, float width, float height, Color color)
    {
        Vector3 position = transform.position;
        Draw(position.x - width / 2, position.y - height / 2, position.x + width / 2, position.y + height / 2, color);
    }
}
