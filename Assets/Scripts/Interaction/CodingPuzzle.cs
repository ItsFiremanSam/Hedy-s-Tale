using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingPuzzle : InteractableObject
{
    private CodingUIHandler _codingUIHandler;

    public List<PuzzleBlock> Answer;
    public string PuzzleDescription;

    private void Awake()
    {
        _codingUIHandler = FindObjectOfType<CodingUIHandler>();
    }

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        _codingUIHandler.ShowCodingUI(playerInteraction.Inventory, Answer, PuzzleDescription, OnPuzzleCompleteCallback, OnPuzzleWrongCallback);
    }

    // TODO: Make animation possible using animation waypoint system
    public void OnPuzzleCompleteCallback()
    {
        Debug.Log("Correct answer");
    }

    // Used for puzzles with limited amount of tries or similar
    public void OnPuzzleWrongCallback()
    {
    }
}
