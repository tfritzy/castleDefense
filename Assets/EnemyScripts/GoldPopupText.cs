using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPopupText : MonoBehaviour {

	float lastTime;
	int count;
	float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastTime + .01) {
			
			if (Time.time < startTime + 1.0f) {
				Vector2 newPos = transform.position;
				newPos.y += .04f;
				this.transform.position = newPos;
			} else {
				if (Time.time > startTime + 2.5f) {
					Destroy (this.gameObject);
	
				}
			}
			lastTime = Time.time;
		}
	}
}
