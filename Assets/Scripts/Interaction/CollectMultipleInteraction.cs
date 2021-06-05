using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMultipleInteraction : InteractableObject
{
    public PCollectMultiple collection;

    public AudioClip itemPickUpClip;
    public AudioSource sfxSource;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        sfxSource.PlayOneShot(itemPickUpClip);

        collection.CurrentAmount++;
        if (collection.CurrentAmount == collection.Amount)
        {
                playerInteraction.Inventory.Add(collection.PuzzleBlock);
        }

        Destroy(gameObject);
    }
}
