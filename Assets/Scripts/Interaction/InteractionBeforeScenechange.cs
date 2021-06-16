using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionBeforeScenechange : InteractableObject
{
    public Dialog dialog;

    protected override void OnInteractWithPlayer(PlayerInteraction playerInteraction)
    {
        DialogManager dialogManager = DialogManager.Instance;
        if (!dialogManager.DialogActive)
            StartCoroutine(dialogManager.StartDialog(dialog));

    }

    private void ChangeSceneToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    IEnumerator Coroutine()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.DialogUIActive = true;

        yield return new WaitForSeconds(.5f);

        DialogManager dialogManager = DialogManager.Instance;
        yield return dialogManager.StartDialog(dialog);
    }
}
