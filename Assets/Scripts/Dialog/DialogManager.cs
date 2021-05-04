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
    public bool donePrinting = true;
    public bool doneDialog = true;
    private bool initalNPCTalkFirst;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    IEnumerator PrintDialog()
    {
        typingSpeed = normalTypingSpeed;
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

        foreach (char letter in dialog.sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        index++;
        donePrinting = true;
    }

    void Update()
    {
        doneDialog = false;
        _playerMovement.CodingUIActive = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (donePrinting)
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
        if (index < dialog.sentences.Count)
        {
            donePrinting = false;
            textDisplay.text = "";
            textName.text = "";
            StartCoroutine(PrintDialog());
        }
        else
        {
            textDisplay.text = "";
            textName.text = "";
            index = 0;
            donePrinting = false;
            doneDialog = true;
            dialog.NPCTalkFirst = initalNPCTalkFirst;
            _playerMovement.CodingUIActive = false;
            gameObject.SetActive(false);
        }
    }

    public void StartDialog(Dialog dialog)
    {
        this.dialog = dialog;
        initalNPCTalkFirst = dialog.NPCTalkFirst;
        doneDialog = false;
        StartCoroutine(PrintDialog());
    }
}
