using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordManager : MonoBehaviour {
	public static BordManager instace ;
	public List <TGNode> allTgNodes;
	public Sprite tigerTexture;
	public Sprite goatTexture;
    int noOfTiger;
    int noOfGoat;
	GameManager gameManager;
	int selectedTigerIndex;
	public int selectedGoatIndex;
	void Awake()
	{
		if (instace == null)
			instace = this;
	}

	public void OnGameStart()
	{
		gameManager = GameManager.instance;
		SetDefaultData ();
	}

	void SetDefaultData()
	{
		allTgNodes [0].currNodeHolder = eNodeHolder.tiger;
		allTgNodes [3].currNodeHolder = eNodeHolder.tiger;
		allTgNodes [4].currNodeHolder = eNodeHolder.tiger;
		SetTexture ();
	}

	void SetTexture()
	{
		for (int i = 0; i < allTgNodes.Count; i++) {
			allTgNodes [i].SetNodeHolderSprint ();
		}
	}

	public void OnInputByUser(int pData)
	{
        if (gameManager.currTurnStatus == eTurnStatus.friend)
        {
            FriendMove(pData);
        }
        else if (gameManager.currTurnStatus == eTurnStatus.my)
        {
            MyMove(pData);
        }
        
    }

    void FriendMove(int pData)
    {
        pData = pData - 1;
        if (gameManager.friendAnimalType == eAnimalType.goat)
        {
            if (noOfGoat >= gameManager.totalNoOfGoat)
            {
                if (selectedGoatIndex <= 0)
                {
                    selectedGoatIndex = pData;
                }
                else
                {
                    SetData(pData);
                }
            }
            else {
                if (allTgNodes[pData - 1].currNodeHolder != eNodeHolder.none)
                    return;
                noOfGoat++;
                allTgNodes[pData].currNodeHolder = eNodeHolder.goat;
                allTgNodes[pData].SetNodeHolderSprint();

            }
        }
        else if(gameManager.friendAnimalType == eAnimalType.tiger)
        {
        }
    }

    void MyMove(int pData)
    {
    }

    void SetData(int pData)
    {
        if (allTgNodes[selectedGoatIndex].branchTgNodes[pData].firstLayerNode == null)
        {
            allTgNodes[pData].currNodeHolder = eNodeHolder.goat;
            allTgNodes[pData].SetNodeHolderSprint();
        }
    }
}
