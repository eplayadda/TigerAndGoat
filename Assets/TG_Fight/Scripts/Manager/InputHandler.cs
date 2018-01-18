using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
	public static InputHandler instance;
	void Awake()
	{
		if (instance == null)
			instance = this;
	}
	public void OnInputTaken(int pData)
	{
		Debug.Log ("Data"+pData);
        BordManager.instace.OnInputByUser(pData);
		ConnectionManager.Instance.OnSendMeAnswer (pData+"");
	}
	public void OnInputTakenBYServer(int pData)
	{
		BordManager.instace.OnInputByUser(pData);

	}
}
