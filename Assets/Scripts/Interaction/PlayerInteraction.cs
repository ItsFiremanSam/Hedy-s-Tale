using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<PuzzleBlock> Inventory = new List<PuzzleBlock>();

    public delegate void InteractionHandler(PlayerInteraction playerInteraction);
    public event InteractionHandler InteractionEvent;

    private void Awake()
    {
        foreach (PuzzleBlock block in Inventory)
        {
            if (block.Type == PuzzleBlockType.Undefined)
            {
                Debug.LogWarning("Player has a puzzle block in Inventory with type Undefined: " + block.Content);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (InteractionEvent != null) InteractionEvent(this);
        }
    }
}
