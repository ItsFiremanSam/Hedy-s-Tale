using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool _inTrigger;
    public GameObject _interactibleObject;
    private List<PuzzleBlock> _inventory = new List<PuzzleBlock>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inTrigger)
        {
            Debug.Log("Interacting with an " + _interactibleObject.name);
            PuzzleBlock puzzleBlock = _interactibleObject.GetComponent<InteractionChest>().GetContent();
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
            Debug.Log("Entering trigger:" + collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleObject"))
        {
            _inTrigger = false;
            _interactibleObject = null;
            Debug.Log("Exiting trigger:" + collision.name);
        }
    }
}
