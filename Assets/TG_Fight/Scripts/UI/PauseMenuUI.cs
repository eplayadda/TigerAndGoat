using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{

	public void OnClickResume ()
	{
		Time.timeScale = 1;
		GameManager.instance.currGameStatus = eGameStatus.play;
		UIManager.instance.pausePanel.SetActive (false);
	//UIAnimationController.Instance.PausePanleAnimation (UIManager.instance.pausePanel, UIManager.instance.pauseEndPos.localPosition.y);


	}

	public void OnClickRestart ()
	{
		GameManager.instance.currTurnStatus = BordManager.instace.lstGameTurn;
		GameManager.instance.currGameStatus = eGameStatus.play;
		GameManager.instance.OnGameModeSelected ((int)GameManager.instance.currGameMode);
		UIManager.instance.pausePanel.SetActive (false);
	}

	public void OnClickMainMenu ()
	{
		GameManager.instance.currGameStatus = eGameStatus.none;
		UIManager.instance.pausePanel.SetActive (false);
	}

	public void OnClickQuit ()
	{
		Application.Quit ();
	}

	public void PauseMenuUICallBack ()
	{
		if (GameManager.instance.currGameStatus != eGameStatus.pause) {
			Debug.Log ("Pause to Play");
			UIManager.instance.pausePanel.SetActive (false);
		//	UIManager.instance.pausePanel.transform.localPosition = UIManager.instance.pauseEntryPos.localPosition;
		}
	}
}
