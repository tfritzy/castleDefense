using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float movementSpeed;
	public int damage;
	public bool isExplosion;
	protected float explosionRadius;
	public Vector2 travelDirection;
	protected float lastUpdate;
	public GameObject target;
	public bool isTracker;
	protected float size;
	public Companion owner;
	public bool isPiercing;
	protected float startTime;
	public bool isEnemyProjectile;
	private Rigidbody2D rb;
	HashSet<GameObject> enemiesHit;
	private float clearEnemiesHitTime;
    public string attackerName;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		this.size = 1;
		lastUpdate = Time.time;
		this.movementSpeed = 15f;
		enemiesHit = new HashSet<GameObject> ();
		clearEnemiesHitTime = 0f;
		this.rb = this.GetComponent<Rigidbody2D> ();

	}

	public virtual void extraUpdates() {

	}

	public void SetDamage(int damage){
		this.damage = damage;
	}

	public void SetDirection(Vector2 direction){
		this.travelDirection = direction;
		this.GetComponent<Rigidbody2D> ().velocity = this.travelDirection;
	}

	public void SetTarget(GameObject target){
		this.target = target;
	}

	protected virtual void Explode(){
		Collider2D[] explosionsTargets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
		for (int j = 0; j < explosionsTargets.Length; j++) {
			if (explosionsTargets [j].tag == "Enemy") {
                explosionsTargets[j].SendMessage ("SetLastAttacker", this.attackerName);
                explosionsTargets [j].SendMessage ("TakeDamage", damage);
			}
		}
	}

	// Update is called once per frame
	public void Update () {
		if (Time.time > clearEnemiesHitTime + .5f) {
			enemiesHit = new HashSet<GameObject> ();
			clearEnemiesHitTime = Time.time;
		}


		Quaternion newRotation = transform.rotation;
		newRotation.z = Mathf.Rad2Deg * Mathf.Atan(rb.velocity.y / rb.velocity.x);
        if (this.rb.velocity.x < 0)
        {
            //newRotation.z += 180;
        }
        this.transform.eulerAngles = new Vector3(newRotation.x, newRotation.y, newRotation.z);

        extraUpdates ();
		if (Time.time > lastUpdate + .01f ) {
			if (isTracker) {
				Vector2 directionToTarget = target.transform.position - this.transform.position;
				directionToTarget = directionToTarget / directionToTarget.magnitude;
				this.GetComponent<Rigidbody2D> ().velocity = directionToTarget * movementSpeed;
			}
			Collider2D[] collisions = Physics2D.OverlapCircleAll (this.transform.position, this.size);
			for (int i = 0; i < collisions.Length; i++) {
				if (target != null && collisions [i].gameObject == target.gameObject && !enemiesHit.Contains(target)) {
                    AudioManager.manager.Play("mageBasicAttackHit");
                    target.SendMessage("SetLastAttacker", this.attackerName);

                    target.SendMessage ("TakeDamage", this.damage);
					enemiesHit.Add (target);
					Destroy (this.gameObject);
				}
			}
			lastUpdate = Time.time;
		}
	}
}
