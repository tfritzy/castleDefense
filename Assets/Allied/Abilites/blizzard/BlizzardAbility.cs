using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlizzardAbility : MonoBehaviour {

	public GameObject blizzardObject;

	private float cooldown = 30f;
	private float lastCastTime;
	private float blizzardObjectMovementSpeed = 10f;
	private int blizzardDamage;
	private Vector2 targetLocation;
	private bool isGoing;

	private Text cooldownTimerLabel;
	private bool isDisabled;

	void Go(){
		if (Time.time > lastCastTime + this.cooldown) {
			this.isGoing = true;
            this.cooldown = 30;
            this.lastCastTime = Time.time;
		}
	}

	// Use this for initialization
	void Start () {
		this.lastCastTime = this.cooldown * -1f;
		this.isDisabled = false;
		this.cooldownTimerLabel = GameObject.Find ("BlizzardTimer").GetComponent<Text> ();
			
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

    // Update is called once per frame
    void Update () {
		if (isGoing) {
			if (targetLocation != Vector2.zero && !IsInThisButton(this.targetLocation)) {
				GameObject blizzardInst = Instantiate (this.blizzardObject);
				blizzardInst.transform.position = GameObject.Find ("MageStartLocation").transform.position;
				Vector2 movementDirection = targetLocation - (Vector2)blizzardInst.transform.position;
				movementDirection = movementDirection / movementDirection.magnitude;
				blizzardInst.GetComponent<Rigidbody2D> ().velocity = movementDirection * blizzardObjectMovementSpeed;

				this.isGoing = false;
				this.targetLocation = Vector2.zero;
				this.lastCastTime = Time.time;

			} else {
				if (Input.touchCount > 0) {
					targetLocation = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				} else if (Input.GetMouseButton (0)) {
					targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}

			}
		} else
        {
            if (GameControl.control.blizzardAbilityLevel > 0)
            {
                if (Time.time > lastCastTime + cooldown)
                {
                    this.GetComponent<Button>().interactable = true;
                } else
                {
                    this.GetComponent<Button>().interactable = false;
                }
            } else
            {
                this.GetComponent<Button>().interactable = false;
            }
        }
	}
}
