using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingPuzzle : InteractableObject
{
    private CodingUIHandler _codingUIHandler;
    private AnimationTrigger _animationTrigger;
    public DialogManager DialogManager;
    public Dialog Dialog;
    private PlayerInteraction _playerIntercation;

    public List<PuzzleBlock> Answer;
    [TextArea(4, 8)]
    public string PuzzleDescription;

    private void Awake()
    {
        _codingUIHandler = FindObjectOfType<CodingUIHandler>();
        _animationTrigger = GetComponentInChildren<AnimationTrigger>();
    }

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        _playerIntercation = playerInteraction;
        if (DialogManager.isDialogDone)
        {
            DialogManager.gameObject.SetActive(true);
            DialogManager.StartDialog(Dialog, ShowCodingUICallback);
        }
    }

    public void ShowCodingUICallback()
    {
        _codingUIHandler.ShowCodingUI(_playerIntercation.Inventory, Answer, PuzzleDescription, OnPuzzleCompleteCallback, OnPuzzleWrongCallback);
    }

    // TODO: Make animation possible using animation waypoint system
    public void OnPuzzleCompleteCallback()
    {
        _isInteracted = true;
        _player.InteractionEvent -= OnInteractWithPlayer;
        ShowInteractionBubble(false);
        _animationTrigger.StartAnimation();
        Debug.Log("Correct answer");
    }

    // Used for puzzles with limited amount of tries or similar
    public void OnPuzzleWrongCallback()
    {
    }
}
