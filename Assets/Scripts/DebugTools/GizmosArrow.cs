using UnityEngine;

public static class GizmosArrow
{
    /// <summary>
    /// Draws an arrow using gizmos pointing from pos1 towards pos2
    /// </summary>
    /// <remarks>
    /// Will not draw anything if attempting to draw an arrow from and towards the same position
    /// SOURCE: https://forum.unity.com/threads/debug-drawarrow.85980/
    ///     Changed to be used for 2d
    /// </remarks>
    public static void Draw(Vector2 pos1, Vector2 pos2, Color color)
    {
        if (pos1 != pos2)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(pos1, pos2);

            Gizmos.DrawRay(pos2, Quaternion.LookRotation(pos2 - pos1) * Quaternion.Euler(180 + 20, 0, 0) * new Vector3(0, 0, 1) * 0.5f);
            Gizmos.DrawRay(pos2, Quaternion.LookRotation(pos2 - pos1) * Quaternion.Euler(180 - 20, 0, 0) * new Vector3(0, 0, 1) * 0.5f);
        }
    }
}

