using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CodingBlocksAnimation : MonoBehaviour
{
    public GameObject KeywordPrefab;
    public GameObject StringPrefab;
    public GameObject VariablePrefab;
    public GameObject NumberPrefab;

    private int _lastCodingBlock = 0;
    private List<GameObject> _codingBlocks;
    private Transform _container;

    private void Awake()
    {
        _container = transform.GetChild(0);
    }

    /// <summary>
    /// Instantiates the proper coding blocks for a waypoint animation
    /// </summary>
    /// <param name="answer">A list of the coding blocks to add</param>
    public void EnterCodingBlocksAnimation(List<PuzzleBlock> answer)
    {

        _codingBlocks = new List<GameObject>(answer.Count);
        for (int i = 0; i < answer.Count; i++)
        {
            GameObject codingBlock = null;
            PuzzleBlock currentPuzzleBlock = answer[i];
            Debug.Log(name);
            switch (currentPuzzleBlock.Type)
            {
                case PuzzleBlockType.Keyword:
                    codingBlock = Instantiate(KeywordPrefab, _container);
                    break;
                case PuzzleBlockType.String:
                    codingBlock = Instantiate(StringPrefab, _container);
                    break;
                case PuzzleBlockType.Variable:
                    codingBlock = Instantiate(VariablePrefab, _container);
                    break;
                case PuzzleBlockType.Number:
                    codingBlock = Instantiate(NumberPrefab, _container);
                    break;
            }
            codingBlock.GetComponentInChildren<Text>().text = currentPuzzleBlock.Content;
            _codingBlocks.Add(codingBlock);
        }
    }

    /// <summary>
    /// Destroys the coding blocks and resets all variables
    /// </summary>
    public void ExitCodingBlocksAnimation()
    {
        foreach (GameObject codingBlock in _codingBlocks)
        {
            Destroy(codingBlock);
        }
        _codingBlocks.Clear();
        _lastCodingBlock = 0;
    }

    /// <summary>
    /// Makes the animation of which coding block is currently being used progress
    /// </summary>
    /// <param name="progressSteps">The amount of coding blocks to continue with</param>
    public void ProgressAnimation(int progressSteps)
    {
        for (int i = 0; i < _lastCodingBlock; i++)
        {
            RectTransform codingBlockRect = _codingBlocks[i].transform.GetChild(0).transform.GetComponent<RectTransform>();
            codingBlockRect.anchoredPosition = new Vector2(0, 0);
        }

        int newLastBlock = Mathf.Min(_lastCodingBlock + progressSteps, _codingBlocks.Count);
        for (int i = _lastCodingBlock; i < newLastBlock; i++)
        {
            RectTransform codingBlockRect = _codingBlocks[i].transform.GetChild(0).transform.GetComponent<RectTransform>();
            codingBlockRect.anchoredPosition = new Vector2(30, 0);
        }

        _lastCodingBlock = newLastBlock;
    }
}
