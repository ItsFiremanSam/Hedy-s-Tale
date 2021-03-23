using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPickUp : InteractableObject
{
    public override PuzzleBlock GetContent()
    {
        if (!_isInteracted)
        {
            _isInteracted = true;
            gameObject.SetActive(false);
            return new PuzzleBlock(_isKeyword, _content);
        }
        else
        {
            return null;
        }
    }
}
