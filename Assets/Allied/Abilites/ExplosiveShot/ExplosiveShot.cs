using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosiveShot : MonoBehaviour {

	private Vector2 finalLocation;
	public GameObject explosiveProjectile;
	private float cooldown = 30f;
	private float lastFireTime;
	private bool isGoing;
	private Text cooldownText;

	void Start(){
		lastFireTime = Time.time - 100f;
		cooldownText = GameObject.Find ("ExplosiveShotTimer").GetComponent<Text>();
	}

	public void Go(){
		if (Time.time > lastFireTime + this.cooldown) {
			this.isGoing = true;
			this.lastFireTime = Time.time;
		}
	}

    private bool IsInThisButton(Vector2 location)
    {
        Vector2 thisLocation = (Vector2)this.transform.position;
        if (location.x > thisLocation.x - 3 && location.x < thisLocation.x + 3)
        {
            if (location.y > thisLocation.y - 3 && location.y < thisLocation.y + 3)
            {
                return true;
            }
        }
        return false;
    }

	public bool IsInputSatisfied(){

		if (this.finalLocation == Vector2.zero || IsInThisButton(finalLocation)) {
			return false;
		} else {
			return true;
		}
	}

	public void GetInput(){
		if (Input.touchCount > 0) {
			this.finalLocation = Camera.main.ScreenToWorldPoint(Input.GetTouch (0).position);
		}
		if (Input.GetMouseButton (0)) {
			this.finalLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

	public void Fire(){
		GameObject ep = Instantiate (this.explosiveProjectile, null);
		
        Vector3 startLocation = GameObject.Find("Ballista1").gameObject.transform.position;
        startLocation.z = 0;
        ep.transform.localScale = new Vector3(.2f, .2f, .2f);
        ep.transform.position = startLocation;
        ep.SendMessage ("SetDamage", GetDamage ());
		ep.SendMessage ("SetFinalLocation", finalLocation);

		// if level 10, do level 10 bonus effect.
		if (GameControl.control.explosiveShotLevel > 9) {
			ep.SendMessage ("ShouldMakeLittle", true);
		}

		// set travel direction
		Vector2 travelDirection = finalLocation - (Vector2)GameObject.Find ("Ballista1").gameObject.transform.position;
		travelDirection = travelDirection / travelDirection.magnitude;
		ep.GetComponent<Rigidbody2D> ().velocity = travelDirection * 25;

		//set rotation
		Quaternion newRotation = ep.transform.rotation;
		newRotation.z = Mathf.Rad2Deg * Mathf.Atan(travelDirection.y / travelDirection.x);
		ep.transform.eulerAngles = new Vector3 (newRotation.x, newRotation.y, newRotation.z);

		//reset variables for next launch
		finalLocation = Vector2.zero;
		lastFireTime = Time.time;
		this.isGoing = false;
	}

	public int GetDamage(){
		return 8 + GameControl.control.explosiveShotLevel * 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (isGoing) {
			if (IsInputSatisfied ()) {
				Fire ();
			} else {
				GetInput ();
			}
		} else {
			float timeRemaining = (this.cooldown - (Time.time - this.lastFireTime));
			if (timeRemaining > 0) {
				this.cooldownText.GetComponent<Text> ().text = ((int)timeRemaining).ToString ();
			} else {
				this.cooldownText.GetComponent<Text> ().text = "";
			}
		}

		if (GameControl.control.explosiveShotLevel > 0) {
			if (Time.time > lastFireTime + this.cooldown) {
				this.GetComponent<Button> ().interactable = true;
			} else {
				this.GetComponent<Button> ().interactable = false;
			}
		} else {
			this.GetComponent<Button> ().interactable = false;
		}
	}
}
