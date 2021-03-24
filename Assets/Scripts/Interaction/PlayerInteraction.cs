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
            PuzzleBlock puzzleBlock = _interactibleObject.GetComponent<InteractableObject>().OnInteractWithPlayer();
            if (puzzleBlock != null)
            {
                _inventory.Add(puzzleBlock);
            }
        }

    }

    /*
     * We cannot use the method "OnTriggerStay2D" to check input of the player, because "OnTriggerStay2D" is like "FixedUpdate". 
     * "OnTriggerStay2D" and "FixedUpdate" doesn't always register the input of the player.
     * Source: https://forum.unity.com/threads/need-help-with-ontriggerstay-and-getkeydown.200356/
     */
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
