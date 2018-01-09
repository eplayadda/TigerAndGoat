using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	public static UIManager instance;
	public MainMenuUI mainMenuUI;
	public GamePlayUI gamePlayUI;

	void Awake()
	{
		if (instance == null)
			instance = this;
	}



	public void DisableAllUI()
	{
		mainMenuUI.gameObject.SetActive (false);
		gamePlayUI.gameObject.SetActive (false);
	}

}
