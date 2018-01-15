using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	public Toggle tigerTgl;
	public Toggle goatTgl;
	GameManager gameManager;
	UIManager uiManager;

	void OnEnable ()
	{
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
	}

	public void OnGameModeSelected (int a)
	{
		gameManager.currGameStatus = eGameStatus.play;
		uiManager.DisableAllUI ();
		if (tigerTgl.isOn == true) {
			gameManager.myAnimalType = eAnimalType.tiger;
			gameManager.friendAnimalType = eAnimalType.goat;
		}
		if (goatTgl.isOn == true) {
			gameManager.myAnimalType = eAnimalType.goat;
			gameManager.friendAnimalType = eAnimalType.tiger;
		}
		uiManager.gamePlayUI.gameObject.SetActive (true);
		GameManager.instance.OnGameModeSelected (a);
	}

	public void OnClickWhatsAppShare ()
	{
		UIAnimationController.Instance.OnClickShare ();
	}

	public void OnClickFBShare ()
	{
		UIAnimationController.Instance.OnClickShare ();
	}
}
