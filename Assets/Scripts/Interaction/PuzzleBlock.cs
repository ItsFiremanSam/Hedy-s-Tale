using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The reason for puting the enum in this file is because this enum will only be used by the PuzzleBlock,
// which itself is only a container class without much implementation.
public enum PuzzleBlockType
{
    Undefined, // The Undefined type is used as a default so the IsKeyword is used whenever type is set to Undefined
    Keyword,
    String,
    Variable,
    Number,
    None, // The None type is used in the Coding UI to support punctuation 
}

[Serializable]
public class PuzzleBlock : IEquatable<PuzzleBlock>
{
    [Obsolete]
    [Header("The IsKeyword is obsolete, it is only used if type is set to Undefined")]
    public bool IsKeyword;
    public PuzzleBlockType Type;
    public string Content;

    public PuzzleBlock(PuzzleBlock otherBlock)
    {
        IsKeyword = otherBlock.IsKeyword;
        Type = otherBlock.Type;
        Content = otherBlock.Content;
    }

    public PuzzleBlock(bool isKeyword, PuzzleBlockType type, string content)
    {
        IsKeyword = isKeyword;
        Type = type;
        Content = content;
    }

    public PuzzleBlock(PuzzleBlockType type, string content)
    {
        Type = type;
        Content = content;
    }

    public bool Equals(PuzzleBlock otherBlock)
    {
        if (otherBlock.Content != Content)
            return false;

        // Check when this uses obsolete IsKeyword
        if (Type == PuzzleBlockType.Undefined)
        {
            if (otherBlock.Type == PuzzleBlockType.Undefined)
                return IsKeyword == otherBlock.IsKeyword;

            return (IsKeyword && otherBlock.Type == PuzzleBlockType.Keyword) || (!IsKeyword && otherBlock.Type != PuzzleBlockType.Keyword);
        }

        // Check when otherBlock uses obsolete IsKeyword
        if (otherBlock.Type == PuzzleBlockType.Undefined)
        {
            return (otherBlock.IsKeyword && Type == PuzzleBlockType.Keyword) || (!otherBlock.IsKeyword && Type != PuzzleBlockType.Keyword);
        }

        return Type == otherBlock.Type;
    }
}
