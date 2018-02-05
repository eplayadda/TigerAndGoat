using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {
	public static ScoreHandler instance;
	public int matchWinCount;
	public Text coinTxt;
	public int scoreFactor;
	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public void SetScore()
	{
		int oldScore = PlayerPrefs.GetInt ("Score");
		int coin = (oldScore + 1)*scoreFactor;
		coinTxt.text = coin.ToString ();
		oldScore = oldScore + 1;
		PlayerPrefs.SetInt ("Score",oldScore);
	}

	public void GetCoin()
	{
		coinTxt.text = (PlayerPrefs.GetInt ("Score")*scoreFactor).ToString();
	}
}
