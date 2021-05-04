using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public Dialog dialog;
    public int index = 0;
    public float normalTypingSpeed;
    public float fastTypingSpeed;
    private float typingSpeed;
    public bool _donePrinting = true;

    IEnumerator PrintDialog()
    {
        typingSpeed = normalTypingSpeed;
        if (dialog.NPCTalkFirst)
        {
            dialog.NPCTalkFirst = false;
            textDisplay.text += dialog.NPCName + ": ";
        }
        else
        {
            dialog.NPCTalkFirst = true;
            textDisplay.text += "Hedy: ";
        }
        foreach (char letter in dialog.getDialog()[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        index++;
        _donePrinting = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_donePrinting)
            {
                NextSentence();
            }
            else
            {
                typingSpeed = fastTypingSpeed;
            }
        }
    }

    public void NextSentence()
    {
        if (index < dialog.getDialog().Length)
        {
            _donePrinting = false;
            textDisplay.text = "";
            StartCoroutine(PrintDialog());
        }
        else
        {
            textDisplay.text = "";
            index = 0;
            _donePrinting = false;
            gameObject.SetActive(false);
        }
    }

    public void StartDialog(Dialog dialog)
    {
        gameObject.SetActive(true);
        this.dialog = dialog;
        StartCoroutine(PrintDialog());
    }
}
