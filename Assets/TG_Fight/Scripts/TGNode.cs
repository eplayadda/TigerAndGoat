using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eNodeHolder
{
	none = 0,
	tiger,
	goat
};
public class TGNode : MonoBehaviour {
    public int ID;
	public List <BranchTGNode> branchTgNodes;
	public eNodeHolder currNodeHolder;
	public Image nodeHolderSprint; 

	public void SetNodeHolderSprint()
	{
		if (currNodeHolder == eNodeHolder.goat) {
			nodeHolderSprint.sprite = BordManager.instace.goatTexture;
			nodeHolderSprint.enabled = true;
		} else if (currNodeHolder == eNodeHolder.tiger) {
			nodeHolderSprint.enabled = true;
			nodeHolderSprint.sprite = BordManager.instace.tigerTexture;
		} else {
			nodeHolderSprint.enabled = false;
		}
	}
}

[System.Serializable]
public class BranchTGNode
{
	public TGNode firstLayerNode;
	public TGNode secondLayerNode;
}