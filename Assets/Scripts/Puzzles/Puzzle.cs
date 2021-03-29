using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //[SerializeReference]
    public List<PuzzleBlock> Answer;
    public CodingUIHandler CodingUIHandler;

    [TextArea(minLines: 3, maxLines: 10)]
    public string description;

    // TODO: Change with inventory system
    private PlayerMovement _playerMovement;

    private void Update()
    {
        if (_playerMovement != null && Input.GetKeyDown("e"))
        {
            CodingUIHandler.ShowCodingUI(_playerMovement.Inventory, Answer, description);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            _playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            _playerMovement = null;
    }

}
