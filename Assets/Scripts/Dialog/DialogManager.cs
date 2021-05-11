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
    private float typingSpeed;
    public bool isSentenceDonePrinting;
    public bool isDialogDone = true;
    private bool initalNPCTalkFirst;
    private Action _onCompleteCallback;

    private PlayerMovement _playerMovement;
    private GameObject _canvas;

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
        foreach (char letter in dialog.sentences[index])
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
            if (_onCompleteCallback != null)
            {
                _onCompleteCallback();
            }
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
        _playerMovement.DialogUIActive = false;
        _onCompleteCallback = null;
        gameObject.SetActive(false);
    }

    //Starting the first sentence of the dialog
    public void StartDialog(Dialog dialog, Action onCompleteCallback = null)
    {
        _onCompleteCallback = onCompleteCallback;
        if (dialog.sentences.Count != 0)
        {
            gameObject.SetActive(true);
            isDialogDone = false;
            _playerMovement.DialogUIActive = true;
            this.dialog = dialog;
            initalNPCTalkFirst = dialog.NPCTalkFirst;
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