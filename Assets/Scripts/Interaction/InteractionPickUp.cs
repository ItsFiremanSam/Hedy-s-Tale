using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPickUp : InteractableObject
{
    public PuzzleBlock PuzzleBlock;

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

            playerInteraction.Inventory.Add(PuzzleBlock);

            DialogManager dialogManager = DialogManager.Instance;
            if (!dialogManager.DialogActive)
                StartCoroutine(dialogManager.StartDialog(Dialog, gameObject));
        }
    }
}
