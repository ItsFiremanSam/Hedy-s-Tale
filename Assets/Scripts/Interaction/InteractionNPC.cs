using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNPC : InteractableObject
{
    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
