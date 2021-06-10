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
            playerInteraction.Inventory.Add(PuzzleBlock);
            StartCoroutine(Coroutine());
        }
    }

    IEnumerator Coroutine()
    {
        sfxSource.PlayOneShot(itemPickUpClip);
       
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.DialogUIActive = true;

        yield return new WaitForSeconds(.5f);

        DialogManager dialogManager = DialogManager.Instance;
        if(!dialogManager.DialogActive)
            yield return dialogManager.StartDialog(Dialog, gameObject);
    }
}
