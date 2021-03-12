using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is responsible for a single step of an animation
/// </summary>
/// <remarks>
/// It can include a movement and other functionality, but maximum one of each
/// </remarks>
public class WaypointScript : MonoBehaviour
{
    // A waypoint this waypoint will wait on to be finished
    public WaypointScript WaypointToWaitOn;

    public TalkAction TalkAction;

    // This speed multiplier will be multiplied by the speed of the subject and the multiplier of the waypointcollection
    public float SpeedMultiplier = 1;

    // If true, this waypoint won't include movement
    // Can be used to create conversations
    public bool noMoving;

    bool _isDone = false;

    /// <summary>
    /// Is responsible for animating this waypoint
    /// </summary>
    /// <param name="subject">The subject this waypoints animates</param>
    /// <param name="speed">The combined speed of the subject with the speed multiplier of the waypoint collection</param>
    /// <returns></returns>
    public IEnumerator Animate(AnimatableEntity subject, float speed)
    {
        if (WaypointToWaitOn != null) while (!WaypointToWaitOn._isDone) yield return null;
        if (!noMoving) yield return subject.MoveTo(transform.position, speed * SpeedMultiplier);
        if (TalkAction.Seconds > 0) yield return subject.Speak(TalkAction);

        _isDone = true;
    }
}

[Serializable]
public struct TalkAction
{
    public string Message;
    public float Seconds;
    public TalkAction(string message, float seconds)
    {
        Message = message;
        Seconds = seconds;
    }
}
