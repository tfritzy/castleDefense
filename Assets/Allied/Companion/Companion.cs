using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Companion : MonoBehaviour {

	float lastEnemyCheck;

	//firing variables
	public GameObject target;
	public float range;
	public float autoAttackCooldown;
	private float lastFireTime;
	public GameObject projectile;
	public float damage;
	float lastFireCheck;
	private int companionCount;


	// Use this for initialization
	void Start () {
		lastEnemyCheck = Time.time;
		lastFireTime = Time.time;
		target = null;


	}

	// Update is called once per frame
	void Update () {
		if (Time.time > lastEnemyCheck + 2.0f) {
			FindTarget ();
		}
		FireAutoAttack ();
	}

	private void FireAutoAttack(){
		if (target != null) {

			if (Time.time > lastFireTime + autoAttackCooldown) {

				GameObject proj = Instantiate (projectile);
				FormatProjectile (proj);

				//reset cooldown
				lastFireTime = Time.time;

			}
		}
	}

	protected void FindTarget(){
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, range);

		float closestDistance = 10000;
		Collider2D closest = null;

		for (int i = 0; i < colls.Length; i++) {
			float dist = (transform.position - colls[i].gameObject.transform.position).magnitude;
			if (dist < closestDistance && colls[i].tag == "Enemy"){
				closestDistance = dist;
				closest = colls [i];
			}
		}
		lastEnemyCheck = Time.time;
		if (closest != null) {
			target = closest.gameObject;
		} else {
			target = null;
		}
	}
	
	protected virtual void FormatProjectile(GameObject projectileClone){
		Vector2 startPos = new Vector2 (transform.position.x + Random.Range (-.5f, .5f), transform.position.y + Random.Range (-2.0f, 0.0f));
		projectileClone.transform.position = startPos;
		projectileClone.SendMessage ("SetDamage", GetDamage ());
	}
	public abstract int GetDamage ();
	public abstract int GetCompanionCount();
	public abstract void AddXp(int amount);
}
