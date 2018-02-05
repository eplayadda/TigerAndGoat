﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;
	public MainMenuUI mainMenuUI;
	public GamePlayUI gamePlayUI;
	public GameOverUI gameOverUI;
	public GameObject loginPanel;
	public InviteUI inviteUI;
	public GameObject fbFriendsPanel;
	public GameObject pausePanel;
	public PauseMenuUI pauseMenuUI;

	public GameObject fbLoginCheckPanel;


	public Transform pauseEntryPos;
	public Transform pauseEndPos;

	void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape) && GameManager.instance.currGameStatus == eGameStatus.play) {
			pausePanel.SetActive (true);
			GameManager.instance.currGameStatus = eGameStatus.pause;
			UIAnimationController.Instance.PausePanleAnimation (pausePanel, 0);
		}
	}

	public void OnGameOver ()
	{
		Invoke ("GameOverInvoke", 1f);
	}

	void GameOverInvoke ()
	{
		gameOverUI.gameObject.SetActive (true);
	}

	public void DisableAllUI ()
	{
		mainMenuUI.gameObject.SetActive (false);
		loginPanel.SetActive (false);
		gameOverUI.gameObject.SetActive (false);
		inviteUI.gameObject.SetActive (false);
		fbFriendsPanel.gameObject.SetActive (false);
	}

	public void OnClickAsGuest ()
	{
		GameManager.instance.currentGameType = GameType.OffLine;
		ConnectionManager.Instance.MakeConnection ();
		loginPanel.SetActive (false);
		mainMenuUI.gameObject.SetActive (true);
	}

	public void OnSendRequest (int price, int type)
	{
		
		inviteUI.gameObject.SetActive (true);
		inviteUI.friendAnimalType = type;
	}

	public void OnGameStartOnServer ()
	{
		gamePlayUI.gameObject.SetActive (true);
		gamePlayUI.OnServerGameStart ();

	}

	public void OnChallangeAccepted ()
	{
		gamePlayUI.OnServerPlayerAccepted ();
	}

	public void OnFriendInviteAccepted ()
	{
		DisableAllUI ();
		gamePlayUI.gameObject.SetActive (true);
		gamePlayUI.OnInvieAcceptedByME ();
		BordManager.instace.OnGameStart ();

	}

	public void OnCancleFriendList ()
	{
		fbFriendsPanel.SetActive (false);
	}

	public void OnCloseFbCheck ()
	{
		fbLoginCheckPanel.SetActive (false);
	}

}
