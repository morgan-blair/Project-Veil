using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public static GameController control;

	public bool playerDead;
	public bool inBattle;

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void Disable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "Game" && playerDead == true)
		{
			inBattle = false;

			Destroy(GameObject.Find("BadGuy"));

		}
	}

	void Awake()
	{
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this)
		{
			Destroy(gameObject);
		}

	}

}