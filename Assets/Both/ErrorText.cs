using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour {

	public Text text;
	private float lastTime;
	private float startTime;
	private bool isGoing;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<Text> ();
		lastTime = Time.time;
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > (lastTime + .05f) && isGoing){

			if (Time.time > startTime + .3f) {
				Color c = text.color;
				c.a = c.a - .15f;
				text.color = c;
				Debug.Log (c.a);
				Debug.Log (this.GetComponent<Text> ().color.a);
			}

			if (Time.time > startTime + 1.0f) {
				Color currentColor = text.color;
				currentColor.a = 0f;
				text.color = currentColor;
				isGoing = false;
			}
				
			lastTime = Time.time;
		}
	}

	void Go(string text){
		Debug.Log ("Go recieved");
		GameObject.Find ("ErrorText").GetComponent<Text> ().text = text;
		Color currentColor = this.GetComponent<Text>().color;
		currentColor.a = 1f;
		this.GetComponent<Text>().color = currentColor;
		isGoing = true;
		startTime = Time.time;
	}
}
