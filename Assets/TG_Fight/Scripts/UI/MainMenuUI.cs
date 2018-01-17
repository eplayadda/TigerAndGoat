using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	public Toggle tigerTgl;
	public Toggle goatTgl;
	public GameObject ServerRoomPanel;
	GameManager gameManager;
	UIManager uiManager;

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
		}
		if (goatTgl.isOn == true) {
			gameManager.myAnimalType = eAnimalType.goat;
			gameManager.friendAnimalType = eAnimalType.tiger;
		}
		if (a < 3) {
			gameManager.currGameStatus = eGameStatus.play;
			uiManager.DisableAllUI ();
			uiManager.gamePlayUI.gameObject.SetActive (true);
			GameManager.instance.OnGameModeSelected (a);
		} else {
			ServerRoomPanel.SetActive (true);
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

	public void OnCreateRoom(int a)
	{
		uiManager.DisableAllUI ();
		uiManager.gamePlayUI.gameObject.SetActive (true);
		uiManager.gamePlayUI.WaittingFriendBtn ();
		GameManager.instance.OnGameModeSelected (a);
		ConnectionManager.Instance.OnSendRequest ("100", (int)gameManager.friendAnimalType+"");
	}


}
