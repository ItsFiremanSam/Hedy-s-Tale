using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public string sceneName;

	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerMovement>())
		{
			ChangeScene(sceneName);
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
}
