using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	float birthTime;
	public float damage;
	public Archer archer;
	public float velocity;
	protected Vector2 destination;

	private Vector2 startPos;
	private float lastAttackCheck;
	private Rigidbody2D rb;

	private bool isExplosive = false;
	public GameObject explosion;

    public string attackerName;

	void Start(){
		birthTime = Time.time;
		GameObject[] allies = GameObject.FindGameObjectsWithTag ("Ally");
		for (int i = 0; i < allies.Length; i++) {
			Physics2D.IgnoreCollision (allies[i].GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
		}
			
		rb = this.GetComponent<Rigidbody2D> ();


	}

	// Update is called once per frame
	void Update () {
		if (Time.time > (birthTime + 8.0f)) {
			Destroy (this.gameObject);
		}

		if (rb.velocity.magnitude > .5f) {
			Quaternion newRotation = transform.rotation;
			newRotation.z = Mathf.Rad2Deg * Mathf.Atan(rb.velocity.y / rb.velocity.x);
			this.transform.eulerAngles = new Vector3 (newRotation.x, newRotation.y, newRotation.z);
		}

		Collider2D[] colls = Physics2D.OverlapCircleAll (this.transform.position, 1.0f);
		for (int i = 0; i < colls.Length; i++) {
			if (colls [i].gameObject.tag == "Enemy") {
                if (Random.RandomRange(0, 100) <= GameControl.control.archerCritChance)
                {
                    this.damage *= (1f + (200 + GameControl.control.archerCritDamage)/100f );
                }
                colls[i].gameObject.SendMessage("SetLastAttacker", attackerName);
                colls [i].gameObject.SendMessage ("TakeDamage", this.damage);
                
                this.damage = 0;
				OnHitEffects(colls[i].gameObject);
				if (isExplosive) {
					Explode ();
				}

                AudioManager.manager.Play("arrowHit");
				Destroy (this.gameObject);
			}
			if (colls [i].gameObject.tag == "Ground") {

				if (this.isExplosive) {
					Explode ();
				}

				Destroy (this.gameObject, 1f);
			}
		}


	}

	void Explode(){
		Collider2D[] colls = Physics2D.OverlapCircleAll (this.transform.position, 3f);
		if (isExplosive) {
			GameObject explosionInst = Instantiate (this.explosion);
			explosionInst.transform.localScale = new Vector3 (2f, 2f, 2f);
			explosionInst.transform.position = this.transform.position;
			for (int j = 0; j < colls.Length; j++) {
				if (colls [j].tag == "Enemy") {
                    colls [j].SendMessage("SetLastAttacker", attackerName);
                    colls [j].SendMessage ("TakeDamage", this.damage);
				}
			}
			Destroy (explosionInst, explosionInst.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length/explosionInst.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).speed);

		}
	}
		

	void SetExplosive(bool value){
		this.isExplosive = value;
	}
		

	void SetDamage(float damage){
		this.damage = damage;
	}

	public virtual void OnHitEffects (GameObject target){

	}

	public void SetDestination(Vector2 destinaiton){

		/*
		this.destination = destinaiton;

		this.startPos = (Vector2)this.transform.position;
		Vector2 travelDirection = destination - startPos;

		Vector2 arrowVector = (travelDirection / travelDirection.magnitude) * velocity;
		this.GetComponent<Rigidbody2D> ().velocity = arrowVector;
		Quaternion newRotation = transform.rotation;
		newRotation.z = Mathf.Rad2Deg * Mathf.Atan(arrowVector.y / arrowVector.x);
		if (arrowVector.x < 0) {
			newRotation.z += 180;
		}
		this.transform.eulerAngles = new Vector3 (newRotation.x, newRotation.y, newRotation.z);
		*/
	}


}
