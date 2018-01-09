using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {
	GameManager gameManager;
	UIManager uiManager;
	void OnEnable () {
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
	}

	public void OnGameModeSelected(int a){
		uiManager.DisableAllUI ();
		uiManager.gamePlayUI.gameObject.SetActive (true);
		GameManager.instance.OnGameModeSelected (a);
	}
}
