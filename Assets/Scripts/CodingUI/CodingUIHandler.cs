﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This object will set up the coding puzzle and checks for the right answer 
/// </summary>
public class CodingUIHandler : MonoBehaviour
{
    public GameObject KeywordPrefab;
    public GameObject StringPrefab;
    public GameObject VariablePrefab;
    public GameObject NumberPrefab;

    public GameObject AnswerBlockPrefab;

    public Transform KeywordContainer;
    public Transform StringContainer;
    public Transform VariableContainer;
    public Transform NumberContainer;

    public Transform AnswerPanel;
    public GameObject DescriptionPanel;

    public GameObject CodingUIContainer;

    // The maximum number of blocks per column in the coding ui
    //  Can be changed if the coding UI changes its proportions 
    public int MaxNumberOfPuzzleBlocks = 4;

    private List<PuzzleBlock> _answer;
    private Action _onCorrectAnswerCallback;
    private Action _onWrongAnswerCallback;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    /// <summary>
    /// This method is called to open up the puzzle
    ///     It will use the proper inventory blocks using random blocks and the answer
    ///     It will split the answer and inventory into keywords and nonkeywords for simplicity
    /// </summary>
    /// <param name="inventory">The inventory of the player</param> 
    /// <param name="answer">The correct answer of this puzzle</param> 
    /// <param name="puzzleDescription"></param> 
    /// <param name="onCorrectAnswerCallback">The function that will be called when the player finds the correct answer</param> 
    /// <param name="onWrongAnswerCallback">The function that will be called when the player puts in the wrong answer</param> 
    public void ShowCodingUI(List<PuzzleBlock> inventory, List<PuzzleBlock> answer, string puzzleDescription, Action onCorrectAnswerCallback, Action onWrongAnswerCallback)
    {
        if (CodingUIContainer.activeSelf)
            return;
        _playerMovement.CodingUIActive = true;

        _answer = answer;
        _onCorrectAnswerCallback = onCorrectAnswerCallback;
        _onWrongAnswerCallback = onWrongAnswerCallback;

        // Split the inventory by creating deep copies
        List<PuzzleBlock> inventoryKeywords = inventory.Where(block => block.Type == PuzzleBlockType.Keyword).ToList();
        List<PuzzleBlock> inventoryStrings = inventory.Where(block => block.Type == PuzzleBlockType.String).ToList();
        List<PuzzleBlock> inventoryVariables = inventory.Where(block => block.Type == PuzzleBlockType.Variable).ToList();
        List<PuzzleBlock> inventoryNumbers = inventory.Where(block => block.Type == PuzzleBlockType.Number).ToList();
        //List<PuzzleBlock> inventoryNonKeywords = inventory
        //    .Where(block =>
        //    (block.Type != PuzzleBlockType.Undefined && block.Type != PuzzleBlockType.Keyword) || // This check will be changed when new Coding UI is implemented (for now all not Undefined and not Keywords are non keywords)
        //    (block.Type == PuzzleBlockType.Undefined && !block.IsKeyword)).ToList();

        List<PuzzleBlock> answerKeywords = answer.Where(block => block.Type == PuzzleBlockType.Keyword).ToList();
        //List<PuzzleBlock> answerNonKeywords = answer
        //    .Where(block =>
        //    (block.Type != PuzzleBlockType.Undefined && block.Type != PuzzleBlockType.Keyword) || // This check will be changed when new Coding UI is implemented (for now all not Undefined and not Keywords are non keywords)
        //    (block.Type == PuzzleBlockType.Undefined && !block.IsKeyword)).ToList();

        // Create the Puzzle Block lists to use of the coding UI
        List<PuzzleBlock> keywords = CreatePuzzleBlocksForPuzzle(inventoryKeywords, answerKeywords);
        //List<PuzzleBlock> nonKeywords = CreatePuzzleBlocksForPuzzle(inventoryNonKeywords, answerNonKeywords);

        SetCodingUI(keywords, answer, puzzleDescription);
    }

