using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleBlock : IEquatable<PuzzleBlock>
{
    public bool IsKeyword;
    public string Content;

    public PuzzleBlock(PuzzleBlock otherBlock)
    {
        IsKeyword = otherBlock.IsKeyword;
        Content = otherBlock.Content;
    }

    public PuzzleBlock(bool isKeyword, string content)
    {
        IsKeyword = isKeyword;
        Content = content;
    }

    public bool Equals(PuzzleBlock otherBlock)
    {
        return otherBlock.IsKeyword == IsKeyword && otherBlock.Content == Content;
    }
}
