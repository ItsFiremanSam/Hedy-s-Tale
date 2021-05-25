using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int CurrentLevel = 1;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void ChangeSceneEndOfLevel()
    {
        LevelSelectHandler.ShowNextLevelAnimation(CurrentLevel);
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            ChangeSceneEndOfLevel();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
