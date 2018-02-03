﻿using System.Collections;
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
			ServerRoomPanel.SetActive (true);
			SocialManager.Instance.facebookManager.UserProfile ();
		}
	}

	public void OnClickWhatsAppShare ()
	{
		UIAnimationController.Instance.OnClickShare ();
	}

	public void OnClickFBShare ()
	{
		UIAnimationController.Instance.OnClickShare ();
	}

	public void OnCreateRoom (int a)
	{
		uiManager.DisableAllUI ();
		uiManager.gamePlayUI.gameObject.SetActive (true);
		uiManager.gamePlayUI.WaittingFriendBtn ();
		GameManager.instance.OnGameModeSelected (a);
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
