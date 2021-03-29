using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleBlock
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

    public static bool IsCorrectAnswer(List<PuzzleBlock> correctAnswer, List<PuzzleBlock> answerToCheck)
    {
        for (int i = 0; i < answerToCheck.Count; i++)
        {
            if (!correctAnswer[i].Equals(answerToCheck[i]))
            {
                return false;
            }
        }
        return true;
    }
}
