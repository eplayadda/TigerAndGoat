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
	public GameObject pausePanel;
	public PauseMenuUI pauseMenuUI;

	public GameObject internetCheckPanel;
	public GameObject fbLoginCheckPanel;
	public GameObject panelLoading;

	public GameObject tutorialLeft;
	public GameObject tutorialRight;
	public GameObject tutorialParent;

	public Transform pauseEntryPos;
	public Transform pauseEndPos;

	private float tutorialDelay = 5.0f;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		panelLoading.SetActive (true);
		Invoke ("LoadingDisable", 2.0f);
	}

	void LoadingDisable ()
	{
		panelLoading.SetActive (false);
		TutorialReset ();
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape) && GameManager.instance.currGameStatus == eGameStatus.play) {
			pausePanel.SetActive (true);
			GameManager.instance.currGameStatus = eGameStatus.pause;
			Time.timeScale = 0;
			//UIAnimationController.Instance.PausePanleAnimation (pausePanel, 0);
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

	public void NoINternetDisplay ()
	{
		internetCheckPanel.SetActive (true);
		UIAnimationController.Instance.InternetCheckPanel (internetCheckPanel, 0.25f);
	}

	public void OnCloseInternetCheck ()
	{
		internetCheckPanel.SetActive (false);
		internetCheckPanel.transform.localScale = Vector3.zero;
	}

	public void DisplayTutorial ()
	{
		if (GameManager.instance.showTutorial) {
			tutorialParent.SetActive (true);
			tutorialLeft.SetActive (true);
			UIAnimationController.Instance.TutorialAnimation (tutorialLeft);
			Invoke ("DisplayRightTutorial", tutorialDelay);
		}

	}

	private void DisplayRightTutorial ()
	{
		tutorialLeft.SetActive (false);
		tutorialRight.SetActive (true);
		UIAnimationController.Instance.TutorialAnimation (tutorialRight);
		Invoke ("TutorialReset", tutorialDelay);
	}

	private void TutorialReset ()
	{
		tutorialParent.SetActive (false);
		tutorialLeft.SetActive (false);
		tutorialRight.SetActive (false);
		tutorialLeft.transform.localScale = Vector3.zero;
		tutorialRight.transform.localScale = Vector3.zero;
	}

}
