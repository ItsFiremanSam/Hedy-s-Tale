using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractionChest : InteractableObject
{
    public PuzzleBlock PuzzleBlock;
    public Animator openChestAnimator;
    public Animator codeBlockAnimator;
    public Dialog dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        if (!_isInteracted)
        {
            playerInteraction.Inventory.Add(PuzzleBlock);

            _isInteracted = true;
            ShowInteractionBubble(false);
            openChestAnimator.SetBool("isInteracted", true);
            codeBlockAnimator.SetBool("isChestOpened", true);

            StartCoroutine(Coroutine());
        }
    }

    IEnumerator Coroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.5f);

        DialogManager dialogManager = Resources.FindObjectsOfTypeAll<DialogManager>()[0];
        yield return dialogManager.StartDialog(dialog);
    }
}