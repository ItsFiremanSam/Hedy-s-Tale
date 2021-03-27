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

    private List<TempAnswerBlock> _answer;


    public void ShowCodingUI(List<TempAnswerBlock> inventory, List<TempAnswerBlock> answer, string puzzleDescription)
    {
        if (CodingUIContainer.activeSelf) return;

        _answer = answer;

        List<TempAnswerBlock> blocksToAdd = new List<TempAnswerBlock>();
        List<TempAnswerBlock> inventoryCopy = inventory.ConvertAll(block => new TempAnswerBlock(block.IsKeyword, block.Content));
        inventoryCopy.Shuffle();

        foreach (TempAnswerBlock answerBlock in answer)
        {
            TempAnswerBlock blockToAdd = new TempAnswerBlock(answerBlock.IsKeyword, null);
            foreach (TempAnswerBlock inventoryBlock in inventory)
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

        foreach (TempAnswerBlock block in blocksToAdd)
        {
            if (block.IsKeyword)
            {
                if (block.Content != null)
                    Instantiate(KeywordPrefab, KeywordPanel).GetComponent<DraggableCodingBlock>().SetAnswerBlock(block);
                else
                    Instantiate(MissingBlockPrefab, KeywordPanel);
            }
            else
            {
                if (block.Content != null)
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
        List<TempAnswerBlock> answerToCheck = new List<TempAnswerBlock>();
        foreach (Transform answerBLock in AnswerPanel)
        {
            if (answerBLock.childCount == 0) return false;
            answerToCheck.Add(answerBLock.GetComponentInChildren<DraggableCodingBlock>().GetAnswerBlock());
        }

        return TempAnswerBlock.IsCorrectAnswer(_answer, answerToCheck);
    }
}

// TODO: Will change
[Serializable]
public struct TempAnswerBlock
{
    public bool IsKeyword;
    public string Content;

    public TempAnswerBlock(bool isKeyword, string content)
    {
        IsKeyword = isKeyword;
        Content = content;
    }
    public bool Equals(TempAnswerBlock otherBlock)
    {
        return otherBlock.IsKeyword == IsKeyword && otherBlock.Content == Content;
    }

    public static bool IsCorrectAnswer(List<TempAnswerBlock> correctAnswer, List<TempAnswerBlock> answerToCheck)
    {
        for (int i = 0; i < answerToCheck.Count; i++)
        {
            if (!correctAnswer[i].Equals(answerToCheck[i]))
            {
                return false;
            }
        }
        return true;
    }
}
