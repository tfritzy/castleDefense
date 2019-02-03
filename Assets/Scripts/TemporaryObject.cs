using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObject : MonoBehaviour {

	float startTime;
	public float duration;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	void Update() {
		if (Time.time > startTime + duration) {
			Destroy (this.gameObject);
		}
	}
}
