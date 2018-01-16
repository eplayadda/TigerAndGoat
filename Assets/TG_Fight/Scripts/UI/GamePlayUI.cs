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
		waittingPanelBtn.interactable = false;
		gameManager.currGameStatus = eGameStatus.play;

	}
}
