using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaBolt : MonoBehaviour {

	public int damage;
	float birthTime;
	public float radius = .6f;
	private Rigidbody2D rb;
	List<GameObject> dealtDamageTo;
	public int pierceCount;

	// Use this for initialization
	void Start () {
		birthTime = Time.time;
		rb = this.GetComponent<Rigidbody2D> ();
		dealtDamageTo = new List<GameObject> ();
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > birthTime + 10.0f) {
			Destroy (this.gameObject);
		}


		if (rb.velocity.magnitude > .5f) {
			Quaternion newRotation = transform.rotation;
			newRotation.z = Mathf.Rad2Deg * Mathf.Atan(rb.velocity.y / rb.velocity.x);
			this.transform.eulerAngles = new Vector3 (newRotation.x, newRotation.y, newRotation.z);
		}

		Collider2D[] collisions = Physics2D.OverlapCircleAll (transform.position, radius);
		for (int i = 0; i < collisions.Length; i++) {
			Collider2D current = collisions [i];
			if (current.tag == "Enemy" && dealtDamageTo.Contains(current.gameObject) == false) {
				if (dealtDamageTo.Count < pierceCount) {
                    current.SendMessage("SetLastAttacker", "Ballista");
                    current.SendMessage ("TakeDamage", damage);
					dealtDamageTo.Add (current.gameObject);
					if (dealtDamageTo.Count == pierceCount){
						Destroy(this.gameObject);
					}
                    if (GameControl.control.bleedChance >= Random.Range(0, 100))
                    {
                        // Set Bleed Effect
                        current.SendMessage("SetBleed", this.damage / 2);
                        Debug.Log("bleed");
                    }
                    
				}
			}

		}

	}

	void SetDamage(int damage){
		this.damage = damage;
	}

	void SetPierce(int pierceAmount){
		this.pierceCount = pierceAmount;
	}
}
