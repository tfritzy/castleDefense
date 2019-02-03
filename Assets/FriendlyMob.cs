using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyMob : MonoBehaviour {

    public int damage;
    public int health;

    private GameObject target;
    private float range = .5f;
    private Collider2D targetCollider;
    private Collider2D thisCollider;
    private float lastAttackTime;
    private float attackSpeed;
    private float lastTargetCheckTime;
    private Animator animator;
    private float rightSideOfScreen;

	// Use this for initialization
	void Start () {
        thisCollider = this.GetComponent<Collider2D>();
        this.attackSpeed = 2f;
        this.animator = this.transform.Find("Global_CTRL").GetComponent<Animator>();
        this.rightSideOfScreen = Camera.main.ScreenToWorldPoint(
                                new Vector2(Camera.main.pixelWidth, 0)).x;
    }
	
	// Update is called once per frame
	void Update () {
        FindTarget();
        AttackAnimation();
	}

    void Attack()
    {
        if (target == null)
            return;
        target.SendMessage("TakeDamage", damage);
    }

    void AttackAnimation()
    {
        if (target == null || targetCollider == null)
        {
            animator.SetBool("isAttacking", false);
            return;
        }
            
        Vector2 difference = target.transform.position - this.transform.position;
        float dist = targetCollider.Distance(thisCollider).distance;
        if (dist > range)
        {
            animator.SetBool("isAttacking", false);
        } else
        {
            animator.SetBool("isAttacking", true);
        }
        if (Time.time > lastAttackTime + attackSpeed &&  dist <= range)
        {
            lastAttackTime = Time.time;
        }
        if (target.transform.position.x >= this.transform.position.x)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x) * -1;
            transform.localScale = currentScale;
        } else
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 2f);
    }

    void FindTarget()
    {
        if (target != null)
            return;
        if (Time.time > lastTargetCheckTime + 1f)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript.isFlyer)
                    continue;
                if (enemy.transform.position.x >= rightSideOfScreen)
                    continue;
                target = enemy;
                targetCollider = target.GetComponent<Collider2D>();
                return;
            }
            lastTargetCheckTime = Time.time;
        }
    }

    void SetHealth(int health)
    {
        this.health = health;
    }

    void SetDamage(int damage)
    {
        this.damage = damage;
    }

    void TakeDamage(int amount)
    {
        this.health -= amount;
        if (this.health <= 0)
        {
            this.animator.SetBool("isDead", true);
            this.GetComponent<MonoBehaviour>().enabled = false;
            Destroy(this.GetComponent<Collider2D>());
        }
    }

    void SetLastAttacker()
    {
        return;
    }


}
