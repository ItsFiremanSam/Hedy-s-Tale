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
    public PuzzleBlockType Type;
    public string Content;

    [TextArea(1, 4)]
    public string Explanation;

    public PuzzleBlock(PuzzleBlock otherBlock)
    {
        Type = otherBlock.Type;
        Content = otherBlock.Content;
    }

    public PuzzleBlock(PuzzleBlockType type, string content, string explanation)
    {
        Type = type;
        Content = content;
        Explanation = explanation;
    }

    public bool Equals(PuzzleBlock otherBlock)
    {
        if (Type == PuzzleBlockType.Undefined)
            Debug.LogWarning("Puzzle block '" + Content + "' has type of Undefined");
        if (otherBlock.Type == PuzzleBlockType.Undefined)
            Debug.LogWarning("Puzzle block '" + otherBlock.Content + "' has type of Undefined");

        return Type == otherBlock.Type && otherBlock.Content == Content;
    }
}
