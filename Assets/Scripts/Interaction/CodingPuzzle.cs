using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingPuzzle : InteractableObject
{
    private CodingUIHandler _codingUIHandler;
    private AnimationTrigger _animationTrigger;
    public Dialog DialogFirst;
    public Dialog DialogHasAnswer;
    public Dialog DialogPuzzleCompleted;
    private DialogManager dialogManager;
    private PlayerInteraction _playerIntercation;
    private bool PuzzleComplete;

    public List<PuzzleBlock> Answer;
    [TextArea(4, 8)]
    public string PuzzleDescription;

    private void Awake()
    {
        _codingUIHandler = FindObjectOfType<CodingUIHandler>();
        _animationTrigger = GetComponentInChildren<AnimationTrigger>();
        dialogManager = Resources.FindObjectsOfTypeAll<DialogManager>()[0];
    }

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        _playerIntercation = playerInteraction;

        if (dialogManager.isDialogDone)
        {
            bool hasAnswer = true;
            foreach (PuzzleBlock block in Answer)
            {
                if (!_playerIntercation.Inventory.Contains(block))
                {
                    hasAnswer = false;
                }
            }
            if (PuzzleComplete)
            {
                // if the puzzle is finished
                dialogManager.StartDialog(DialogPuzzleCompleted);
            }
            else if (hasAnswer)
            {
                // if Hedy got the answer before the puzzle
                dialogManager.StartDialog(DialogHasAnswer, ShowCodingUICallback);
            }
            else
            {
                // if Hedy doesn't got the answer before the puzzle
                dialogManager.StartDialog(DialogFirst, ShowCodingUICallback);
            }
        }
    }

    public void ShowCodingUICallback()
    {
        _codingUIHandler.ShowCodingUI(_playerIntercation.Inventory, Answer, PuzzleDescription, OnPuzzleCompleteCallback, OnPuzzleWrongCallback);
    }

    // TODO: Make animation possible using animation waypoint system
    public void OnPuzzleCompleteCallback()
    {
        //_isInteracted = true;
        //_player.InteractionEvent -= OnInteractWithPlayer;
        //ShowInteractionBubble(false);

        dialogManager.StartDialog(DialogPuzzleCompleted);
        _animationTrigger.StartAnimation();
        PuzzleComplete = true;
    }

    // Used for puzzles with limited amount of tries or similar
    public void OnPuzzleWrongCallback()
    {
    }
}
