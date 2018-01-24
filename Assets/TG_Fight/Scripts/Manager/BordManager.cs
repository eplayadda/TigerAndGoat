using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public int noOfGoat;
	GameManager gameManager;
	int selectedTigerIndex;
	public int selectedGoatIndex;
	public int coutGoatKill;
	public Text goatKillTxt;
	public Text usedGoatTxt;
	bool isMoveAlow ;
	public Transform markerToMove;
	public Transform defaultPos;
	Transform startPos;
	Transform endPos;
	float speed = 10f;
	void Awake()
	{
		if (instace == null)
			instace = this;
	}

	public void OnGameStart()
	{
		gameManager = GameManager.instance;
		SetDefaultData ();
		if(IsAiEnable(false))
			StartCoroutine ("AITurnGoat");
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
		if (gameManager.currGameStatus != eGameStatus.play)
			return;
        if (gameManager.currTurnStatus == eTurnStatus.friend)
        {
			FriendMove(pData,gameManager.friendAnimalType);
        }
        else if (gameManager.currTurnStatus == eTurnStatus.my)
        {
			FriendMove(pData,gameManager.myAnimalType);
        }
        
    }
	void MarkerReset()
	{
		foreach (TGNode item in allTgNodes) {
			item.transform.GetChild (0).gameObject.SetActive (false);
		}
	}

	void FriendMove(int pData,eAnimalType pCurrAnimalType)
    {
        pData = pData - 1;
		if (pCurrAnimalType == eAnimalType.goat)
        {
			markerToMove.GetComponent<Image> ().sprite = goatTexture;
            if (noOfGoat >= gameManager.totalNoOfGoat)
            {
				if (allTgNodes[pData].currNodeHolder == eNodeHolder.goat ) 
                {
                    selectedGoatIndex = pData;
					markerToMove.position = allTgNodes [pData].transform.position;
					MarkerReset ();
					allTgNodes [selectedGoatIndex].transform.GetChild (0).gameObject.SetActive (true);
                }
				else if(selectedGoatIndex >= 0 && allTgNodes[pData].currNodeHolder == eNodeHolder.none)
                {
					if (SetDataGoat (pData)) { 
						MarkerReset ();
						endPos = allTgNodes [pData].transform;
						isMoveAlow = true;
//						speed = Vector3.Distance (markerToMove.position,endPos.position) / 5f;
						markerToMove.gameObject.SetActive (true);
						if(gameManager.currTurnStatus == eTurnStatus.friend)
							gameManager.currTurnStatus = eTurnStatus.my;
						else
							gameManager.currTurnStatus = eTurnStatus.friend;

						if(IsAiEnable(true))
							StartCoroutine ("AITurnTiger");
					}
                }
            }
            else {
                if (allTgNodes[pData ].currNodeHolder != eNodeHolder.none)
                    return;
				noOfGoat++;
				usedGoatTxt.text = (gameManager.totalNoOfGoat - noOfGoat).ToString();
				markerToMove.position = defaultPos.position;
				markerToMove.gameObject.SetActive (true);
				endPos = allTgNodes [pData].transform;
                allTgNodes[pData].currNodeHolder = eNodeHolder.goat;
				isMoveAlow = true;
//                allTgNodes[pData].SetNodeHolderSprint();
				if(gameManager.currTurnStatus == eTurnStatus.friend)
					gameManager.currTurnStatus = eTurnStatus.my;
				else
					gameManager.currTurnStatus = eTurnStatus.friend;
				if(IsAiEnable(true))
					StartCoroutine ("AITurnTiger");

			
            }
        }
		else if(pCurrAnimalType == eAnimalType.tiger)
        {
			markerToMove.GetComponent<Image> ().sprite = tigerTexture;
			if (!IsTigerMoveAlv ()) {
				currWinStatus = eWinStatus.goat;
				UIManager.instance.OnGameOver ();
			}
			if (allTgNodes[pData].currNodeHolder == eNodeHolder.tiger ) 
			{
				selectedGoatIndex = pData;
				markerToMove.position = allTgNodes [pData].transform.position;
				MarkerReset ();
				allTgNodes [selectedGoatIndex].transform.GetChild (0).gameObject.SetActive (true);

			}
			else if(selectedGoatIndex >= 0 && allTgNodes[pData].currNodeHolder == eNodeHolder.none)
			{
				if (SetDataTiger (pData)) {
					MarkerReset ();
					endPos = allTgNodes [pData].transform;
					isMoveAlow = true;
					markerToMove.gameObject.SetActive (true);
//					speed = Vector3.Distance (markerToMove.position,endPos.position) / 5f;
					if(gameManager.currTurnStatus == eTurnStatus.friend)
						gameManager.currTurnStatus = eTurnStatus.my;
					else
						gameManager.currTurnStatus = eTurnStatus.friend;
					if(IsAiEnable(false))
						StartCoroutine ("AITurnGoat");

				}
			}
        }
    }
	bool IsAiEnable(bool isTiger)
	{
		bool isEbnable = false;
		if (isTiger) {
			if (gameManager.currGameMode == eGameMode.vCPU && gameManager.friendAnimalType == eAnimalType.tiger)
				isEbnable = true;
		} else {
			if (gameManager.currGameMode == eGameMode.vCPU && gameManager.friendAnimalType == eAnimalType.goat)
				isEbnable = true;
		}
		return isEbnable;
	}
	IEnumerator AITurnTiger()
    {
		yield return new WaitForSeconds (2f);
		List <int> aiMOve = new List<int> ();
		aiMOve  = Tg_FightAI.instance.GetTigerNextMove ();
		if (aiMOve [0] >= 0) {
			OnInputByUser (aiMOve [0]);
			yield return new WaitForSeconds (.5f);
			OnInputByUser (aiMOve [1]);
		} else {
			currWinStatus = eWinStatus.goat;
			UIManager.instance.OnGameOver ();
		}
    }

	IEnumerator AITurnGoat()
	{
		yield return new WaitForSeconds (2f);
		List <int> aiMOve = new List<int> ();
		aiMOve  = Tg_FightAI.instance.GetGoatNextMove ();
		if (noOfGoat < gameManager.totalNoOfGoat) {
			OnInputByUser (aiMOve [0]);
		} else {
			Debug.Log (aiMOve[1]+" "+aiMOve[0]);
			OnInputByUser (aiMOve [1]);
			yield return new WaitForSeconds (.5f);
			OnInputByUser (aiMOve[0]);
		}

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

	void Update()
	{
		if (isMoveAlow) {
			Debug.Log ("Moving");
			markerToMove.transform.position = Vector3.MoveTowards (markerToMove.transform.position,endPos.position,speed);
			if (Vector3.Distance (markerToMove.transform.position, endPos.position) <= .01f) {
				isMoveAlow = false;
				markerToMove.gameObject.SetActive (false);
				SetTexture ();

			}
		}
	}
	bool SetDataTiger(int pData)
	{
		bool correctTile = false;
		foreach (BranchTGNode item in allTgNodes[selectedGoatIndex].branchTgNodes) {
			if (item.firstLayerNode.ID == pData + 1 || 
				(item.secondLayerNode != null && item.secondLayerNode.ID == pData + 1 && allTgNodes[item.firstLayerNode.ID -1 ].currNodeHolder == eNodeHolder.goat)) {
				allTgNodes[pData].currNodeHolder = eNodeHolder.tiger;
				allTgNodes[selectedGoatIndex].currNodeHolder = eNodeHolder.none;
//				allTgNodes[pData].SetNodeHolderSprint();
				allTgNodes[selectedGoatIndex].SetNodeHolderSprint();
				selectedGoatIndex = -1;
				correctTile = true;
				if(item.secondLayerNode != null && item.secondLayerNode.ID == pData + 1 && allTgNodes[item.firstLayerNode.ID -1 ].currNodeHolder == eNodeHolder.goat)
				{
					allTgNodes[item.firstLayerNode.ID -1].currNodeHolder = eNodeHolder.none;
//					allTgNodes[item.firstLayerNode.ID -1 ].SetNodeHolderSprint();
					coutGoatKill++;
					goatKillTxt.text = coutGoatKill.ToString ();

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
