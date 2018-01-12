using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tg_FightAI : MonoBehaviour {
	public static Tg_FightAI instance;
	BordManager bordManager;
	List <int> aiMove = new List<int>();
	void Start()
	{
		if (instance == null)
			instance = this;
		bordManager = BordManager.instace;
		Debug.Log ("Chadnan");
	}

	public List<int> GetTigerNextMove()
	{
		int indexTo = -1 ;
		int indexFrom = -1 ;
		List <int> tempTo = new List<int>();
		List <int> tempFrom = new List<int>();
		aiMove.Clear ();
		tempTo.Clear ();
		tempFrom.Clear ();
		Debug.Log (bordManager.allTgNodes.Count+" jh");
		foreach (TGNode node in bordManager.allTgNodes) {
			if (node.currNodeHolder == eNodeHolder.tiger) {
				foreach (BranchTGNode brNodes in node.branchTgNodes) {
					if (brNodes != null) {
						if (brNodes.firstLayerNode.currNodeHolder == eNodeHolder.goat && brNodes.secondLayerNode != null && 
							brNodes.secondLayerNode.currNodeHolder == eNodeHolder.none) {
							indexTo = brNodes.secondLayerNode.ID;
							indexFrom = node.ID;
							break;
						} else if (brNodes.firstLayerNode.currNodeHolder == eNodeHolder.none) {
							tempTo.Add(brNodes.firstLayerNode.ID);
							tempFrom.Add(node.ID) ;
						}
					}
				}
				if (indexTo > 0)
					break;
				
			}
		}
		if (indexTo < 0) {
			int a = Random.Range (0,tempTo.Count);
			indexTo = tempTo[a];
			indexFrom = tempFrom[a];
		}
		aiMove.Add(indexFrom);
		aiMove.Add(indexTo);
		Debug.Log (indexTo+" AI Move");
		return aiMove;
	}

	public int GetGoatNextMove()
	{
		return 0;
	}

}
