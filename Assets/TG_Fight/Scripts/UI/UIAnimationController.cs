using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationController : MonoBehaviour
{
	
	public static UIAnimationController Instance;
	public GameObject sharePanel;
	private bool isShareOn = false;

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		//OnClickShare ();
	}

	public void OnClickShare ()
	{
		isShareOn = !isShareOn;
		Debug.Log ("OnClickShare");
		if (isShareOn)
			LeanTween.scale (sharePanel, Vector3.one, 0.25f).setEaseInSine ();
		else
			LeanTween.scale (sharePanel, Vector3.zero, 0.25f).setEaseInExpo ();
	}
}
