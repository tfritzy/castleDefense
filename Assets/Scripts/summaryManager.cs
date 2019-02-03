using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summaryManager : MonoBehaviour {

	GameObject castle;
	GameObject archer;
	GameObject gold;

	// Use this for initialization
	void Start () {
		castle = transform.Find ("Castle").gameObject;
		archer = transform.Find ("Archer").gameObject;
		gold = transform.Find ("Gold").gameObject;
		//castle.SendMessage ("Sumarize");

	}
	public void CastleFinished(){
		archer.SendMessage ("Sumarize");
	}

	public void ArcherFinished(){
		gold.SendMessage ("Sumarize");
	}

	public void GoldFinished(){
		Application.LoadLevel ("Main Menu");
	}
}
