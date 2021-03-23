using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected GameObject _text;
    [SerializeField] protected bool _isKeyword;
    [SerializeField] protected string _content;
    protected bool _isInteracted;
    // Start is called before the first frame update
    void Start()
    {
        _text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInteracted)
        {
            _text.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>() && !_isInteracted)
        {
            _text.SetActive(true);
            Debug.Log("Entering");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>() && !_isInteracted)
        {
            _text.SetActive(false);
            Debug.Log("Exiting");
        }
    }
    public abstract PuzzleBlock GetContent();
}
