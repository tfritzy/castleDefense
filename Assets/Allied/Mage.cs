using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour {

	float lastEnemyCheck;

	//firing variables
	public GameObject target;
	public float range;
	public float cooldown;
	float lastShot;
	public GameObject projectile;
	public float arrowVelocity;
	public float damage;
	private List<float> arrows;
	public int nextLevelXP;
	float lastFireCheck;
    Animator mageAnimator;
    private int extraShots;

	// Use this for initialization
	void Start () {

		lastEnemyCheck = Time.time;
		lastShot = Time.time;
		target = null;
        this.arrowVelocity = 20f;
		this.cooldown = 4f / (1f + GameControl.control.mageAttackSpeed/100f);
		this.range = 20f;
        this.mageAnimator = transform.Find("Global_CTRL").gameObject.GetComponent<Animator>();
			
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > lastEnemyCheck + 2.0f) {
			findTarget ();
		}
		fireArrow ();
	}
		

	int getDamage(){
		return 4 + GameControl.control.extraMageDamage + GameControl.control.castleInhabitantBuff; 
	}

    private void Attack()
    {

        // If the target has dissapeared, Don't attack.
        if (target == null)
        {
            return;
        }

        Vector2 startPos = (Vector2)this.transform.position;
        GameObject proj = Instantiate(projectile);

        proj.transform.position = startPos;

        // Set Arrow damage
        proj.SendMessage("SetDamage", getDamage());

        AudioManager.manager.Play("mageFire");

        // Set the travel direction of the projectile
        Vector2 diff = target.transform.position - this.transform.position;
        diff = (diff / diff.magnitude) * this.arrowVelocity;
        proj.GetComponent<Rigidbody2D>().velocity = diff;

        //reset cooldown
        lastShot = Time.time;

    }

	private void fireArrow(){
		if (target != null) {

            // Start the attack animation. The animator handles the fire 
            // logic and sends a message back here calling the Attack() method
			if (Time.time > lastShot + cooldown || extraShots > 0) {

                this.mageAnimator.SetTrigger("attack");
                this.lastShot = Time.time;
                this.extraShots -= 1;
                if (Random.Range(0,100) < GameControl.control.extraMageProjectileChance)
                {
                    extraShots = 1;
                }
            }

		}
	}

	private void findTarget(){

		GameObject[] targetPotentials = GameObject.FindGameObjectsWithTag ("Enemy");
		if (targetPotentials.Length > 0) {
			GameObject closest = targetPotentials [0];
			float closestDist = ((Vector2)targetPotentials [0].transform.position - (Vector2)this.transform.position).magnitude;
			for (int i = 0; i < targetPotentials.Length; i++) {
				float dist = ((Vector2)targetPotentials [i].transform.position - (Vector2)this.transform.position).magnitude;
				if (dist < closestDist) {
					closestDist = dist;
					closest = targetPotentials [i];
				}
			}
			if (closestDist <= this.range) {
				this.target = closest;
			} else {
				this.target = null;
			}
		} else {
			target = null;
		}
	}
}
