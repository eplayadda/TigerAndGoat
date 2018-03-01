using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	public Toggle tigerTgl;
	public Toggle goatTgl;
	public GameObject ServerRoomPanel;
	public GameObject settingPanle;
	public GameObject selectPlayerPanel;

	public Transform settingStartPos;
	public Transform settingEndPos;

	GameManager gameManager;
	UIManager uiManager;
	public Text username;
	public  Image ProfilePic;
	int curMode;

	void OnEnable ()
	{
		GameManager.instance.currGameStatus = eGameStatus.mainmenu;
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
		ServerRoomPanel.SetActive (false);
		ScoreHandler.instance.GetCoin ();
		AdsHandler.Instance.ShowBannerAdsMenuPage ();
		AdsHandler.Instance.HideBannerAdsPausePage ();
	}

	public void OnGameModeSelected (int a)
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		curMode = a;
//		SelectPlayer ();
		GameManager.instance.currGameStatus = eGameStatus.playerselection;
		selectPlayerPanel.SetActive (true);

	}

	public void PlayerSelectionBack ()
	{
		Invoke ("MainMenuDelay", 0.2f);
		selectPlayerPanel.SetActive (false);
	}

	void MainMenuDelay ()
	{
		GameManager.instance.currGameStatus = eGameStatus.mainmenu;
	}

	public void SelectPlayer ()
	{
		int a = curMode;
//		selectPlayerPanel.SetActive (true);
		Debug.Log (a + "---");
		gameManager.isRandomPlayer = false;
		Debug.Log (a + "--dd-");

		selectPlayerPanel.SetActive (false);
		if (tigerTgl.isOn == true) {
			gameManager.myAnimalType = eAnimalType.tiger;
			gameManager.friendAnimalType = eAnimalType.goat;
			GameManager.instance.currTurnStatus = eTurnStatus.friend;
			if (a == 1) {
				uiManager.gamePlayUI.tigerText.text = "You";
				uiManager.gamePlayUI.goatText.text = "Computer";
			}

		}
		if (goatTgl.isOn == true) {
			gameManager.myAnimalType = eAnimalType.goat;
			gameManager.friendAnimalType = eAnimalType.tiger;
			GameManager.instance.currTurnStatus = eTurnStatus.my;
			if (a == 1) {
				uiManager.gamePlayUI.tigerText.text = "Computer";
				uiManager.gamePlayUI.goatText.text = "You";
			}
		}
		if (a < 3) {
			gameManager.currGameStatus = eGameStatus.play;
			//GameManager.instance.showTutorial = true;
			UIManager.instance.DisplayTutorial ();
			uiManager.DisableAllUI ();
			uiManager.gamePlayUI.gameObject.SetActive (true);
			GameManager.instance.OnGameModeSelected (a);
		} else if (a == 4) {
			gameManager.isRandomPlayer = true;
			ServerRoomPanel.SetActive (true);
		} else {
			if (GameManager.instance.currentGameType == GameType.OnLine) {
				ServerRoomPanel.SetActive (true);
				SocialManager.Instance.facebookManager.UserProfile ();
			} else {
				UIManager.instance.fbLoginCheckPanel.SetActive (true);
				//UIManager.instance.NoINternetDisplay ();
			}
		}
	}

	public void OnClickWhatsAppShare ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		//UIAnimationController.Instance.OnClickShare ();
		SocialManager.Instance.ShareWithWhatsApp ();
	}

	public void OnClickFBShare ()
	{
		//UIAnimationController.Instance.OnClickShare ();
		SocialManager.Instance.ShareWithFacebook ();
	}

	public void OnCreateRoom (int a)
	{
		uiManager.DisableAllUI ();
		uiManager.gamePlayUI.gameObject.SetActive (true);
		uiManager.gamePlayUI.WaittingFriendBtn ();
		GameManager.instance.OnGameModeSelected (a);
		SocialManager.Instance.facebookManager.GetFriendsNameByID (ConnectionManager.Instance.friedID);
		ConnectionManager.Instance.OnSendRequest ("100", (int)gameManager.friendAnimalType + "");
	}


	public void OnCloseServerPanel ()
	{
		ServerRoomPanel.SetActive (false);

	}

	private bool isSettingOn = false;

	public void OnSettingActive ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);
		GameManager.instance.currGameStatus = eGameStatus.setting;
		isSettingOn = true;
		settingPanle.SetActive (true);
		UIAnimationController.Instance.SettingPanelAnimation (settingPanle, 0);
	}


	public void OnClickBackSetting ()
	{
		AudioManager.Instance.PlaySound (AudioManager.SoundType.ButtonClick);

		isSettingOn = false;
		UIAnimationController.Instance.SettingPanelAnimation (settingPanle, settingEndPos.localPosition.x);
	}

	public void SettingAnimationCallback ()
	{
		if (!isSettingOn) {
			settingPanle.SetActive (false);
			settingPanle.transform.position = settingStartPos.position;
		}
	}

}
