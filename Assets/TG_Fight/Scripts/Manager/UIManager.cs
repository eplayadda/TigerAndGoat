using System.Collections;
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

	void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	public void OnGameOver ()
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

}
