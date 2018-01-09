using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
	public Text msgTxt;
	void OnEnable()
	{
		if(BordManager.instace.currWinStatus == BordManager.eWinStatus.tiger)
			msgTxt.text = "Tiger won Game";
		else
			msgTxt.text = "Goat won Game";

	}

	public void OnReplay()
	{
		Application.LoadLevel (0);
	}
}
