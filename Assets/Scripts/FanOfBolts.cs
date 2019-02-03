using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanOfBolts : MonoBehaviour {

	private bool isGoing;
	public float cooldown = 30f;
	public float lastAbilityCastTime;
	public GameObject bolt;
	private int damage;
    float secondsPerBolt;

	private float lastBoltFireTime;
	private Vector2 fireDirection; 
	private Vector2 ballistaLocation;
	private float fireUpOrDown;
	private float t;
	private Text cooldownText;
    private GameObject ballista;


	// Use this for initialization
	void Start () {
		this.damage = 3 + GameControl.control.fanOfBoltsLevel * 2;
        this.secondsPerBolt = 10 + GameControl.control.fanOfBoltsLevel;
		ballistaLocation = (Vector2)GameObject.Find ("Ballista1").transform.position;
		this.lastAbilityCastTime = -100f;
		this.cooldownText = GameObject.Find ("FanOfBoltsTimer").GetComponent<Text> ();
        ballista = GameObject.Find("Ballista1");
	}

	void Go(){
		if (Time.time > lastAbilityCastTime + cooldown) {
			fireDirection = new Vector2 (1, 1f);
			this.isGoing = true;
			this.fireUpOrDown = 1;
            this.lastAbilityCastTime = Time.time;
			this.t = 70f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isGoing) {
			DoAFire (this.fireUpOrDown);
		} 

		if (GameControl.control.fanOfBoltsLevel > 0) {
			if (Time.time > lastAbilityCastTime + this.cooldown) {
				this.GetComponent<Button> ().interactable = true;
			}  else
            {
                this.GetComponent<Button>().interactable = false;
            }
		} else {
			this.GetComponent<Button> ().interactable = false;
		}

		float timeRemaining = (this.cooldown - (Time.time - this.lastAbilityCastTime));
		if (timeRemaining > 0) {
			this.cooldownText.GetComponent<Text> ().text = ((int)timeRemaining).ToString ();
		} else {
			this.cooldownText.GetComponent<Text> ().text = "";
		}


	}


	/**
	 *  Fires the bolts in a fan. Pass in 1 to fire in a downards motion. Pass in -1 to fire in an upwards motion.
	 * 
	 * */
	public void DoAFire(float direction){
		if (Time.time > lastBoltFireTime + .0025) {
			float moveDist = .1f * (1 - GameControl.control.fanOfBoltsLevel * .05f);
			GameObject boltInst = Instantiate (this.bolt, this.ballista.transform);
            boltInst.transform.localScale = new Vector3(1f, 1f, 1f);

			//format the projetile
			boltInst.transform.position = ballistaLocation;
			boltInst.SendMessage ("SetDamage", this.damage);

			//chose the fire direction. This should be based on a circle. 
			float r = 10f;
			float x = r * Mathf.Cos (this.t * Mathf.Deg2Rad);
			float y = r * Mathf.Sin (this.t * Mathf.Deg2Rad);
			this.t -= 3f * direction;

			Vector2 fireDirection = new Vector2 (x, y);
			fireDirection = fireDirection / fireDirection.magnitude;

			boltInst.GetComponent<Rigidbody2D> ().velocity = fireDirection * 25f;

			

			// Stop the first fan. fire the second fan if ability is level 10.
			if (t < -90f) {
				this.isGoing = false;

				//TODO remove or true
				if (GameControl.control.fanOfBoltsLevel > 9) {
					this.isGoing = true;
					this.fireUpOrDown = -1;
				}


			}

			// stop the second wave special level 10 ability.
			if (t > 90) {
				isGoing = false;
                lastAbilityCastTime = Time.time;
                lastBoltFireTime = Time.time;
                this.isGoing = false;
            }
		}
	}
}
