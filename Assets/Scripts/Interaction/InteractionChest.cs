﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChest : InteractableObject
{
    public override PuzzleBlock GetContent()
    {
        if (!_isInteracted)
        {
            _isInteracted = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            return new PuzzleBlock(_isKeyword, _content);
        }
        else
        {
            return null;
        }
    }
}
