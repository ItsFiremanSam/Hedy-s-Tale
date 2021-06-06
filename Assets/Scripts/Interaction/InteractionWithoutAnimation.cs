using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithoutAnimation : InteractableObject
{
    public PuzzleBlock PuzzleBlock;
    public Animator codeBlockAnimator;
    public Dialog dialog;

    public AudioSource itemPickupSource;
    public AudioSource itemFoundSource;

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

        itemPickupSource.Play();

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.DialogUIActive = true;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitUntil(() => codeBlockAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle again"));

        itemFoundSource.Play();

        yield return new WaitForSeconds(.5f);

        DialogManager dialogManager = DialogManager.Instance;
        yield return dialogManager.StartDialog(dialog);
    }
}
