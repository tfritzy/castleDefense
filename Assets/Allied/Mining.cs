using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mining : MonoBehaviour {

	public float mineTime = 5.0f;
	float lastMineTime;
	static public int rockCount = 5;
	Text label;

	// Use this for initialization
	void Start () {
		label = GameObject.Find ("RockCountLabel").GetComponent<Text> ();
		lastMineTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((lastMineTime + mineTime) < Time.time) {
			rockCount += 1;
			lastMineTime = Time.time;
			label.text = rockCount.ToString ();

		}
	}
}
