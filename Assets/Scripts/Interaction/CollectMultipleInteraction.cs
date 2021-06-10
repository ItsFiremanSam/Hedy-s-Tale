using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMultipleInteraction : InteractableObject
{
    public PCollectMultiple collection;

    public AudioClip itemPickUpClip;
    public AudioSource sfxSource;

    public Dialog Dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        if (!_isInteracted)
        {
            _isInteracted = true;
            ShowInteractionBubble(false);
            sfxSource.PlayOneShot(itemPickUpClip);

            collection.CurrentAmount++;
            if (collection.CurrentAmount == collection.Amount)
            {
                playerInteraction.Inventory.Add(collection.PuzzleBlock);
            }

            DialogManager dialogManager = DialogManager.Instance;
            if (!dialogManager.DialogActive)
                StartCoroutine(dialogManager.StartDialog(Dialog, gameObject));
        }
    }
}
