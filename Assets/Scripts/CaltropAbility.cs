using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaltropAbility : MonoBehaviour {

	public GameObject caltrop;

	private int caltropDamage;
	private float cooldown = 30f;
	private float lastCastTime;
	private int caltropCount;

	private Text cooldownText;

	private void Go(){
		if (Time.time > lastCastTime + cooldown) {
			this.caltropCount = 2 + GameControl.control.caltropsLevel;
			for (int i = 0; i < caltropCount; i++) {
				GameObject caltropInst = Instantiate (this.caltrop);
                Vector3 caltropStartPos = GameObject.Find("Castle").transform.position;
                caltropStartPos.z = 0f;
                caltropInst.transform.position = caltropStartPos;
				caltropInst.SendMessage ("SetDamage", 1 + GameControl.control.caltropsLevel);
				caltropInst.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (1, 13), Random.Range (1, 13));
				caltropInst.GetComponent<Rigidbody2D> ().rotation = -10000f;
				lastCastTime = Time.time;
			}
		}
	}

	void Start(){
		this.lastCastTime = -100f;
		this.cooldownText = GameObject.Find ("CaltropsTimer").GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		float timeRemaining = (this.cooldown - (Time.time - this.lastCastTime));
		if (timeRemaining > 0) {
			this.cooldownText.GetComponent<Text> ().text = ((int)timeRemaining).ToString ();
		} else {
			this.cooldownText.GetComponent<Text> ().text = "";
		}
		if (GameControl.control.caltropsLevel > 0) {
			if (Time.time > lastCastTime + this.cooldown) {
				this.GetComponent<Button> ().interactable = true;
			} else {
				this.GetComponent<Button> ().interactable = false;
			}
		} else {
			this.GetComponent<Button> ().interactable = false;
		}
	}
}
