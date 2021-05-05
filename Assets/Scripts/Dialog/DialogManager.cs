using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI textName;
    public Dialog dialog;
    public int index = 0;
    public float normalTypingSpeed;
    public float fastTypingSpeed;
    private float typingSpeed;
    public bool isSentenceDonePrinting;
    public bool isDialogDone = true;
    private bool initalNPCTalkFirst;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    IEnumerator PrintDialog()
    {
        isSentenceDonePrinting = false;
        textDisplay.text = "";
        textName.text = "";
        typingSpeed = normalTypingSpeed;
        //Switch between NPC and Hedy talking
        if (dialog.NPCTalkFirst)
        {
            dialog.NPCTalkFirst = false;
            textName.text = dialog.NPCName;
        }
        else
        {
            dialog.NPCTalkFirst = true;
            textName.text = "Hedy";
        }
        
        //Prints each letter of the sentence
        foreach (char letter in dialog.sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        index++;
        isSentenceDonePrinting = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //If the sentence is still printing, then fast forward the typing speed of that sentence. Otherwise go to the next sentence
            if (isSentenceDonePrinting)
            {
                NextSentence();
            }
            else
            {
                typingSpeed = fastTypingSpeed;
            }
        }
    }

    //Go to the next sentence if there's a sentence, otherwise close the dialog and reset it.
    public void NextSentence()
    {
        if (index < dialog.sentences.Count)
        {
            StartCoroutine(PrintDialog());
        }
        else
        {
            ResetDialog();
        }
    }

    //Reset the dialog so the dialog can be used multiple times
    public void ResetDialog()
    {
        textDisplay.text = "";
        textName.text = "";
        index = 0;
        isSentenceDonePrinting = false;
        isDialogDone = true;
        typingSpeed = normalTypingSpeed;
        dialog.NPCTalkFirst = initalNPCTalkFirst;
        _playerMovement.CodingUIActive = false;
        gameObject.SetActive(false);
    }

    //Starting the first sentence of the dialog
    public void StartDialog(Dialog dialog)
    {
        isDialogDone = false;
        _playerMovement.CodingUIActive = true;
        this.dialog = dialog;
        initalNPCTalkFirst = dialog.NPCTalkFirst;
        StartCoroutine(PrintDialog());
    }
}
