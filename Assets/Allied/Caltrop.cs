using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caltrop : MonoBehaviour {

	public int damage;
	public float lifespan;
	public float slow;
	private Hashtable recentHits;
	private float lastClearTime;
	private float birthTime;

	void Start(){
		recentHits = new Hashtable();
		this.birthTime = Time.time;
	}

	public void SetDamage(int damage){
		this.damage = damage;
	}


	public void SetLifespan(float lifespan){
		this.lifespan = lifespan;
	}

	public void SetSlow(float slow){
		this.slow = slow;
	}

	// Update is called once per frame
	void Update () {

		if (Time.time > birthTime + lifespan) {
			Destroy (this.gameObject);
		}




		Collider2D[] hits = Physics2D.OverlapCircleAll (this.transform.position, 1.0f);
		foreach (Collider2D hit in hits){
			if (hit.gameObject.tag == "Enemy") {
				if (!recentHits.Contains (hit)) {
					recentHits.Add (hit, 1);

                    hit.gameObject.SendMessage("SetLastAttacker", "Castle");

                    //Temp values!!!
                    hit.gameObject.SendMessage ("AddSlow", .5);
					hit.gameObject.SendMessage ("TakeDamage", 1);

					if (GameControl.control.caltropsLevel > 9) {
						hit.gameObject.SendMessage ("SetBleed", this.damage * 5);
					}

				}
			}
		}
	}
}
