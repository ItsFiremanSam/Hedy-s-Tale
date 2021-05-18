using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class DialogManager : MonoBehaviour
{
    public Text textDisplay;
    public Text textName;
    public Dialog dialog;
    private int index = 0;
    public float normalTypingSpeed;
    public float fastTypingSpeed;
    public float _typingSpeed;
    public bool isSentenceDonePrinting;
    public bool isDialogDone = true;
    private bool initalNPCTalkFirst;
    private Action _onCompleteCallback;

    private PlayerMovement _playerMovement;
    public GameObject container;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _typingSpeed = normalTypingSpeed;
        container.SetActive(false);
    }

    IEnumerator PrintDialog()
    {
        isSentenceDonePrinting = false;
        textDisplay.text = "";
        textName.text = "";
        _typingSpeed = normalTypingSpeed;
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
        foreach (char letter in dialog.sentences[index])
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
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
                _typingSpeed = fastTypingSpeed;
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
            if (_onCompleteCallback != null)
            {
                _onCompleteCallback();
            }
            HideDialog();
        }
    }

    //Reset the dialog so the dialog can be used multiple times
    public void HideDialog()
    {
        textDisplay.text = "";
        textName.text = "";
        index = 0;
        isSentenceDonePrinting = false;
        isDialogDone = true;
        _typingSpeed = normalTypingSpeed;
        dialog.NPCTalkFirst = initalNPCTalkFirst;
        _playerMovement.DialogUIActive = false;
        _onCompleteCallback = null;
        container.SetActive(false);
    }

    public void ClearDialog()
    {
        textDisplay.text = "";
        textName.text = "";
        _typingSpeed = normalTypingSpeed;
        isSentenceDonePrinting = false;
    }

    public void ShowDialog()
    {
        container.SetActive(true);
        isDialogDone = false;
        _playerMovement.DialogUIActive = true;
        _typingSpeed = normalTypingSpeed;
        initalNPCTalkFirst = dialog.NPCTalkFirst;
        ClearDialog();
    }

    //Starting the first sentence of the dialog
    public void StartDialog(Dialog dialog, Action onCompleteCallback = null)
    {
        _onCompleteCallback = onCompleteCallback;
        if (dialog.sentences.Count != 0)
        {
            this.dialog = dialog;
            ShowDialog();
            StartCoroutine(PrintDialog());
        }
        else
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            Debug.LogWarning($"Dialog is empty! \n {trace}");
#endif
            if (onCompleteCallback != null)
            {
                _onCompleteCallback();
            }
        }
    }
}