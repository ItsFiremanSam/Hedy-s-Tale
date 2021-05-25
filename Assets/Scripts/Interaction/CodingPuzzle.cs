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
    public PuzzleBlock RewardBlock;

    public List<PuzzleBlock> Answer;
    [TextArea(4, 8)]
    public string PuzzleDescription;

    private void Awake()
    {
        _codingUIHandler = FindObjectOfType<CodingUIHandler>();
        _animationTrigger = GetComponentInChildren<AnimationTrigger>();
        dialogManager = DialogManager.Instance;
    }

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        _playerIntercation = playerInteraction;
        StartCoroutine(OnInteract());
    }

    public IEnumerator OnInteract()
    {
        if (dialogManager.DialogActive || _codingUIHandler.CodingUIContainer.activeSelf)
            yield break;

        bool hasAnswer = true;
        foreach (PuzzleBlock block in Answer)
        {
            if (!_playerIntercation.Inventory.Contains(block))
                hasAnswer = false;
        }
        if (PuzzleComplete)
        {
            // if the puzzle is finished
            yield return dialogManager.StartDialog(DialogPuzzleCompleted);
        }
        else if (hasAnswer)
        {
            // if Hedy got the answer before the puzzle
            yield return dialogManager.StartDialog(DialogHasAnswer);
            _codingUIHandler.ShowCodingUI(_playerIntercation.Inventory, Answer, PuzzleDescription, OnPuzzleCompleteCallback, OnPuzzleWrongCallback);
        }
        else
        {
            // if Hedy doesn't got the answer before the puzzle
            yield return dialogManager.StartDialog(DialogFirst);
            _codingUIHandler.ShowCodingUI(_playerIntercation.Inventory, Answer, PuzzleDescription, OnPuzzleCompleteCallback, OnPuzzleWrongCallback);
        }
    }

    public void OnPuzzleCompleteCallback()
    {
        _animationTrigger.StartAnimation();
        if (RewardBlock != null && !string.IsNullOrEmpty(RewardBlock.Content))
        {
            _player.Inventory.Add(RewardBlock);
        }
        Debug.Log("Correct answer");
        PuzzleComplete = true;
    }

    // Used for puzzles with limited amount of tries or similar
    public void OnPuzzleWrongCallback()
    {
    }
}
