using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigun : MonoBehaviour {

	private float duration;
	private int arrowDamage;
	public GameObject arrow;
	private bool isGoing;
	private float lastCastTime;
	private float cooldown = 30f;
	private Vector2 archerLocation;
	private float lastArrowTime;
	private int arrowsPerSecond;
	private float timeBetweenArrows;
    private Text text;
    private float startCastTime;
	void Go(){
		if (Time.time > lastCastTime + this.cooldown) {
			this.isGoing = true;
			this.arrowsPerSecond = 10 + GameControl.control.minigunLevel;
			this.timeBetweenArrows = 1 / (float)arrowsPerSecond;
			Debug.Log ("Time between arrows: " + this.timeBetweenArrows);
			archerLocation = GameObject.Find ("ArcherSpawnLocation").transform.position;
            this.startCastTime = Time.time;
		}
	}

	// Use this for initialization
	void Start () {
        this.text = GameObject.Find("MinigunTimer").GetComponent<Text>();
		this.lastCastTime = -100f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isGoing && Time.time > lastArrowTime + timeBetweenArrows) {
			if (this.archerLocation == null) {
				this.archerLocation = GameObject.Find ("Archer").transform.position;
			}
			Vector2 targetLocation = Vector2.zero;
			if (Input.touchCount > 0) {
				targetLocation = Camera.main.ScreenToWorldPoint(Input.GetTouch (0).position);
			} else if (Input.GetMouseButton (0)) {
				targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
			if (targetLocation != Vector2.zero) {
				float xDiff = Random.Range (-.5f, .5f);
				float yDiff = Random.Range (-.5f, .5f);
				GameObject arrowInst = Instantiate (this.arrow);

				if (GameControl.control.minigunLevel > 9) {
					arrowInst.SendMessage ("SetExplosive", true);
				}
                Vector3 archerPos = GameObject.Find("ArcherSpawnLocation").transform.position;
                archerPos.z = 0;

                arrowInst.transform.position =  archerPos + new Vector3(xDiff, yDiff, 0);
				arrowInst.SendMessage ("SetDamage", 3 + GameControl.control.minigunLevel);
				Vector2 fireDirection = (targetLocation - (Vector2)arrowInst.transform.position) + new Vector2(xDiff, yDiff);
				fireDirection = fireDirection / fireDirection.magnitude;
				arrowInst.GetComponent<Rigidbody2D> ().velocity = fireDirection * 30f;
			}
			lastArrowTime = Time.time;
			if (Time.time > startCastTime + 4f + .5f * GameControl.control.minigunLevel) {
				this.isGoing = false;
				this.lastCastTime = Time.time;
                this.text.text = "";
			}
		}

		if (GameControl.control.minigunLevel > 0) {
			if (Time.time > lastCastTime + this.cooldown) {
				this.GetComponent<Button> ().interactable = true;
			} else {
				this.GetComponent<Button> ().interactable = false;
                this.text.text = ((int)(this.cooldown - (Time.time - this.lastCastTime))).ToString(); 
			}
		} else {
			this.GetComponent<Button> ().interactable = false;
		}
	}
}
