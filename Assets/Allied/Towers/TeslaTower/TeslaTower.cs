using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class TeslaTower : Tower {

    private float lastAttackCheckTime;
    private LightningBoltScript bolt;
    private float lastAnimationAttackTime;
    public int boltBounceCount;
    private int currentBounceCount;
    public float boltJumpRange;
    private bool isBoltBouncing;


    protected override void Initialization()
    {
        level = GameControl.control.teslaTowerLevel;
        projectileDamage = 5 + level * 2;
        range = 5 + level;
        canShootAir = true;
        canShootGround = true;
        isRangedTower = true;
        attackSpeed = 6 * Mathf.Pow(.8f, level);
        projMovementSpeed = 0;
        boltBounceCount = 3 + level;
        boltJumpRange = 2 + level / 2f;
        bolt = GetComponent<LightningBoltScript>();
    }

    protected override void ExtraUpdatesFromChildren()
    {
        BounceLightning();
    }

    private void BounceLightning()
    {
        
        if (currentBounceCount >= boltBounceCount)
        {
            isBoltBouncing = false;
            bolt.StartObject = null;
            bolt.EndObject = null;
            return;
        }
        if (!isBoltBouncing)
            return;
        if (Time.time < lastAnimationAttackTime + .1f)
            return;
        bool foundNextTarget = false;
        foreach (Collider2D col in Physics2D.OverlapCircleAll(bolt.EndObject.transform.position, boltJumpRange)){
            if (col.tag == "Enemy" && col.gameObject != bolt.EndObject && !foundNextTarget)
            {
                bolt.StartObject = bolt.EndObject;
                bolt.EndObject = col.gameObject;
                col.SendMessage("TakeDamage", projectileDamage);
                foundNextTarget = true;
                isBoltBouncing = true;
            }
        }
        if (!foundNextTarget)
            currentBounceCount = boltBounceCount;
        else
            currentBounceCount += 1;
        lastAnimationAttackTime = Time.time;
        
    }

    protected override void ShootProjectile()
    {
        
        if (Time.time < lastAttackTime + attackSpeed)
            return;
        if (Time.time < lastAttackCheckTime + .2f)
            return;
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, range);
        foreach (Collider2D hit in hits)
        {
            if (hit.tag == "Enemy" && bolt.StartObject == null &&
               ((Vector2)(hit.transform.position - this.transform.position)).magnitude <= range){
                hit.SendMessage("TakeDamage", projectileDamage);
                bolt.EndObject = hit.gameObject;
                bolt.StartObject = this.gameObject;
                isBoltBouncing = true;
                currentBounceCount = 1;
            }
        }
        lastAttackCheckTime = Time.time;
        lastAnimationAttackTime = Time.time;
        lastAttackTime = Time.time;
    }
}
