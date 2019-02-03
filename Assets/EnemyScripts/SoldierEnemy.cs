using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierEnemy : MonoBehaviour {

	//health variables
	public int health;
	public int maxhealth;
	public float healthFactor;
	public GameObject damageText;
	public GameObject blockObject;
	public GameObject poisonDamagePopup;

	Rigidbody2D rb;

	//attack variables
	public float attackRange;
	public float attackCooldown;
	private float lastAttack;
	public int attackDamage;

	//movement variables
	public float movementSpeed = -0.5f;
	float flyingHeight = -4.4f;
	public Vector2 startPos = new Vector2 ((float)10, (float)-4.456);
	public Vector3 scale;
	private Animator animator;
	//death variables
	public GameObject shield;
	public GameObject sword;
	public GameObject goldDrop;
	float birthTime;
	public GameObject dyingAnimation;

	//poison variables
	private float lastPoisonHit;
	private float firstPoisonTime;
	private float poisonDuration;
	private int poisonDPS;
	public bool isPoisoned;

	//paralyze variables
	private float firstParalyzeTime;
	private float paralyzeDuration;
	public bool isParalyzed;


	public bool isGiant = false;
	public bool isDead = false;

	//enemy variables
	public GameObject castle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		//rb.position = startPos;
		GameObject manager = GameObject.Find ("Manager");
		LevelManager managerScript = manager.GetComponent<LevelManager> ();
		maxhealth = (int)(5*Mathf.Pow(1.035f, (float)GameControl.control.gameLevel));
		this.transform.localScale = this.scale;
		health = maxhealth;
		if (isGiant) {

			this.scale *= 2;
			this.transform.localScale = this.scale;		
			maxhealth *= 4;
			health = maxhealth;
			attackDamage *= 2;
		}

		castle = GameObject.Find ("Castle");
		lastAttack = Time.time;
		this.birthTime = Time.time;
		animator = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {
		move ();
		attack ();
		PoisonDamage ();
		ManageParalysis ();
		if (Time.time > birthTime + 320.0f) {
			Destroy (this.gameObject);
		}

	}

	void TakeDamage(int damage){

		int block = Random.Range (0, 10);
		if (block != 1) {
			health -= damage;
			updateHealthBar ();
			if (health <= 0 && !isDead) {
				isDead = true;
				GameObject dying = Instantiate (dyingAnimation);
				dying.transform.position = this.transform.position;
				dying.transform.localScale = this.scale;
				dying.transform.localScale = scale;
				Destroy (dying, this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length + .3f);
				deathAnimation ();
				RewardGold ();

				Destroy (this.gameObject);
			}
		}
	}

	void TakePoisonDamage(int damage){

		int block = Random.Range (0, 10);
		if (block != 1) {
			health -= damage;
			updateHealthBar ();
			if (health <= 0 && !isDead) {
				isDead = true;
				GameObject dying = Instantiate (dyingAnimation);
				dying.transform.position = this.transform.position;
				dying.transform.localScale = this.scale;
				dying.transform.localScale = scale;
				Destroy (dying, this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length + .3f);
				deathAnimation ();
				RewardGold ();

				Destroy (this.gameObject);
			}
		}
	}

	void Poison(bool value){
		this.isPoisoned = value;
		this.firstPoisonTime = Time.time;
		this.lastPoisonHit = Time.time;

	}

	void SetPoisonDuration(float length){
		this.poisonDuration += length;
	}


	void SetPoisonDPS(int damage){
		this.poisonDPS += damage;
	}

	void PoisonDamage(){
		if (isPoisoned) {
			if (Time.time > lastPoisonHit + 1.0f) {
				TakePoisonDamage (poisonDPS);
				lastPoisonHit = Time.time;
			}

			if (Time.time > poisonDuration + firstPoisonTime) {
				isPoisoned = false;
				poisonDPS = 0;

			}
		}
	}

	void ManageParalysis(){
		if (Time.time > firstParalyzeTime + paralyzeDuration) {
			this.isParalyzed = false;

		}
	}

	void Paralyze(){
		this.isParalyzed = true;

		firstParalyzeTime = Time.time;
		this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}

	void SetParalyzeDuration(float length){
		this.paralyzeDuration = length;
	}



	void RewardGold(){
		GameObject gold = Instantiate (goldDrop);
		gold.SendMessage ("SetValue", (this.maxhealth) + Random.Range(this.maxhealth * -.3f, this.maxhealth * .3f));
		gold.transform.position = this.transform.position;

	}

	void updateHealthBar(){
		Transform healthbar = this.gameObject.transform.Find ("Healthbar");
		Transform greenbar = healthbar.Find ("Greenbar");
		Image green = greenbar.GetComponent<Image> ();
		Vector3 newScale = green.rectTransform.localScale;
		if (maxhealth != 0) {
			newScale.x = (float)((float)health / (float)maxhealth);
			green.rectTransform.localScale = newScale;
		}
	}

	void deathAnimation(){
		shield.transform.position = transform.position;
		sword.transform.position = transform.position;
		shield.transform.localScale = scale;
		sword.transform.localScale = scale;
		Instantiate (shield);
		Instantiate (sword);
	}

	void move(){
		if (rb.velocity.x > movementSpeed && transform.position.y < flyingHeight){
			if (!isParalyzed) {
				rb.AddForce(new Vector2(-35, 0));
			}
		}
	}

	void attack (){
		if ((castle.transform.position - transform.position).magnitude < attackRange) {
			animator.SetBool ("isAttacking", true);
			animator.SetBool ("isWalking", false);

			if (Time.time > (lastAttack + attackCooldown)) {
				castle.SendMessage ("TakeDamage", attackDamage);
				lastAttack = Time.time;
			}
		} else {
			animator.SetBool ("isWalking", true);
			animator.SetBool ("isAttacking", false);

		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
		}
	}


}
