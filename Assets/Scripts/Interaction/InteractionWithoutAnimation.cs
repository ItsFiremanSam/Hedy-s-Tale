using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithoutAnimation : InteractableObject
{
    public PuzzleBlock PuzzleBlock;
    public Animator codeBlockAnimator;
    public Dialog dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        if (!_isInteracted)
        {
            playerInteraction.Inventory.Add(PuzzleBlock);

            _isInteracted = true;
            ShowInteractionBubble(false);
            codeBlockAnimator.SetBool("isChestOpened", true);

            StartCoroutine(Coroutine());
        }
    }

    IEnumerator Coroutine()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.DialogUIActive = true;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitUntil(() => codeBlockAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle again"));
        DialogManager dialogManager = DialogManager.Instance;
        yield return dialogManager.StartDialog(dialog);
    }
}
