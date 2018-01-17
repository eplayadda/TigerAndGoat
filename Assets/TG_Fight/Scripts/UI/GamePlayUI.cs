using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour {
	GameManager gameManager;
	UIManager uiManager;
	public GameObject waitingPanel;
	public Button waittingPanelBtn;
	void OnEnable () {
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
	}
	public void OnBackClicked()
	{
		uiManager.DisableAllUI ();
		uiManager.mainMenuUI.gameObject.SetActive (true);
	}

	public void WaittingFriendBtn()
	{
		waittingPanelBtn.interactable = false;
		waitingPanel.SetActive (true);
	}

	public void OnServerPlayerAccepted()
	{
		waittingPanelBtn.interactable = true;
		gameManager.currGameStatus = eGameStatus.play;

	}

	public void OnGameStart(){
		gameManager.currGameStatus = eGameStatus.play;
		waitingPanel.SetActive (false);
		ConnectionManager.Instance.OnServerGameStart ();
	}

	public void OnServerGameStart()
	{
		waitingPanel.SetActive (false);
		gameManager.currGameStatus = eGameStatus.play;
	}

	public void OnInvieAcceptedByME()
	{
		waitingPanel.SetActive (false);
		GameManager.instance.OnGameModeSelected (3);
		gameManager.currGameStatus = eGameStatus.pause;
	}
}
