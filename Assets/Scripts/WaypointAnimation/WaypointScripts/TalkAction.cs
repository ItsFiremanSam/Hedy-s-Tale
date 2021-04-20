using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Used as a container for a talk action in waypoint animations
/// </summary>
[Serializable]
public class TalkAction
{
    public string Message;
    public float Seconds;
    public TalkAction(string message, float seconds)
    {
        Message = message;
        Seconds = seconds;
    }
}