    /// <summary>
    /// This is the brains of this object
    ///     1. It will look for the answer puzzle blocks in the inventory and put them in the list 
    ///         a. If it does not find an answer puzzle block it will put a block without content in the list instead
    ///     2. It will fill up the list of puzzle blocks until the maximum amount per list is reached or the inventory copy is empty
    ///     3. It will shuffle the list and then return it
    /// </summary>
    /// <param name="inventory">A copy of the inventory containing either only keywords or only nonkeywords (same as answer)</param> 
    /// <param name="answers">A copy of the answer blocks containing either only keywords or only nonkeywords (same as inventory)</param> 
    /// <returns>The created shuffled list containing the user input blocks</returns> 
    private List<PuzzleBlock> CreatePuzzleBlocksForPuzzle(List<PuzzleBlock> inventory, List<PuzzleBlock> answers)
    {
        List<PuzzleBlock> result = new List<PuzzleBlock>();

        // 1. Put answer puzzle blocks in result
        foreach (PuzzleBlock answer in answers)
        {
            PuzzleBlock possiblePuzzleBlock = inventory
                .Where(e => e.Equals(answer))
                .FirstOrDefault();

            result.Add(possiblePuzzleBlock);
            inventory.Remove(possiblePuzzleBlock);
        }

        // 2. Fill the rest of the puzzleblocks
        while (result.Count < MaxNumberOfPuzzleBlocks && inventory.Count > 0)
        {
            result.Add(inventory[0]);
            inventory.RemoveAt(0);
        }

        // 3. Return the shuffled array for a randomized outcome
        return result.Shuffle();
    }

    /// <summary>
    /// This will put the coding fill the coding UI with the proper blocks
    /// </summary>
    /// <param name="keywords">A list of the user input keywords</param> 
    /// <param name="nonKeywords">A list of the user input non keywords</param> 
    /// <param name="answer">A list of the answer blocks</param> 
    /// <param name="puzzleDescription"></param> 
    private void SetCodingUI(List<PuzzleBlock> keywords, List<PuzzleBlock> answer, string puzzleDescription)
    {
        keywords.ForEach(b => AddToPanel(b, KeywordPanel, KeywordPrefab));
        //nonKeywords.ForEach(b => AddToPanel(b, NonKeywordPanel, NonKeywordPrefab));
        answer.ForEach(b => Instantiate(AnswerBlockPrefab, AnswerPanel));

        DescriptionPanel.GetComponentInChildren<Text>().text = puzzleDescription;

        CodingUIContainer.SetActive(true);
    }

    private void AddToPanel(PuzzleBlock block, Transform panel, GameObject puzzleBlockPrefab)
    {
        Instantiate(puzzleBlockPrefab, panel).GetComponent<DraggableCodingBlock>().SetAnswerBlock(block);
    }

    public void CloseCodingUI()
    {
        if (!CodingUIContainer.activeSelf)
            return;
        _playerMovement.CodingUIActive = false;

        _onCorrectAnswerCallback = null;

        foreach (Transform c in KeywordPanel)
            Destroy(c.gameObject);
        //foreach (Transform c in NonKeywordPanel) Destroy(c.gameObject);
        foreach (Transform c in AnswerPanel)
            Destroy(c.gameObject);

        CodingUIContainer.SetActive(false);
    }

    public void OnCheckAnswer()
    {
        if (IsAnswerCorrect())
        {
            _onCorrectAnswerCallback();
            CloseCodingUI();
        }
        else
        {
            _onWrongAnswerCallback();
            // TODO: Show that answer is incorrect in GUI
            Debug.Log("Incorrect Answer! Try again!");
        }
    }

    private bool IsAnswerCorrect()
    {
        List<PuzzleBlock> answerToCheck = new List<PuzzleBlock>();
        foreach (Transform answerBLock in AnswerPanel)
        {
            if (answerBLock.childCount == 0)
                return false;
            answerToCheck.Add(answerBLock.GetComponentInChildren<DraggableCodingBlock>().GetAnswerBlock());
        }

        return _answer.SequenceEqual(answerToCheck);
    }
}
