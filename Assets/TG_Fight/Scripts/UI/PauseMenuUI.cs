using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
	public void OnClickResume ()
	{
		GameManager.instance.currGameStatus = eGameStatus.play;
		UIManager.instance.pausePanel.SetActive (false);
	}

	public void OnClickRestart ()
	{
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
}
