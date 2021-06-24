using System.Collections;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This waypoint destroy an GameObject
/// </summary>
public class ActivateObjectWaypointScript : WaypointScript
{
    public GameObject ObjectToActivate;

    protected override IEnumerator AnimateInstance(AnimatableEntity subject, float speed)
    {
        ObjectToActivate.SetActive(true);
        yield return null;
    }

    protected override void DrawGizmosInstance(Transform prevWaypoint)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(prevWaypoint.position, transform.position);
        if (ObjectToActivate)
        {
            Gizmos.color = Color.red;
            Vector2 destroyPosition = ObjectToActivate.transform.position;

            Gizmos.DrawLine(transform.position, destroyPosition);

            Vector2 v0 = new Vector2(0.5f, 0.5f);
            Vector2 v1 = new Vector2(0.5f, -0.5f);

            Gizmos.DrawLine(destroyPosition - v0, destroyPosition + v0);
            Gizmos.DrawLine(destroyPosition - v1, destroyPosition + v1);
        }
    }
}
