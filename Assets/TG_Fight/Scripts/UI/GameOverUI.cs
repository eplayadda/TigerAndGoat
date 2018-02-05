using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
	UIManager uiManager;

	public Text msgTxt;
	public Text winStatus;
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
		GameResult ();

	}

	void GameResult()
	{
		if (BordManager.instace.currWinStatus.ToString () == GameManager.instance.myAnimalType.ToString ()) {
			ScoreHandler.instance.SetScore ();
			winStatus.text = "You Win";
		} else {
			winStatus.text = "You Lose";

		}
	}
	public void OnReplay()
	{
		uiManager.gamePlayUI.gameObject.SetActive (false);
		uiManager.mainMenuUI.gameObject.SetActive (true);
		uiManager.gameOverUI.gameObject.SetActive (false);
//		Application.LoadLevel (0);
	}
}
