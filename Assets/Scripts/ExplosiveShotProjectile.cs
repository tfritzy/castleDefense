using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveShotProjectile : MonoBehaviour {

	public Vector2 finalLocation;
	private int damage;
	private bool makeSubArrows;
	public GameObject deathAnim;
	public GameObject secondaryArrow;
	private bool isExplosive = true;

	// Use this for initialization
	void Start () {
		this.isExplosive = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (this.transform.position.x > finalLocation.x - 1f && this.transform.position.x < finalLocation.x + 1f) {

			if (this.transform.position.y > finalLocation.y - 1f && this.transform.position.y < finalLocation.y + 1f) {

				Collider2D[] hits = Physics2D.OverlapCircleAll (this.transform.position, 3f);
				foreach (Collider2D col in hits) {
					if (col.gameObject.tag == "Enemy") {
                        col.gameObject.SendMessage("SetLastAttacker", "Ballista");
                        col.gameObject.SendMessage ("TakeDamage", this.damage);
                    }
				}
					
				if (makeSubArrows) {
					Debug.Log ("Make em");
					float[] xVars = new float[] { 0f, .5f, 1f, .5f, 0f, -.5f, -1f, -.5f };
					float[] yVars = new float[] { 1f, .5f, 0f, -.5f, -1f, -.5f, 0f, .5f };
					for (int i = 0; i < 8; i++) {
						GameObject lilArrow = Instantiate (this.secondaryArrow);

                        lilArrow.SendMessage ("ShouldMakeLittle", false);
						lilArrow.transform.position = this.transform.position;
						lilArrow.SendMessage ("SetFinalLocation", (Vector2)this.transform.position + new Vector2 (xVars [i] * 3, yVars [i] * 3));
						lilArrow.SendMessage ("SetDamage", this.damage / 2f);

						//set rotation
						Quaternion newRotation = lilArrow.transform.rotation;
						newRotation.z = Mathf.Rad2Deg * Mathf.Atan (yVars [i] / xVars [i]);
						lilArrow.transform.eulerAngles = new Vector3 (newRotation.x, newRotation.y, newRotation.z);

						lilArrow.transform.localScale = new Vector3 (.8f, .8f, .8f);
						lilArrow.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xVars [i] * 15, yVars [i] * 15);

					}
				}
				if (this.deathAnim == null) {
					Destroy (this.gameObject);
				}
			
				// Give the arrow an explosion animation on destroy.
				Debug.Log(this.name);
				GameObject death = Instantiate (this.deathAnim);
				death.transform.position = this.transform.position;
				death.transform.localScale = this.transform.localScale * 12;
				Destroy (death, death.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length/death.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).speed);

				Destroy (this.gameObject);
			}
		}
	}

	void ShouldMakeLittle(bool value){
		this.makeSubArrows = value;
	}

	void SetFinalLocation(Vector2 finalLocation){
		this.finalLocation = finalLocation;
	}

	void DisableExplosion(){
		this.isExplosive = false;
	}

	void SetDamage(int damage){
		this.damage = damage;
	}


}
