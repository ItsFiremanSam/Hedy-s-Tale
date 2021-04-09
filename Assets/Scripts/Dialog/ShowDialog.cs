using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public Dialog dialog;
    private int index;
    public float typingSpeed;

    private void Start()
    {
        StartCoroutine(Type());
    }
    IEnumerator Type()
    {
        foreach (char letter in dialog.getDialog()[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextSentence();
        }
    }

    public void NextSentence()
    {
        if(index < dialog.getDialog().Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void StartDialog(Dialog dialog)
    {
        this.dialog = dialog;
        gameObject.SetActive(true);
    }
}
