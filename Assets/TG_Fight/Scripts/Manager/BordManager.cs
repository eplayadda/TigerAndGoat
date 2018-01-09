using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordManager : MonoBehaviour {
	public static BordManager instace ;
	public enum eWinStatus
	{
		none,
		tiger,
		goat
	}
	public eWinStatus currWinStatus;
	public List <TGNode> allTgNodes;
	public Sprite tigerTexture;
	public Sprite goatTexture;
    int noOfTiger;
    int noOfGoat;
	GameManager gameManager;
	int selectedTigerIndex;
	public int selectedGoatIndex;
	public int coutGoatKill;
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
			FriendMove(pData);

//            MyMove(pData);
        }
        
    }

    void FriendMove(int pData)
    {
        pData = pData - 1;
        if (gameManager.friendAnimalType == eAnimalType.goat)
        {
            if (noOfGoat >= gameManager.totalNoOfGoat)
            {
				if (allTgNodes[pData].currNodeHolder == eNodeHolder.goat ) 
                {
                    selectedGoatIndex = pData;
                }
				else if(selectedGoatIndex > 0 && allTgNodes[pData].currNodeHolder == eNodeHolder.none)
                {
					if (SetDataGoat (pData)) {
						gameManager.currTurnStatus = eTurnStatus.my;
						gameManager.friendAnimalType = eAnimalType.tiger;
					}
                }
            }
            else {
                if (allTgNodes[pData ].currNodeHolder != eNodeHolder.none)
                    return;
				noOfGoat++;
                allTgNodes[pData].currNodeHolder = eNodeHolder.goat;
                allTgNodes[pData].SetNodeHolderSprint();
				gameManager.currTurnStatus = eTurnStatus.my;
				gameManager.friendAnimalType = eAnimalType.tiger;
            }
        }
        else if(gameManager.friendAnimalType == eAnimalType.tiger)
        {
			if (!IsTigerMoveAlv ()) {
				currWinStatus = eWinStatus.goat;
				UIManager.instance.OnGameOver ();
			}
			if (allTgNodes[pData].currNodeHolder == eNodeHolder.tiger ) 
			{
				selectedGoatIndex = pData;
			}
			else if(selectedGoatIndex > 0 && allTgNodes[pData].currNodeHolder == eNodeHolder.none)
			{
				if (SetDataTiger (pData)) {
					gameManager.currTurnStatus = eTurnStatus.friend;
					gameManager.friendAnimalType = eAnimalType.goat;
				}
			}
        }
    }

    void MyMove(int pData)
    {
    }

	bool SetDataGoat(int pData)
    {
		bool correctTile = false;
		foreach (BranchTGNode item in allTgNodes[selectedGoatIndex].branchTgNodes) {
			if (item.firstLayerNode.ID == pData + 1) {
				allTgNodes[pData].currNodeHolder = eNodeHolder.goat;
				allTgNodes[selectedGoatIndex].currNodeHolder = eNodeHolder.none;
				allTgNodes[pData].SetNodeHolderSprint();
				allTgNodes[selectedGoatIndex].SetNodeHolderSprint();
				selectedGoatIndex = -1;
				correctTile = true;
			}
		}
		return correctTile;
    }

	bool SetDataTiger(int pData)
	{
		bool correctTile = false;
		foreach (BranchTGNode item in allTgNodes[selectedGoatIndex].branchTgNodes) {
			if (item.firstLayerNode.ID == pData + 1 || 
				(item.secondLayerNode != null && item.secondLayerNode.ID == pData + 1 && allTgNodes[item.firstLayerNode.ID -1 ].currNodeHolder == eNodeHolder.goat)) {
				allTgNodes[pData].currNodeHolder = eNodeHolder.tiger;
				allTgNodes[selectedGoatIndex].currNodeHolder = eNodeHolder.none;
				allTgNodes[pData].SetNodeHolderSprint();
				allTgNodes[selectedGoatIndex].SetNodeHolderSprint();
				selectedGoatIndex = -1;
				correctTile = true;
				if(item.secondLayerNode != null && item.secondLayerNode.ID == pData + 1 && allTgNodes[item.firstLayerNode.ID -1 ].currNodeHolder == eNodeHolder.goat)
				{
					allTgNodes[item.firstLayerNode.ID -1].currNodeHolder = eNodeHolder.none;
					allTgNodes[item.firstLayerNode.ID -1 ].SetNodeHolderSprint();
					coutGoatKill++;
					if (coutGoatKill >= 6) {
						currWinStatus = eWinStatus.tiger;
						UIManager.instance.OnGameOver ();
					}
				}
			}
		}
		return correctTile;
	}

	bool IsTigerMoveAlv()
	{
		bool isAbvl = false;
		foreach (TGNode item in allTgNodes) {
			if (item.currNodeHolder == eNodeHolder.tiger) {
				foreach (BranchTGNode branchTg in item.branchTgNodes) {
					if (branchTg.firstLayerNode != null && 
						(branchTg.firstLayerNode.currNodeHolder == eNodeHolder.none ||
							(branchTg.firstLayerNode.currNodeHolder == eNodeHolder.goat && 
								branchTg.secondLayerNode != null && 
								branchTg.firstLayerNode.currNodeHolder == eNodeHolder.none))) {
						isAbvl = true;
						break;
					}
				}
			}
		}
		return isAbvl;
	}
}
