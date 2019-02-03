using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArrowBarrage : MonoBehaviour {

	private float cooldown = 30f;

	public GameObject arrow;
	public GameObject fireArrow;

	private float lowTargetRange = -11f;
	private float highTargetRange = 18f;
	private float height = 12f;
	private float groundHeight = -6.63f;

	private float arrowsPerSecond;
	public float timeBetweenArrows;
	private float lastArrowTime;
	private float duration;
	private int damagePerArrow;
	public float lastCastTime;
	private bool isGoing;
	private float arrowVelocity = 20f;
    private int baseDamage;

	private Text cooldownText;

	void Go(){
		Debug.Log ("Arrowbarrage going");
		this.arrowsPerSecond = 10 + GameControl.control.seekingArrowBarrageLevel;
		this.timeBetweenArrows = 1f / (float)arrowsPerSecond;
		this.damagePerArrow = 1 + GameControl.control.seekingArrowBarrageLevel;
		this.duration = 3f + .1f * GameControl.control.seekingArrowBarrageLevel;
        this.baseDamage = damagePerArrow;
		if (Time.time > lastCastTime + this.cooldown) {
			this.isGoing = true;
			lastCastTime = Time.time;
		}
	}

	// Use this for initialization
	void Start () {
		lastCastTime = Time.time;
		this.lastCastTime = -100f;
		this.cooldownText = GameObject.Find ("ArrowBarrageTimer").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.seekingArrowBarrageLevel > 0) {
			if (Time.time > lastCastTime + this.cooldown) {
				this.GetComponent<Button> ().interactable = true;
			} else {
				this.GetComponent<Button> ().interactable = false;
			}
		} else {
			this.GetComponent<Button> ().interactable = false;
		}

		float timeRemaining = (this.cooldown - (Time.time - this.lastCastTime));
		if (timeRemaining > 0) {
			this.cooldownText.GetComponent<Text> ().text = ((int)timeRemaining).ToString ();
		} else {
			this.cooldownText.GetComponent<Text> ().text = "";
		}

		if (isGoing) {
			if (Time.time > lastArrowTime + timeBetweenArrows) {
				Debug.Log ("fire an arrow");
                GameObject arrowInst;
                if (GameControl.control.seekingArrowBarrageLevel > 9)
                {
                    arrowInst = Instantiate(this.fireArrow);
                    this.damagePerArrow = baseDamage * 2;
                } else
                {
                    arrowInst = Instantiate(this.arrow);
                }
                arrowInst.SendMessage ("SetDamage", this.damagePerArrow);
				Vector2 finalLocation;

				finalLocation = new Vector2 (Random.Range (lowTargetRange, highTargetRange) + 5f, groundHeight);
				
				Vector3 startLocation = new Vector3 (finalLocation.x - 15f, finalLocation.y + 15f, 0);
				arrowInst.transform.position = startLocation;
				Vector2 direction = finalLocation - (Vector2)startLocation;
				direction = direction / direction.magnitude;
				arrowInst.GetComponent<Rigidbody2D> ().velocity = direction * Random.Range(this.arrowVelocity - 5f, this.arrowVelocity + 5f);
				this.lastArrowTime = Time.time;
			}		
			if (Time.time > lastCastTime + this.duration) {
				isGoing = false;
				lastCastTime = Time.time;
			}
		}

	}
}
