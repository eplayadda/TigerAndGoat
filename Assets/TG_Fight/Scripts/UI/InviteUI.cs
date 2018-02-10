using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InviteUI : MonoBehaviour
{
	GameManager gameManager;
	UIManager uiManager;
	public Text playerNameTxt;
	public int friendAnimalType;

	void OnEnable ()
	{
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
	}

	public void OnInviteClicked (bool isAccepted)
	{
		gameObject.SetActive (false);

		if (isAccepted) {
			GameManager.instance.currGameMode = eGameMode.vServerMulltiPlayer;
			GameManager.instance.currPlayerIdentity = ePlayerIdentity.client;
			GameManager.instance.myAnimalType = (eAnimalType)friendAnimalType;
			if (friendAnimalType == 1) {
				GameManager.instance.currTurnStatus = eTurnStatus.friend;
				friendAnimalType = 2;
			} else {
				friendAnimalType = 1;
				GameManager.instance.currTurnStatus = eTurnStatus.my;
			}
			GameManager.instance.friendAnimalType = (eAnimalType)friendAnimalType;
			ConnectionManager.Instance.IacceptChallage (1);
			uiManager.OnFriendInviteAccepted ();
		} else {
			ConnectionManager.Instance.IacceptChallage (0);

		}
	}

}
