using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog
{
    public string NPCName;
    [TextArea(1,4)]
    public List<string> sentences = new List<string>();
    public bool NPCTalkFirst;
}
