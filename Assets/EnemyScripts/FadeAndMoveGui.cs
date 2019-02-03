using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeAndMoveGui : MonoBehaviour {

	public Text text;
	public Color color;
	private float lastTime;
	private float startTime;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<Text> ();
		lastTime = Time.time;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > (lastTime + .02f)){
			/*
			if (Time.time < startTime + .5f) {
				//slide text upwards
				Vector2 lastPos = transform.position;
				lastPos.y += .05f;
				transform.position = lastPos;
			}
			*/

			if (Time.time > startTime + 1.0f) {
				Destroy (this.gameObject);
			}


			lastTime = Time.time;
		}

		
	}
}
