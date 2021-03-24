using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPickUp : InteractableObject
{
    private bool _isInteracted;
    public override PuzzleBlock OnInteractWithPlayer()
    {
        if (!_isInteracted)
        {
            _isInteracted = true;
            gameObject.SetActive(false);
            return new PuzzleBlock(_puzzleBlockIsKeyword, _puzzleBlockContent);
        }
        else
        {
            return null;
        }
    }
}
