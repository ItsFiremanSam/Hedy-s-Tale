using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChest : InteractableObject
{
    public PuzzleBlock PuzzleBlock;
    public Animator openChestAnimator;
    public Animator codeBlockAnimator;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        if (!_isInteracted)
        {
            playerInteraction.Inventory.Add(PuzzleBlock);

            _isInteracted = true;
            ShowInteractionBubble(false);
            openChestAnimator.SetBool("isInteracted", true);
            codeBlockAnimator.SetBool("isChestOpened", true);
        }
    }
}