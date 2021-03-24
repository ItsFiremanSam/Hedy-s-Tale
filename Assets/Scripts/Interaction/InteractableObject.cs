using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected GameObject _interactionBubbleText;
    [SerializeField] protected bool _puzzleBlockIsKeyword;
    [SerializeField] protected string _puzzleBlockContent;
    protected bool _isInteracted;
    // Start is called before the first frame update
    void Start()
    {
        _interactionBubbleText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInteracted)
        {
            _interactionBubbleText.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>() && !_isInteracted)
        {
            _interactionBubbleText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>() && !_isInteracted)
        {
            _interactionBubbleText.SetActive(false);
        }
    }
    public abstract PuzzleBlock GetContent();
}
