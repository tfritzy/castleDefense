using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Abilities : MonoBehaviour {

	//Shared Variables
	public GameObject abilityArrow;
	Button thisButton;
	Camera cam;
	GameObject archer;
	GameObject cursor;

	// Trace Shot Variables
	bool traceShot = false;
	int progress;
	List<Vector3> locations;
	GameObject arrowClone = null;
	public GameObject redDot;
	bool isArrowFlying;
	bool downPress;

	//timing variables
	float lastTraceTime;
	public float traceCooldownLength = 7.0f;

	void Start(){
		cam = Camera.main;
		progress = -1;
		isArrowFlying = false;
		lastTraceTime = Time.time;

		thisButton = this.GetComponent<Button> ();
		this.thisButton.interactable = false;

	}

	void Update(){
		TraceShot ();

		if (Time.time > lastTraceTime + traceCooldownLength) {
			thisButton.interactable = true;
		}
	} 

	void TraceShot(){
		if (Time.time > lastTraceTime + traceCooldownLength) {
			thisButton.interactable = true;
		}
		if (traceShot) {
			if (this.archer == null) {
				this.archer = GameObject.Find ("Archer");
			}
			if (progress == -1) {
				if (Input.touchCount > 0 && !isPointOverThis(Input.GetTouch(0).position)) {
					downPress = true;
					GameObject dot = Instantiate (redDot);
					Vector3 touchLocation = (Vector3)cam.ScreenToWorldPoint(Input.GetTouch (0).position);
					touchLocation = new Vector3 (touchLocation.x, touchLocation.y, 0);
					dot.transform.position = archer.transform.position;
					locations.Add (archer.transform.position);
					progress += 1;
				}
			} else {
				if (cursor == null) {
					cursor = Instantiate (redDot);
				}
				if (Input.touchCount > 0 && !isPointOverThis(Input.GetTouch(0).position)) {
					cursor.transform.position = Input.GetTouch (0).position;
					Vector3 touchLocation = cam.ScreenToWorldPoint (cursor.transform.position);
					touchLocation = new Vector3 (touchLocation.x, touchLocation.y, 0f);
					if ((touchLocation - locations [progress]).magnitude >= .5) {

						GameObject dot = Instantiate (redDot);
						Vector3 differenceVec = (touchLocation - locations [progress]);
						differenceVec = differenceVec / differenceVec.magnitude;
						differenceVec = differenceVec * .5f;
						dot.transform.position = locations [progress] + differenceVec;
						locations.Add (dot.transform.position);
						progress += 1;
					}
				}
				if (progress > 50) {
					traceShot = false;
					isArrowFlying = true;
					arrowClone = Instantiate (abilityArrow);
					progress = 0;
					Rigidbody2D arrowRb = arrowClone.GetComponent<Rigidbody2D> ();
					arrowRb.velocity = locations [progress] - arrowRb.transform.position;
					arrowClone.SendMessage ("SetDamage", (GameControl.control.arrowLevel * 30 + 30) * (GameControl.control.arrowLevel * 0.1f + 1.0f));
				}
			}
		}
		if (isArrowFlying) {
			Rigidbody2D arrowRb = arrowClone.GetComponent<Rigidbody2D> ();
			Vector2 direction = (locations [progress] - arrowClone.transform.position);
			direction = direction / direction.magnitude;
			direction = direction * 12;
			arrowRb.velocity = direction;
			if (Close (arrowClone.transform.position, locations [progress])) {
				progress += 1;
				if (progress == locations.Count - 1) {
					GameObject[] dots = GameObject.FindGameObjectsWithTag ("Temp");
					isArrowFlying = false;
					locations = new List<Vector3> ();
					Destroy (arrowClone);
					progress = -1;
					for (int i = dots.Length-1; i > 0; i--) {
						Destroy (dots [i]);
					}
				}
			}
		}
	}

	void SetTraceShot(){
		if (this.traceShot == false && Time.time > lastTraceTime + traceCooldownLength) {
			this.traceShot = true;
			locations = new List<Vector3> ();
			lastTraceTime = Time.time;
			thisButton.interactable = false;
		} 
		if (Time.time > lastTraceTime + traceCooldownLength) {
			thisButton.interactable = true;
		}
	}
		
	/**
	 * returns true if two vectors are very close to each other
	 * 
	 **/
	private bool Close(Vector2 v1, Vector2 v2){
		if (v1.x < v2.x + .5f && v1.x > v2.x - .5f) {
			if (v1.y < v2.y + .5f && v1.y > v2.y - .5f) {
				return true;
			}
		}
		return false;
	}

	private bool isPointOverThis(Vector2 point){
		RectTransform transform = this.GetComponent<RectTransform> ();
		if (transform.rect.Contains(point)){
			return true;
		} else {
			return false;
		}
	}

}
