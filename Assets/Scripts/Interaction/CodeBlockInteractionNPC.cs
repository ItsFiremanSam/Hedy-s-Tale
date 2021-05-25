using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBlockInteractionNPC : InteractableObject
{
    public PuzzleBlock PuzzleBlock;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        if (!_isInteracted)
        {
            playerInteraction.Inventory.Add(PuzzleBlock);

            _isInteracted = true;
            ShowInteractionBubble(false);            
        }
    }
}