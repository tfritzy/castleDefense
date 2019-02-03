using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaltropLauncher : MonoBehaviour {

	float lastLaunchTime;
	public float resetTime;
	public GameObject caltrop;
	public float lifespan;
	public float damage;
	public float slow;

	// Use this for initialization
	void Start () {
		if (GameControl.control.caltropsLevel > 0) {
			this.resetTime = Mathf.Min(20.0f - (GameControl.control.caltropsLevel), 10f);
			this.lastLaunchTime = -1f - resetTime;
			this.slow = Mathf.Min (GameControl.control.caltropsLevel * .05f + .1f, .8f);
			this.lifespan = GameControl.control.caltropsLevel + 10;
			this.damage = 1 + GameControl.control.caltropsLevel / 2;
		} else {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastLaunchTime + resetTime) {
			Launch ();
			lastLaunchTime = Time.time;
		}
	}

	void Launch (){
		for (int i = 0; i < Mathf.Min (GameControl.control.caltropsLevel / 2 + 1, 6); i++) {
			GameObject caltropInst = Instantiate (this.caltrop);
			caltropInst.transform.position = this.transform.position;
			caltropInst.SendMessage ("SetDamage", damage);
			caltropInst.SendMessage ("SetLifespan", lifespan);
			caltropInst.SendMessage ("SetSlow", slow);
			float landingLocation = Random.Range (-2.5f, 2.5f);
			float y0 = this.transform.position.y;
			float y = GameObject.Find ("RightEdge").transform.position.y;
			float angle = Mathf.Deg2Rad * 45f;
			float d = landingLocation - this.transform.position.x;
			float velocity = -1f * (float)((1.90359 * d * d) / (y - 1.61978 * (d + .61737f * y0)));
			caltropInst.GetComponent<Rigidbody2D> ().velocity = new Vector2 (velocity * Mathf.Cos (angle), velocity * Mathf.Cos (angle));
			caltropInst.GetComponent<Rigidbody2D> ().angularVelocity = -200f;
		}
	}
}
