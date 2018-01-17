using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteUI : MonoBehaviour {
	GameManager gameManager;
	UIManager uiManager;

	void OnEnable()
	{
		gameManager = GameManager.instance;
		uiManager = UIManager.instance;
	}

	public void OnInviteClicked(bool isAccepted)
	{
		gameObject.SetActive (false);
		if (isAccepted) {
			ConnectionManager.Instance.IacceptChallage ();
			uiManager.OnFriendInviteAccepted ();
		} else {
		}
	}

}
