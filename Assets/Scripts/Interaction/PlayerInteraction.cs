using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _inTrigger;
    private GameObject _interactibleObject;
    private List<string> _inventory = new List<string>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inTrigger)
        {
            Debug.Log("Interacting with an InteractibleOjbect!");
            string contentInteractibleObject = _interactibleObject.GetComponent<InteractionObject>().GetContent();
            if (contentInteractibleObject != null)
            {
                _inventory.Add(contentInteractibleObject);
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
