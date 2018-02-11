using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLOnlineFriend : MonoBehaviour {

	public GameObject friendPrebs;
	public Transform parentObj;
	public MainMenuUI menuUI;
	public List<GameObject> allFrnd = new List<GameObject>();
	void OnEnable()
	{
		for (int i = 0; i < allFrnd.Count; i++) {
			Destroy (allFrnd[i]);
		}
		allFrnd.Clear ();
		for (int i = 0; i < ConnectionManager.Instance.onlineFriends.Count; i++) {
			GameObject go = Instantiate (friendPrebs);
			go.transform.SetParent (parentObj);
			go.SetActive (true);
			allFrnd.Add (go);
			go.GetComponent<FriendsDetails> ().Name.text = ConnectionManager.Instance.onlineFriends [i].ToString ();
		}
		Debug.Log ("jfvedk"+ConnectionManager.Instance.onlineFriends.Count);
	}

	public void OnCloseClicked()
	{
		gameObject.SetActive (false);
	}

	public void OnInvite(bool isInvite)
	{
		if (isInvite) {
			gameObject.SetActive (false);
			menuUI.OnCreateRoom (3);
		}
			 
	}

}
