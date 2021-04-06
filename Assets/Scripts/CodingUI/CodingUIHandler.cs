using System;
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
    public GameObject NonKeywordPrefab;
    public GameObject MissingBlockPrefab;
    public GameObject AnswerBlockPrefab;

    public Transform KeywordPanel;
    public Transform NonKeywordPanel;
    public Transform AnswerPanel;
    public GameObject DescriptionPanel;

    public GameObject CodingUIContainer;

    // The maximum number of blocks per column in the coding ui
    //  Can be changed if the coding UI changes its proportions 
    public int MaxNumberOfPuzzleBlocks = 4;

    private List<PuzzleBlock> _answer;
    private Action _onCorrectAnswerCallback;

    /// <summary>
    /// This method is called to open up the puzzle
    ///     It will use the proper inventory blocks using random blocks and the answer
    ///     It will split the answer and inventory into keywords and nonkeywords for simplicity
    /// </summary>
    /// <param name="inventory"></param> The inventory of the player
    /// <param name="answer"></param> The correct answer of this puzzle
    /// <param name="puzzleDescription"></param> 
    /// <param name="onCorrectAnswerCallback"></param> The function that will be called when the player finds the correct answer
    public void ShowCodingUI(List<PuzzleBlock> inventory, List<PuzzleBlock> answer, string puzzleDescription, Action onCorrectAnswerCallback)
    {
        if (CodingUIContainer.activeSelf) return;

        _answer = answer;
        _onCorrectAnswerCallback = onCorrectAnswerCallback;

        // Split the inventory by creating deep copies
        List<PuzzleBlock> inventoryKeywords = inventory.CreateDeepCopy(true);
        List<PuzzleBlock> inventoryNonKeywords = inventory.CreateDeepCopy(false);

        List<PuzzleBlock> answerKeywords = answer.CreateDeepCopy(true);
        List<PuzzleBlock> answerNonKeywords = answer.CreateDeepCopy(false);

        // Create the Puzzle Block lists to use of the coding UI
        List<PuzzleBlock> keywords = CreatePuzzleBlocksForPuzzle(inventoryKeywords, answerKeywords);
        List<PuzzleBlock> nonKeywords = CreatePuzzleBlocksForPuzzle(inventoryNonKeywords, answerNonKeywords);

        SetCodingUI(keywords, nonKeywords, answer, puzzleDescription);
    }

    /// <summary>
    /// This is the brains of this object
    ///     1. It will look for the answer puzzle blocks in the inventory and put them in the list 
    ///         a. If it does not find an answer puzzle block it will put a block without content in the list in stead
    ///     2. It will fill up the list of puzzle blocks until the maximum amount per list is reached or the inventory copy is empty
    ///     3. It will shuffle the list and then return it
    /// </summary>
    /// <param name="inventory"></param> A copy of the inventory containing either only keywords or only nonkeywords (same as answer)
    /// <param name="answers"></param> A copy of the answer blocks containing either only keywords or only nonkeywords (same as inventory)
    /// <returns></returns> The created shuffled list containing the user input blocks
    private List<PuzzleBlock> CreatePuzzleBlocksForPuzzle(List<PuzzleBlock> inventory, List<PuzzleBlock> answers)
    {
        List<PuzzleBlock> result = new List<PuzzleBlock>();

        // 1. Put answer puzzle blocks in result
        foreach (PuzzleBlock answer in answers)
        {
            PuzzleBlock keyword;
            if ((keyword = inventory.Where(e => e.Equals(answer)).FirstOrDefault()) != null)
            {
                result.Add(keyword);
                inventory.Remove(keyword);
            }
            else result.Add(new PuzzleBlock(answer.IsKeyword, null));
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
    /// <param name="keywords"></param> A list of the user input keywords
    /// <param name="nonKeywords"></param> A list of the user input non keywords
    /// <param name="answer"></param> A list of the answer blocks
    /// <param name="puzzleDescription"></param> 
    private void SetCodingUI(List<PuzzleBlock> keywords, List<PuzzleBlock> nonKeywords, List<PuzzleBlock> answer, string puzzleDescription)
    {
        keywords.ForEach(b => AddToPanel(b, KeywordPanel, KeywordPrefab));
        nonKeywords.ForEach(b => AddToPanel(b, NonKeywordPanel, NonKeywordPrefab));
        answer.ForEach(b => Instantiate(AnswerBlockPrefab, AnswerPanel));

        DescriptionPanel.GetComponentInChildren<Text>().text = puzzleDescription;

        CodingUIContainer.SetActive(true);
    }

    private void AddToPanel(PuzzleBlock block, Transform panel, GameObject puzzleBlockPrefab)
    {
        if (block.Content != null)
            Instantiate(puzzleBlockPrefab, panel).GetComponent<DraggableCodingBlock>().SetAnswerBlock(block);
        else
            Instantiate(MissingBlockPrefab, panel);
    }

    public void CloseCodingUI()
    {
        if (!CodingUIContainer.activeSelf) return;

        _onCorrectAnswerCallback = null;

        foreach (Transform c in KeywordPanel) Destroy(c.gameObject);
        foreach (Transform c in NonKeywordPanel) Destroy(c.gameObject);
        foreach (Transform c in AnswerPanel) Destroy(c.gameObject);

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
            // TODO: Show that answer is incorrect in GUI
            Debug.Log("Incorrect Answer! Try again!");
        }
    }

    private bool IsAnswerCorrect()
    {
        List<PuzzleBlock> answerToCheck = new List<PuzzleBlock>();
        foreach (Transform answerBLock in AnswerPanel)
        {
            if (answerBLock.childCount == 0) return false;
            answerToCheck.Add(answerBLock.GetComponentInChildren<DraggableCodingBlock>().GetAnswerBlock());
        }

        return PuzzleBlock.IsCorrectAnswer(_answer, answerToCheck);
    }
}
