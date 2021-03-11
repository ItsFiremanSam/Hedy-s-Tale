using System.Collections;
using System.Collections.Generic;
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
    public bool ShowMoveGizmos = true;
    public bool ShowWaitOnGizmos = true;
    public bool ShowTalkGizmos = true;
    public bool ShowNonMovingGizmos = true;

    /// <summary>
    /// Draws the gizmos debug information to help with creating 
    /// </summary>
    private void OnDrawGizmos()
    {
        if (transform.parent.GetComponent<AnimationTrigger>().DrawGizmos && Subject != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector2 pos1 = GetLastPos(i, false);
                Vector2 pos2 = transform.GetChild(i).position;

                WaypointScript curWaypoint = transform.GetChild(i).GetComponent<WaypointScript>();

                if (!curWaypoint.noMoving)
                {
                    if (ShowMoveGizmos) GizmosArrow.Draw(pos1, pos2, Color.blue);
                }
                else
                {
                    if (ShowMoveGizmos)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawLine(GetLastPos(i, true), pos2);
                    }
                }

                if (curWaypoint.WaypointToWaitOn != null) GizmosArrow.Draw(curWaypoint.transform.position, curWaypoint.WaypointToWaitOn.transform.position, Color.red);

            }
            if (ShowMoveGizmos && ReturnToOrigin) GizmosArrow.Draw(GetLastPos(transform.childCount - 1, false), Subject.transform.position, Color.blue);
        }
    }

    /// <summary>
    /// Will get the previous position to use for debug drawing
    /// </summary>
    /// <param name="i">The childcount of the child that is being evaluated</param>
    /// <param name="canBeNoMoving">If false, will skip waypoints that dont include movement</param>
    /// <returns></returns>
    Vector2 GetLastPos(int i, bool canBeNoMoving)
    {
        int prevI = i;
        //if (i == 4)
        //{
        //    Debug.Log("I dont deserve love");
        //}
        do
        {
            prevI--;
        } while (!canBeNoMoving && prevI >= 0 && transform.GetChild(prevI).GetComponent<WaypointScript>().noMoving);

        if (prevI >= 0) return transform.GetChild(prevI).position;
        else
        {
            if (FromTrigger) return transform.parent.position;
            else return Subject.transform.position;
        }
    }
}
