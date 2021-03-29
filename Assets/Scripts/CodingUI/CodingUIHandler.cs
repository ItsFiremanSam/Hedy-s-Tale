using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public int MaxNumberOfPuzzleBlocks = 4;

    private List<PuzzleBlock> _answer;
    private Action _onCorrectAnswerCallback;

    // TODO: Add delegate callback for when oncorrectanswer
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

    private List<PuzzleBlock> CreatePuzzleBlocksForPuzzle(List<PuzzleBlock> inventory, List<PuzzleBlock> answers)
    {
        List<PuzzleBlock> result = new List<PuzzleBlock>();

        // Put answer puzzle blocks in result
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

        // Fill the rest of the puzzleblocks
        while (result.Count < MaxNumberOfPuzzleBlocks && inventory.Count > 0)
        {
            result.Add(inventory[0]);
            inventory.RemoveAt(0);
        }

        // Return the shuffled array for a randomized outcome
        return result.Shuffle();
    }

    public void CloseCodingUI()
    {
        if (!CodingUIContainer.activeSelf) return;

        foreach (Transform c in KeywordPanel) Destroy(c.gameObject);
        foreach (Transform c in NonKeywordPanel) Destroy(c.gameObject);
        foreach (Transform c in AnswerPanel) Destroy(c.gameObject);

        CodingUIContainer.SetActive(false);
    }

    public void OnCheckAnswer()
    {
        if (IsAnswerCorrect())
        {
            // TODO: Possibly show that answer is correct in GUI (maybe for half a seconds or so)
            Debug.Log("Correct Answer!");
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
