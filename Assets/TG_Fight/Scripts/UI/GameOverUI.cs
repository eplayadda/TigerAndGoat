using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	UIManager uiManager;

	public Text msgTxt;
	public Text winStatus;
	public GameObject[] WinnerImage;
	public GameObject winnerLogo;
	public Sprite[] winnerSprites;
	public GameObject shareButton;
	public bool isGameOver;

	void OnEnable ()
	{
		isGameOver = true;
		AdsHandler.Instance.ShowBannerAdsMenuPage ();
		AdsHandler.Instance.HideBannerAdsPausePage ();
		uiManager = UIManager.instance;
		if (BordManager.instace.currWinStatus == BordManager.eWinStatus.tiger) {
			GameManager.instance.currGameStatus = eGameStatus.gameover;
			winnerLogo.GetComponent<Image> ().sprite = winnerSprites [0];
			msgTxt.text = "Tiger Win The Game";
		} else {
			winnerLogo.GetComponent<Image> ().sprite = winnerSprites [1];
			GameManager.instance.currGameStatus = eGameStatus.gameover;	
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
			winStatus.text = "You Lost";
			shareButton.SetActive (false);
			WinnerImage [0].SetActive (false);
			WinnerImage [1].SetActive (false);
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
