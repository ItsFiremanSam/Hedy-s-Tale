using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPickUp : InteractableObject
{
    public PuzzleBlock PuzzleBlock;

    public AudioClip itemPickUpClip;
    public AudioSource sfxSource;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        sfxSource.PlayOneShot(itemPickUpClip);

        playerInteraction.Inventory.Add(PuzzleBlock);

        Destroy(gameObject);
    }
}
