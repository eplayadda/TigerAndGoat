using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	UIManager uiManager;

	public Text msgTxt;
	public Text winStatus;
	public GameObject WinnerImage;

	void OnEnable ()
	{
		uiManager = UIManager.instance;
		if (BordManager.instace.currWinStatus == BordManager.eWinStatus.tiger) {
			GameManager.instance.currGameStatus = eGameStatus.over;	
			msgTxt.text = "Tiger Win The Game";
		} else {
			GameManager.instance.currGameStatus = eGameStatus.over;	
			msgTxt.text = "Goat Win The Game";
		}
		GameResult ();
		AdsHandler.Instance.HideBannerAdsMenuPage ();
		AdsHandler.Instance.ShowBannerAdsPausePage ();

	}

	void GameResult ()
	{
		if (BordManager.instace.currWinStatus.ToString () == GameManager.instance.myAnimalType.ToString ()) {
			ScoreHandler.instance.SetScore ();
			winStatus.text = "You Win";
			AudioManager.Instance.PlaySound (AudioManager.SoundType.Success);

		} else {
			winStatus.text = "You Lose";
			WinnerImage.SetActive (false);
			AudioManager.Instance.PlaySound (AudioManager.SoundType.GameOver);

		}
	}

	public void OnReplay ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		uiManager.gamePlayUI.gameObject.SetActive (false);
		uiManager.mainMenuUI.gameObject.SetActive (true);
		uiManager.gameOverUI.gameObject.SetActive (false);
//		Application.LoadLevel (0);
	}
}
