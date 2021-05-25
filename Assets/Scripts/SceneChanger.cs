using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int CurrentLevel = 1;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// make the next level available for playing, and load the level select scene
    /// </summary>
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
