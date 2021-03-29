using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChest : InteractableObject
{
    public PuzzleBlock PuzzleBlock;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        // Is used to stop the interaction bubble from being displayed after it is activated.
        if (!_isInteracted)
        {
            playerInteraction.GetInventory().Add(PuzzleBlock);

            _isInteracted = true;
            ShowInteractionBubble(false);
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}