using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMultipleInteraction : InteractableObject
{
    public PCollectMultiple collection;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        collection.CurrentAmount++;
        if (collection.CurrentAmount == collection.Amount)
        {
                playerInteraction.Inventory.Add(collection.PuzzleBlock);
        }

        Destroy(gameObject);
    }
}
