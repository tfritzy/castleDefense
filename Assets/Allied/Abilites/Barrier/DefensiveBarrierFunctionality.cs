using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefensiveBarrierFunctionality : MonoBehaviour {

	public int health;
	private int maxHealth;
	public int damage;
	public int level;
	private HashSet<GameObject> recentHits;
	private float lastClearHits;
    private string lastAttacker;

	//level 10 spike
	private float lastSpikeTime;
	private float spikeFrequency = 3f;


	// Use this for initialization
	void Start () {
		lastClearHits = 0f;
		lastSpikeTime = Time.time;
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastClearHits + .5f) {
			recentHits = new HashSet<GameObject> ();
			lastClearHits = Time.time;
		}

		if (GameControl.control.barrierLevel > 9) {
			if (Time.time > lastSpikeTime + spikeFrequency) {
				this.GetComponent<Animator> ().SetTrigger ("isSpiking");
				Debug.Log ("Spiking");
				Collider2D[] hits = Physics2D.OverlapCircleAll (this.transform.position, 5f);
				for (int i = 0; i < hits.Length; i++) {
					if (hits [i].gameObject.tag == "Enemy") {
                        hits[i].gameObject.SendMessage("SetLastAttacker", "Castle");
                        hits [i].gameObject.SendMessage ("TakeDamage", this.damage * 3f);
					}
				}
				lastSpikeTime = Time.time;
				lastClearHits = Time.time;
			}
		}
	}

    void SetLastAttacker(string attackerName)
    {
        this.lastAttacker = attackerName;
    }

	void TakeDamage(int amount){
		this.health -= amount;
		if (this.health <= 0) {
			Destroy (this.gameObject);
		}
		updateHealthBar ();
	}

	void SetDamage(int damage){
		this.damage = damage;
	}

	void updateHealthBar(){
		Transform healthbar = this.gameObject.transform.Find ("Healthbar");
		Transform greenbar = healthbar.Find ("Greenbar");
		Image green = greenbar.GetComponent<Image> ();
		Vector3 newScale = green.rectTransform.localScale;
		if (maxHealth != 0) {
			newScale.x = (float)((float)health / (float)maxHealth);
			green.rectTransform.localScale = newScale;
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col != null && col.gameObject.tag == "Enemy" && !recentHits.Contains(col.gameObject)) {
            col.gameObject.SendMessage("SetLastAttacker", "Castle");
            col.gameObject.SendMessage ("TakeDamage", damage);
			recentHits.Add (col.gameObject);
		}
	}

	void AddHealth(int health){
		this.maxHealth += health;
		this.health = maxHealth;
	}

	void Repair(int amount){
		this.health += amount;
		if (this.health > maxHealth) {
			this.health = maxHealth;
		}
	}

	void LevelUp(){
		this.maxHealth *= 2;
		this.health = maxHealth;
		float newDamage = (float)damage * 1.5f;
		this.damage = (int)newDamage;
	}
}
