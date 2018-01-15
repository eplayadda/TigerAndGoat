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
		none,
		tiger,
		goat
	}

	public class GameManager : MonoBehaviour {
		public static GameManager instance;
		public eGameStatus currGameStatus;
		public eGameMode currGameMode;
		public eTurnStatus currTurnStatus;
		public ePlayerIdentity currPlayerIdentity;
		public eAnimalType myAnimalType;
		public eAnimalType friendAnimalType;
		public int totalNoOfGoat;
		public int totalNoOfTiger;
		void Awake () {
			if (instance == null)
				instance = this;
		}
		
		public void OnGameModeSelected(int pMode)
		{
			currGameMode = (eGameMode)pMode;
			currPlayerIdentity = ePlayerIdentity.host;
			currTurnStatus = eTurnStatus.friend;
            myAnimalType = eAnimalType.tiger;
            friendAnimalType = eAnimalType.goat;
			
            BordManager.instace.OnGameStart ();
		}
	}
