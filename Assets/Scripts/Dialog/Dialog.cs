using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog
{
    public string NPCName;
    public string[] sentences;
    public bool NPCTalkFirst;

    public string[] getDialog()
    {
        return sentences;
    }

    public bool getNPCTalkFirst()
    {
        return NPCTalkFirst;
    }
}