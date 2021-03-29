using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPickUp : InteractableObject
{
    public PuzzleBlock PuzzleBlock;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        playerInteraction.GetInventory().Add(PuzzleBlock);

        Destroy(gameObject);
    }
}
