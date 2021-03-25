using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChest : InteractableObject
{
    private bool _isInteracted;

    public override PuzzleBlock OnInteractWithPlayer()
    {
        if (!_isInteracted)
        {
            _isInteracted = true;
            SetIsInteracted(true);
            ShowInteractionBubble(false);
            GetComponent<SpriteRenderer>().color = Color.red;
            return new PuzzleBlock(_puzzleBlockIsKeyword, _puzzleBlockContent);
        }
        else
        {
            return null;
        }
    }
}