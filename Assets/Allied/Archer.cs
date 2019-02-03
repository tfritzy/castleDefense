using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Archer : MonoBehaviour {

	float lastEnemyCheck;

	//Floating text Variables
	public GameObject floatingText;

	//firing variables
	public GameObject target;
	public float range;
	public float cooldown;
	float lastShot;

	public GameObject projectile;
	public GameObject poisonArrow;
	public GameObject paralyzeArrow;

	public float arrowVelocity;
	public int damage;
	private List<float> arrows;
	public int nextLevelXP;
	float lastFireCheck;
	float lastFireTime;

	private int poisonArrowChanceFactor = 5;
	private int paralysisArrowChanceFactor = 5;
	private int basePoisonArrowChance = 10; 
	private float poisonDurationFactor = .25f;
	private int baseParalysisArrowChance = 10;
	private float paralysisDurationFactor = 1.0f;
	private int extraArrowsNeeded;

	public bool isUnlocked;

    private Animator archerAnimator;
    private float archerAttackSpeed;

	// Use this for initialization
	void Start () {
		
		this.cooldown = (8f / (1.0f + GameControl.control.archerAttackSpeed / 100f));
		this.range = 20;
        this.damage = GetDamage();
		lastEnemyCheck = Time.time;
		lastShot = Time.time;
		target = null;

        // store the animator as the local variable.
        this.archerAnimator = this.transform.Find("Global_CTRL").gameObject.GetComponent<Animator>();

	}
	// Update is called once per frame
	void Update () {
		if (Time.time > lastEnemyCheck + 2.0f || this.target == null) {
			findTarget ();
		}

		if (Time.time > lastFireTime + .5f && this.extraArrowsNeeded > 0) {
            Attack();
            
			extraArrowsNeeded -= 1;
		}

        if (target != null && Time.time > lastFireTime + cooldown)
        {
            archerAnimator.SetTrigger("Attack");
            lastFireTime = Time.time;

        }
	}

	int GetDamage(){
        return GameControl.control.archerDamageLevel + 1 + (GameControl.control.castleInhabitantBuff / 4);
	}

    private void Attack() {


        int fireExtra = Random.Range(0, 100);

        if (fireExtra <= GameControl.control.archerExtraArrowChance) {
            Debug.Log("Archer needs to fire an extra");
            fireExtra = 1;
        }
        if (target == null)
        {
            return;
        }
        Enemy targetEnemy = target.GetComponent<Enemy>();
        if (targetEnemy == null)
        {
            return;
        }

        List<GameObject> targetPotentials = new List<GameObject>();
        GameObject[] allObjs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in allObjs)
        {
            if (((Vector2)obj.transform.position - (Vector2)this.transform.position).magnitude < this.range)
            {
                targetPotentials.Add(obj);
            }
        }

        // Choose random target half the time.
        if (Random.Range(0, 2) == 1)
        {
            if (targetPotentials.Count > 0)
            {
                this.target = targetPotentials[Random.Range(0, targetPotentials.Count - 1)];
            }

        }

        float movementSpeed = targetEnemy.movementSpeed;
		float v = 35;
        
		float y0 = this.transform.position.y;
		float y = target.transform.position.y;
		float d = target.transform.position.x - this.transform.position.x;
		float g = -9.81f;

        /*

        float theta;

        float quadA = (-1 * g * d * d)/(2 * v * v);
        float quadB = d/v;
        float quadC = (g * d * d)/(2 * v * v)+(y0 - y);

        float tanTheta = (-1 * quadB + Mathf.Sqrt(quadB * quadB - 4 * quadA * quadC)) / (2 * quadA);
        
        theta = Mathf.Atan(tanTheta);
        */
        float theta = Mathf.Atan((y - y0)/d);
        theta += 0.004363325f * d;
        GameObject proj;
		int extraArrows;

        AudioManager.manager.Play("archerFire");

		proj = Instantiate (projectile);
		
        Vector3 newPos = this.transform.position;
        newPos.z = 0;
        proj.transform.position = newPos;
        Vector2 newVelocity = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        newVelocity = newVelocity / newVelocity.magnitude;
        proj.GetComponent<Rigidbody2D>().velocity = newVelocity * v;
        //proj.GetComponent<Rigidbody2D>().velocity = new Vector2(v * Mathf.Cos(theta), v * Mathf.Sin(theta));
        proj.SendMessage("SetDamage", GetDamage());
		lastShot = Time.time;
		lastFireTime = Time.time;

	}


    private void findTarget()
    {
        GameObject[] targetPotentials = GameObject.FindGameObjectsWithTag("Enemy");


        if (targetPotentials.Length > 0)
        {
            GameObject closest = targetPotentials[0];
            float closestDist = ((Vector2)targetPotentials[0].transform.position - (Vector2)this.transform.position).magnitude;
            for (int i = 0; i < targetPotentials.Length; i++)
            {
                float dist = ((Vector2)targetPotentials[i].transform.position - (Vector2)this.transform.position).magnitude;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = targetPotentials[i];
                }
            }



            if (closestDist <= this.range)
            {
                this.target = closest;
            }
            else
            {
                this.target = null;
            }
        }
        else
        {
            target = null;
        }



    }

}
