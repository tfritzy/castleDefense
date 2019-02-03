using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollector : MonoBehaviour {

	Rigidbody2D rb;
	GameObject perch;
	bool isGoingToGold;
	float timeOfGoldCollection;
	public GameObject targetMoney;
	bool isWaiting;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
		perch = GameObject.Find ("Perch");
		timeOfGoldCollection = Time.time;
		isGoingToGold = false;
		targetMoney = null;
		isWaiting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeOfGoldCollection +.5f) {
			if (isGoingToGold == false) {
				targetMoney = GameObject.FindGameObjectWithTag ("Money");
				if (targetMoney == null) {
					Vector2 travelDir = perch.transform.position - this.transform.position;
					this.rb.velocity = (travelDir);
				} else {
					isGoingToGold = true;
				}
			}else {
				Vector2 travelDir = targetMoney.transform.position - this.transform.position;
				this.rb.velocity = travelDir;
				if (travelDir.magnitude < 1.0f) {
					targetMoney.SendMessage ("RewardGold");
					isGoingToGold = false;
					targetMoney = null;
					timeOfGoldCollection = Time.time;
					isWaiting = true;
					rb.velocity = Vector2.zero;
				}
			}
		} 
	}
}
