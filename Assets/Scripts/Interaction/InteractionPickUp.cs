using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPickUp : InteractableObject
{
    public override PuzzleBlock OnInteractWithPlayer()
    {
        gameObject.SetActive(false);
        return new PuzzleBlock(_puzzleBlockIsKeyword, _puzzleBlockContent);
    }
}
