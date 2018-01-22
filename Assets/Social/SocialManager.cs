using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System;

public class SocialManager : MonoBehaviour
{
	private static SocialManager mInstance;

	public static SocialManager Instance {
		get {
			if (mInstance == null)
				mInstance = FindObjectOfType<SocialManager> ();
			return mInstance;
		}
		set {
			mInstance = value;
		}
	}

	public FacebookHandler facebookManager;
	public Image userProfile;
	public Image friendProfile;

	// Use this for initialization
	void Start ()
	{
		
	}

	public void LoginWithFB ()
	{
		facebookManager.OnFacebookLogin ();
	}

	public void OnClickFacebookShare ()
	{
		facebookManager.OnFacebookShare ();
	}

	public void RateUs ()
	{
		PlayerPrefs.SetInt ("RateUs", 1);
		Debug.Log ("RetUs");
		//GameManager.instance.ShowRateUsPanel (false);
		Application.OpenURL ("market://details?id=com.eplayadda.mindssmash");
	}

	public void GetFriendFB ()
	{
		UIManager.instance.fbFriendsPanel.SetActive (true);
		facebookManager.GetFriends ();
	}

	public void OnClickInvite ()
	{
		UIManager.instance.fbFriendsPanel.SetActive (false);
	}

	public void UpdateUserProfile (string url)
	{
		Debug.Log (url);
		StartCoroutine (DownloadImage (url));
	}

	private IEnumerator DownloadImage (string url)
	{
		WWW www = new WWW (url);
		yield return www;
		Debug.Log (www.isDone + " " + www.error);
		if (string.IsNullOrEmpty (www.error)) {
			userProfile.sprite =	Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), new Vector2 (0.5f, 0.5f));
		}

	}

	public void UpdateFriendProfilePic (Sprite profilePic)
	{
		friendProfile.sprite = profilePic;
	}

}
