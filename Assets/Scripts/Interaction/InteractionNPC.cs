using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNPC : InteractableObject
{
    public GameObject dialogManager;
    public Dialog dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        if (!_isInteracted)
        {
            _isInteracted = true;
            ShowInteractionBubble(false);
            dialogManager.GetComponent<DialogManager>().StartDialog(dialog);
        }
    }
}
