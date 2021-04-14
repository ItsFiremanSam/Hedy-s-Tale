using System;
using System.Collections;
using UnityEngine;


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