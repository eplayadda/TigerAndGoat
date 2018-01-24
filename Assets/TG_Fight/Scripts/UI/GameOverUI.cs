using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
	UIManager uiManager;

	public Text msgTxt;
	void OnEnable()
	{
		uiManager = UIManager.instance;
		if (BordManager.instace.currWinStatus == BordManager.eWinStatus.tiger) {
			GameManager.instance.currGameStatus = eGameStatus.over;	
			msgTxt.text = "Tiger won Game";
		} else {
			GameManager.instance.currGameStatus = eGameStatus.over;	
			msgTxt.text = "Goat won Game";
		}

	}

	public void OnReplay()
	{
		uiManager.gamePlayUI.gameObject.SetActive (false);
		uiManager.mainMenuUI.gameObject.SetActive (true);
		uiManager.gameOverUI.gameObject.SetActive (true);
//		Application.LoadLevel (0);
	}
}
