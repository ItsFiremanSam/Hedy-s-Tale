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

    public override string ToString()
    {
        return "Keyword: "+ _isKeyword + ", Content: " + _content;
    }
}
