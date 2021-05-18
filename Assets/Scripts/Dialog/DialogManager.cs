using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class DialogManager : MonoBehaviour
{
    public Text textDisplay, textName;
    public float normalTalkingSpeed, fastTalkingSpeed;
    public bool DialogActive { get; private set; }
    private float _currentTalkingSpeed;
    private const string _protagonistName = "Hedy";

    private PlayerMovement _playerMovement;
    private GameObject _container;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _currentTalkingSpeed = normalTalkingSpeed;
        _container = transform.GetChild(0).gameObject;
        _container.SetActive(false);
    }

    IEnumerator PrintDialog(Dialog dialog)
    {
        ShowDialog();
        bool currentTalking = dialog.NPCTalkFirst;
        foreach (string sentence in dialog.sentences)
        {
            //Switch between NPC and Hedy talking
            if (currentTalking)
                textName.text = dialog.NPCName;
            else
                textName.text = _protagonistName;

            yield return new WaitForEndOfFrame();
            Coroutine coroutine = StartCoroutine(AccelerateTalking());

            //Prints each letter of the sentence
            foreach (char letter in sentence)
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(_currentTalkingSpeed);
            }

            StopCoroutine(coroutine);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            ClearDialog();
            currentTalking = !currentTalking;
        }

        HideDialog();
    }

    //Reset the dialog so the dialog can be used multiple times
    public void HideDialog()
    {
        textDisplay.text = "";
        textName.text = "";
        _currentTalkingSpeed = normalTalkingSpeed;
        _playerMovement.DialogUIActive = false;
        _container.SetActive(false);
        DialogActive = false;
    }

    public void ClearDialog()
    {
        textDisplay.text = "";
        textName.text = "";
        _currentTalkingSpeed = normalTalkingSpeed;
    }

    public void ShowDialog()
    {
        _container.SetActive(true);
        _playerMovement.DialogUIActive = true;
        _currentTalkingSpeed = normalTalkingSpeed;
        DialogActive = true;
        ClearDialog();
    }

    private IEnumerator AccelerateTalking()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        _currentTalkingSpeed = fastTalkingSpeed;
    }

    //Starting the first sentence of the dialog
    public IEnumerator StartDialog(Dialog dialog)
    {
        if (dialog.sentences.Count == 0)
        {
            Debug.LogWarning($"Dialog is empty!");
            yield return null;
        }
        yield return PrintDialog(dialog);
    }
}