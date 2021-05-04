using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNPC : InteractableObject
{
    public GameObject dialogManager;
    public Dialog dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        dialogManager.SetActive(true);
        if (dialogManager.GetComponent<DialogManager>().doneDialog) {
            dialogManager.GetComponent<DialogManager>().StartDialog(dialog);
        }

    }
}
