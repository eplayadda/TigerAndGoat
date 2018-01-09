using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour {
	GameManager gameManager;
	UIManager uiManager;

	void OnEnable () {
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
	}
	public void OnBackClicked()
	{
		uiManager.DisableAllUI ();
		uiManager.mainMenuUI.gameObject.SetActive (true);
	}
}
