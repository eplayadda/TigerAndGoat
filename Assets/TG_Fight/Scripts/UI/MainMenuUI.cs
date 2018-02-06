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

	public Transform settingStartPos;
	public Transform settingEndPos;

	GameManager gameManager;
	UIManager uiManager;
	public Text username;
	public  Image ProfilePic;

	void OnEnable ()
	{
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
		ScoreHandler.instance.GetCoin ();
	}

	public void OnGameModeSelected (int a)
	{
		if (tigerTgl.isOn == true) {
			gameManager.myAnimalType = eAnimalType.tiger;
			gameManager.friendAnimalType = eAnimalType.goat;
			GameManager.instance.currTurnStatus = eTurnStatus.friend;


		}
		if (goatTgl.isOn == true) {
			gameManager.myAnimalType = eAnimalType.goat;
			gameManager.friendAnimalType = eAnimalType.tiger;
			GameManager.instance.currTurnStatus = eTurnStatus.my;

		}
		if (a < 3) {
			gameManager.currGameStatus = eGameStatus.play;
			uiManager.DisableAllUI ();
			uiManager.gamePlayUI.gameObject.SetActive (true);
			GameManager.instance.OnGameModeSelected (a);
		} else {
			if (GameManager.instance.currentGameType == GameType.OnLine) {
				ServerRoomPanel.SetActive (true);
				SocialManager.Instance.facebookManager.UserProfile ();
			} else {
				UIManager.instance.fbLoginCheckPanel.SetActive (true);
			}
		}
	}

	public void OnClickWhatsAppShare ()
	{
		UIAnimationController.Instance.OnClickShare ();
		SocialManager.Instance.ShareWithWhatsApp ();
	}

	public void OnClickFBShare ()
	{
		UIAnimationController.Instance.OnClickShare ();
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
		isSettingOn = true;
		settingPanle.SetActive (true);
		UIAnimationController.Instance.SettingPanelAnimation (settingPanle, 0);
	}


	public void OnClickBackSetting ()
	{
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
