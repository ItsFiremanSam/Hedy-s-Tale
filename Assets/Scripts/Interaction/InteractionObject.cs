using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private GameObject _text;
    [SerializeField] private string _content;
    private bool _isInteracted;

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
        if (collision.GetComponent<PlayerInteraction>())
        {
            _text.SetActive(false);
            Debug.Log("Exiting");
        }
    }

    public string GetContent()
    {
        if(!_isInteracted)
        {
            _isInteracted = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            return _content;
        }
        else
        {
            return null;
        }
    }
}
