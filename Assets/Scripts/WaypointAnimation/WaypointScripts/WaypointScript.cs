using System.Collections;
using UnityEngine;

public abstract class WaypointScript : MonoBehaviour
{
    public WaypointScript WaypointToWaitOn;

    [HideInInspector]
    public bool IsDone = false;

    public IEnumerator Animate(AnimatableEntity subject, float speed)
    {
        if (WaypointToWaitOn != null) while (!WaypointToWaitOn.IsDone) yield return null;
        yield return AnimateInstance(subject, speed);
    }

    protected abstract IEnumerator AnimateInstance(AnimatableEntity subject, float speed);

    public void DrawGizmos(Transform prevWaypoint)
    {
        if (WaypointToWaitOn)
            GizmosArrow.Draw(WaypointToWaitOn.transform.position, transform.position, Color.black);
        DrawGizmosInstance(prevWaypoint);
    }
    protected abstract void DrawGizmosInstance(Transform prevWaypoint);
}
