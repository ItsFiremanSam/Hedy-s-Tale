using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected GameObject _interactionBubbleText;

    // Is used to stop the interaction bubble from being displayed after it is activated.
    protected bool _isInteracted;

    void Start()
    {
        _interactionBubbleText = transform.GetChild(0).gameObject;
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

    /*
     * We cannot use the method "OnTriggerStay2D" to check input of the player, because "OnTriggerStay2D" is like "FixedUpdate". 
     * "OnTriggerStay2D" and "FixedUpdate" doesn't always register the input of the player.
     * Source: https://forum.unity.com/threads/need-help-with-ontriggerstay-and-getkeydown.200356/
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteraction player = collision.GetComponent<PlayerInteraction>();
        if (player)
        {
            player.InteractionEvent += OnInteractWithPlayer;
            ShowInteractionBubble(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteraction player = collision.GetComponent<PlayerInteraction>();
        if (player)
        {
            player.InteractionEvent -= OnInteractWithPlayer;
            ShowInteractionBubble(false);
        }
    }

    protected abstract void OnInteractWithPlayer(PlayerInteraction playerInteraction);
}
