using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	private bool isOver;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isOver) {
			if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {
				Application.LoadLevel ("Main Menu");
			}
		}
	}

	public void doGameOver(){
		this.GetComponent<Animator> ().SetBool ("isGameOver", true);
		isOver = true;

	}
}
