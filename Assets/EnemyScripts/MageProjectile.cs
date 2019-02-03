using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : MonoBehaviour {

	public float speed;
	public int damage;
	private GameObject target;

	// Use this for initialization
	void Start () {
		Vector2 targetLoc = (Vector2)target.transform.position;
		targetLoc = targetLoc / targetLoc.magnitude;
		targetLoc = targetLoc * speed;
		this.GetComponent<Rigidbody2D> ().velocity = targetLoc;
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D[] collisions = Physics2D.OverlapCircleAll (transform.position, .6f);
		for (int i = 0; i < collisions.Length; i++) {
			Collider2D current = collisions [i];
			if (current.gameObject.tag == "Ally") {
				current.gameObject.SendMessage ("TakeDamage", this.damage);
				Destroy (this.gameObject);
			}
		}
	}

	public void SetTarget(GameObject target){
		this.target = target;
	}

	public void SetDamage(int amount){
		this.damage = amount;
	}
}
