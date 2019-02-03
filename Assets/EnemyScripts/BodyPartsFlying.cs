using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsFlying : MonoBehaviour {

	float startTime;
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;

	// Use this for initialization
	void Start () {
		//Random.state = (int)((float)System.DateTime.Now.Millisecond * Time.time);
		startTime = Time.time;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (Random.Range(xMin, xMax), Random.Range(yMin, yMax));
	}

	void Update() {
		if (Time.time > startTime + 3.0f) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag != "Ground") {
			Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
		}
	}

}
