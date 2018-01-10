using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;
	public MainMenuUI mainMenuUI;
	public GamePlayUI gamePlayUI;
	public GameOverUI gameOverUI;
	public GameObject loginPanel;

	void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	public void OnGameOver ()
	{
		gameOverUI.gameObject.SetActive (true);
	}

	public void DisableAllUI ()
	{
		mainMenuUI.gameObject.SetActive (false);
		gamePlayUI.gameObject.SetActive (false);
	}

	public void OnClickAsGuest ()
	{
		loginPanel.SetActive (false);
		mainMenuUI.gameObject.SetActive (true);
	}

}
