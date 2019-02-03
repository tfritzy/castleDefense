using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dragon : MonoBehaviour {

	//health variables
	public int health;
	public int maxhealth;
	public float healthFactor;
	public GameObject damageText;
	Rigidbody2D rb;

	//attack variables
	public float attackRange;
	public float attackCooldown;
	private float lastAttack;
	public int attackDamage;
	private bool isAttacking;
	public GameObject projectile;

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


	public bool isGiant = false;
	public bool isDead = false;

	//enemy variables
	public GameObject castle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		//rb.position = startPos;
		GameObject manager = GameObject.Find ("Manager");
		maxhealth = (int)Mathf.Pow(GameControl.control.gameLevel * 12f, 1.2f) + 50;
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

		if (Time.time > birthTime + 320.0f) {
			Destroy (this.gameObject);
		}

	}

	void TakeDamage(int damage){

		//makePopupText
		GameObject text = Instantiate (damageText);
		text.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		text.transform.position = transform.position;
		text.GetComponent<Text> ().text = "-" + damage.ToString ();

		health -= damage;
		updateHealthBar ();
		if (health <= 0 && !isDead) {
			isDead = true;
			/*
			GameObject dying = Instantiate (dyingAnimation);
			dying.transform.position = this.transform.position;
			dying.transform.localScale = this.scale;
			dying.transform.localScale = scale;
			Destroy (dying, this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length + .3f);
			*/

			RewardGold ();

			Destroy (this.gameObject);
		}
	}


	void RewardGold(){
		GameObject gold = Instantiate (goldDrop);
		gold.SendMessage ("SetValue", (this.maxhealth / 10)*2.0f + Random.Range(this.maxhealth/10 * -.3f, this.maxhealth/10 * .3f));
		gold.transform.position = this.transform.position;

	}

	void updateHealthBar(){
		Transform healthbar = this.gameObject.transform.Find ("Healthbar");
		Transform greenbar = healthbar.Find ("Greenbar");
		Image green = greenbar.GetComponent<Image> ();
		Vector3 newScale = green.rectTransform.localScale;
		newScale.x = (float)((float)health / (float)maxhealth);
		green.rectTransform.localScale = newScale;
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
		if (isAttacking) {
			rb.velocity = new Vector2(0,0);
		} else {
			rb.velocity = new Vector2(movementSpeed, Mathf.PingPong(Time.time, 2.0f)-1.0f);
		}

	}

	void attack (){
		if ((castle.transform.position - transform.position).magnitude < attackRange) {
			animator.SetBool ("isFlying", false);
			animator.SetBool ("isStalling", true);
			if (Time.time > lastAttack + .1f){
				animator.SetBool("isAttacking", false);
			}
			this.isAttacking = true;
			Debug.Log ("Hitting");
			if (Time.time > (lastAttack + attackCooldown)) {
				animator.SetBool ("isAttacking", true);
				
				GameObject proj = Instantiate (this.projectile);
				proj.transform.position = this.transform.position;
				Vector2 difference = this.castle.transform.position - this.transform.position;
				difference = difference / difference.magnitude;
				proj.SendMessage ("SetDamage", attackDamage);
				proj.GetComponent<Rigidbody2D> ().velocity = difference*8.0f;
				lastAttack = Time.time;
			}
		} else {
			animator.SetBool ("isFlying", true);
			animator.SetBool ("isAttacking", false);

		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
		}
	}
}
