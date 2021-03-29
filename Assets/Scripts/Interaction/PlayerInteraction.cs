using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<PuzzleBlock> _inventory = new List<PuzzleBlock>();

    public delegate void InteractionHandler(PlayerInteraction playerInteraction);
    public event InteractionHandler InteractionEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InteractionEvent != null) InteractionEvent(this);
        }
    }

    public List<PuzzleBlock> GetInventory()
    {
        return _inventory;
    }
}
