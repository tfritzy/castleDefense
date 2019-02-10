using UnityEngine;
using System;

public class Tower : MonoBehaviour {

    public int level;
    public bool isRangedTower;
    public bool canShootAir;
    public bool canShootGround;
    public float range;
    public float attackSpeed;
    public GameObject projectile;
    public GameObject target;
    public float lastTargetCheckTime;
    public int slot;
    public String towerName;
    public int baseCost;
    public int projectileDamage;
    public String towerDescription;

    protected float lastAttackTime;
    protected float projMovementSpeed;
    protected int levelUpCost;


    // Use this for initialization
    void Start () {
        this.slot = UnityEngine.Random.Range(0, 6);
        Initialization();
	}
	
	// Update is called once per frame
	void Update () {
        CheckNeedTarget();
        ShootProjectile();
        ExtraUpdatesFromChildren();
    }

    public virtual void Initialization()
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
            if (!canShootGround && !enemy.GetComponent<Enemy>().isFlyer)
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

    public int GetCost()
    {
        int otherTowerCount = 0;
        for (int i = 0; i < GameControl.control.towers.Length; i++)
        {
            if (GameControl.control.towers[i] == towerName)
            {
                otherTowerCount += 1;
            }
        }
        return this.baseCost + baseCost * otherTowerCount;
    }

    public bool Purchase(int slot)
    {
        this.slot = slot;
        SaveState();
        return true;
    }

    public bool LevelUp()
    {
        level += 1;
        GameControl.control.gold -= baseCost;
        SaveState();
        return true;
    }

    protected void SaveState()
    {
        GameControl.control.towers[slot] = name;
        GameControl.control.towerLevels[slot] = level;
        GameControl.control.save();
    }


}
