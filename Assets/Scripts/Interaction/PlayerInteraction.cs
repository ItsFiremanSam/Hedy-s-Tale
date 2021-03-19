using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _inTrigger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inTrigger)
        {
            Debug.Log("Interacting with an InteractibleOjbect!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleObject"))
        {
            _inTrigger = true;
            Debug.Log("Entering trigger:" + collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleObject"))
        {
            _inTrigger = false;
            Debug.Log("Exiting trigger:" + collision.name);
        }
    }
}
