using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This script is a parent for waypoints for a single subject in an animation
/// It is also responsible for drawing the debug information for its waypoints
/// </summary>
public class WaypointCollectionScript : MonoBehaviour
{
    public AnimatableEntity Subject;

    // This multiplier is multiplied with the original speed of the subject
    public float SpeedMultiplier = 1;

    // If true, the subject will return to its original position at the start of the animation
    public bool ReturnToOrigin;

    // Variables that don't influence the functionality
    [Header("Debug tools")]
    // Make this true if the subject will start its position from the animation trigger
    // (Probably only used if the subject is the player itself)
    public bool FromTrigger;

    /// <summary>
    /// Draws the gizmos debug information to help with creating 
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!transform.parent) return;
        AnimationTrigger trigger = transform.parent.GetComponent<AnimationTrigger>();
        WaypointScript[] waypointScripts = GetComponentsInChildren<WaypointScript>();
        if (!trigger || !trigger.DrawGizmos || waypointScripts.Length == 0) return;

        Transform firstTransform = (Subject && !FromTrigger) ? Subject.transform : trigger.transform;
        waypointScripts[0].DrawGizmos(firstTransform);
        for (int i = 1; i < waypointScripts.Length; i++)
        {
            waypointScripts[i].DrawGizmos(waypointScripts[i - 1].transform);
        }
        if (ReturnToOrigin) GizmosArrow.Draw(waypointScripts[waypointScripts.Length - 1].transform.position, firstTransform.position, Color.blue);
    }
}
