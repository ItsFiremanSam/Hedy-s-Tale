using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleBlock
{
    public bool IsKeyword;
    public string Content;

    public PuzzleBlock(bool isKeyword, string content)
    {
        IsKeyword = isKeyword;
        Content = content;
    }
}
