using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	public void OnInputTaken(int pData)
	{
		Debug.Log ("Data"+pData);
        BordManager.instace.OnInputByUser(pData);
	}
}
