using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog
{
    public string[] names;
    public string[] sentences;
    public bool NPCTalkFirst;

    public string[] getDialog()
    {
        return sentences;
    }

    public string[] getNames()
    {
        return names;
    }

    public bool getNPCTalkFirst()
    {
        return NPCTalkFirst;
    }
}