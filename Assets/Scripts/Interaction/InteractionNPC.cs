using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNPC : InteractableObject
{
    public DialogManager DialogManager;
    public Dialog Dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        if (DialogManager.isDialogDone) {
            DialogManager.gameObject.SetActive(true);
            DialogManager.StartDialog(Dialog);
        }
    }
}
