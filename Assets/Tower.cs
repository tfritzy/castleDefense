using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public int level;
    public bool isRangedTower;
    public bool canShootAir;
    public float range;
    public float attackSpeed;
    public GameObject projectile;

    public GameObject target;
    public float lastTargetCheckTime;
    protected float lastAttackTime;
    protected float projMovementSpeed;
    protected int projectileDamage;

	// Use this for initialization
	void Start () {
        Initialization();
	}
	
	// Update is called once per frame
	void Update () {
        CheckNeedTarget();
        ShootProjectile();
        ExtraUpdatesFromChildren();
    }

    protected virtual void Initialization()
    {

    }

    private void CheckNeedTarget()
    {
        if (target == null && Time.time > lastTargetCheckTime + .5f)
        {
            FindTarget();
            lastTargetCheckTime = Time.time;
        }
    }

    protected virtual void ExtraUpdatesFromChildren()
    {

    }

    protected virtual void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (!canShootAir && enemy.GetComponent<Enemy>().isFlyer)
            {
                continue;
            }
            if (((Vector2)enemy.transform.position - (Vector2)this.transform.position).magnitude <= range){
                target = enemy;
                return;
            }
        }
    }

    protected virtual void ShootProjectile()
    {
        
    }


}
