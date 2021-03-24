using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _inTrigger;
    private GameObject _interactibleObject;
    private List<PuzzleBlock> _inventory = new List<PuzzleBlock>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inTrigger)
        {
            PuzzleBlock puzzleBlock = _interactibleObject.GetComponent<InteractableObject>().GetContent();
            if (puzzleBlock != null)
            {
                _inventory.Add(puzzleBlock);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleObject"))
        {
            _inTrigger = true;
            _interactibleObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleObject"))
        {
            _inTrigger = false;
            _interactibleObject = null;
        }
    }
}
