using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlock
{
    private bool _isKeyword;
    private string _content;

    public PuzzleBlock(bool isKeyword, string content)
    {
        _isKeyword = isKeyword;
        _content = content;
    }

    public bool IsKeyword()
    {
        return _isKeyword;
    }

    public string GetContent()
    {
        return _content;
    }

    public override string ToString()
    {
        return "Keyword: "+ _isKeyword + ", Content: " + _content;
    }

    public bool Equals(PuzzleBlock otherBlock)
    {
        return otherBlock._isKeyword == _isKeyword && otherBlock._content == _content;
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
