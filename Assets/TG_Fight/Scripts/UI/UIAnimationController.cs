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

	public void SettingPanelAnimation (GameObject gameObject, float to)
	{
		LeanTween.moveLocalX (gameObject, to, 0.25f).setEase (LeanTweenType.easeInOutExpo).setOnComplete (UIManager.instance.mainMenuUI.SettingAnimationCallback);
	}

	public void PausePanleAnimation (GameObject gameObject, float to)
	{
		LeanTween.moveLocalY (gameObject, to, 0.25f).setEase (LeanTweenType.easeInOutExpo).setOnComplete (UIManager.instance.pauseMenuUI.PauseMenuUICallBack);
	}
}
