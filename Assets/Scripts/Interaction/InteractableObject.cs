using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected GameObject _interactionText;
    protected PlayerInteraction _player;

    // Is used to stop the interaction bubble from being displayed after it is activated.
    protected bool _isInteracted;

    void Start()
    {
        _interactionText = transform.GetChild(0).gameObject;
        ShowInteractionBubble(false);
    }

    protected void ShowInteractionBubble(bool showBubble)
    {
        if (showBubble)
        {
            _interactionText.SetActive(true);
        }
        else
        {
            _interactionText.SetActive(false);
        }
    }

    /*
     * We cannot use the method "OnTriggerStay2D" to check input of the player, because "OnTriggerStay2D" is like "FixedUpdate". 
     * "OnTriggerStay2D" and "FixedUpdate" doesn't always register the input of the player.
     * Source: https://forum.unity.com/threads/need-help-with-ontriggerstay-and-getkeydown.200356/
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isInteracted) return;
        _player = collision.GetComponent<PlayerInteraction>();
        if (_player)
        {
            _player.InteractionEvent += OnInteractWithPlayer;
            ShowInteractionBubble(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _player = collision.GetComponent<PlayerInteraction>();
        if (_player)
        {
            _player.InteractionEvent -= OnInteractWithPlayer;
            ShowInteractionBubble(false);
        }
    }

    protected abstract void OnInteractWithPlayer(PlayerInteraction playerInteraction);
}
