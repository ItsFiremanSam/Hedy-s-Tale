using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNPC : InteractableObject
{
    //public GameObject dialogController;
    public Dialog Dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        DialogManager dialogManager = Resources.FindObjectsOfTypeAll<DialogManager>()[0];
        if (!dialogManager.DialogActive)
            StartCoroutine(dialogManager.StartDialog(Dialog));
    }
}
