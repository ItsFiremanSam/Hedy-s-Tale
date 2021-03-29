using System;
using System.Collections;
using System.Collections.Generic;
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

    private List<PuzzleBlock> _answer;

    // TODO: Clean up
    public void ShowCodingUI(List<PuzzleBlock> inventory, List<PuzzleBlock> answer, string puzzleDescription)
    {
        if (CodingUIContainer.activeSelf) return;

        _answer = answer;

        List<PuzzleBlock> blocksToAdd = new List<PuzzleBlock>();
        List<PuzzleBlock> inventoryCopy = inventory.ConvertAll(block => new PuzzleBlock(block.IsKeyword(), block.GetContent()));
        inventoryCopy.Shuffle();

        foreach (PuzzleBlock answerBlock in answer)
        {
            PuzzleBlock blockToAdd = new PuzzleBlock(answerBlock.IsKeyword(), null);
            foreach (PuzzleBlock inventoryBlock in inventory)
            {
                if (inventoryBlock.Equals(answerBlock))
                {
                    blockToAdd = inventoryBlock;
                    inventoryCopy.Remove(inventoryBlock);
                    break;
                }
            }
            blocksToAdd.Add(blockToAdd);
            Instantiate(AnswerBlockPrefab, AnswerPanel);
        }

        while (blocksToAdd.Count < 6 && inventoryCopy.Count > 0)
        {
            blocksToAdd.Add(inventoryCopy[0]);
            inventoryCopy.RemoveAt(0);
        }

        blocksToAdd.Shuffle();

        foreach (PuzzleBlock block in blocksToAdd)
        {
            if (block.IsKeyword())
            {
                if (block.GetContent() != null)
                    Instantiate(KeywordPrefab, KeywordPanel).GetComponent<DraggableCodingBlock>().SetAnswerBlock(block);
                else
                    Instantiate(MissingBlockPrefab, KeywordPanel);
            }
            else
            {
                if (block.GetContent() != null)
                    Instantiate(NonKeywordPrefab, NonKeywordPanel).GetComponent<DraggableCodingBlock>().SetAnswerBlock(block);
                else
                    Instantiate(MissingBlockPrefab, NonKeywordPanel);
            }
        }

        DescriptionPanel.GetComponentInChildren<Text>().text = puzzleDescription;

        CodingUIContainer.SetActive(true);
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
            Debug.Log("Correct Answer!");
            CloseCodingUI();
        }
        else
        {
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
