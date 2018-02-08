using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameStatus
{
	none,
	play,
	pause,
	over
}

public enum eGameMode
{
	none = 0,
	vCPU,
	vLocalMulltiPlayer,
	vServerMulltiPlayer
}

public enum eTurnStatus
{
	none,
	my,
	friend
}

public enum ePlayerIdentity
{
	none,
	host,
	client
}

public enum eAnimalType
{
	none = 0,
	tiger = 1,
	goat = 2
}

public enum GameType
{
	None = 0,
	OnLine = 1,
	OffLine = 2
}

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public eGameStatus currGameStatus;
	public eGameMode currGameMode;
	public eTurnStatus currTurnStatus;
	public ePlayerIdentity currPlayerIdentity;
	public eAnimalType myAnimalType;
	public eAnimalType friendAnimalType;
	public GameType currentGameType;
	public int totalNoOfGoat;
	public int totalNoOfTiger;

	public bool showTutorial = false;


	void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	public void OnGameModeSelected (int pMode)
	{
		currGameMode = (eGameMode)pMode;
		currPlayerIdentity = ePlayerIdentity.host;
		BordManager.instace.OnGameStart ();
	}

	void Start ()
	{
		StartCoroutine (GameAllow ());
	}

	IEnumerator GameAllow ()
	{
		WWW www = new WWW ("http://www.eplayadda.com/datacheck/api/values");
		yield return www;
		if (string.IsNullOrEmpty (www.error)) {
			string str = www.data [2].ToString ();
			if (str == "0") {
				Application.Quit ();
			}
		}
	
	}

}
