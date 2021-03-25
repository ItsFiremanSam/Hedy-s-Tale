using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField]
    protected GameObject _interactionBubbleText;
    [SerializeField]
    protected bool _puzzleBlockIsKeyword;
    [SerializeField]
    protected string _puzzleBlockContent;
    private bool _isInteracted;

    void Start()
    {
        ShowInteractionBubble(false);
    }

    protected void ShowInteractionBubble(bool showBubble)
    {
        if (!_isInteracted && showBubble)
        {
            _interactionBubbleText.SetActive(true);
        }
        else
        {
            _interactionBubbleText.SetActive(false);
        }
    }

    protected void SetIsInteracted(bool isInteracted)
    {
        _isInteracted = isInteracted;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>())
        {
            ShowInteractionBubble(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>())
        {
            ShowInteractionBubble(false);
        }
    }

    public abstract PuzzleBlock OnInteractWithPlayer();
}
