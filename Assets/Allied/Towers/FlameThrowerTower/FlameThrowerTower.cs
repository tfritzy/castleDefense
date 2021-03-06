﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTower : Tower {

    private float flameDamage;

    public override void Initialization()
    {
        base.Initialization();
        flameDamage = .5f + level / 4f;
        range = Mathf.Min(4f + level/2, 7);
        canShootAir = false;
        canShootGround = true;
        isRangedTower = true;
        attackSpeed = .1f * Mathf.Pow(.95f, level);
        projMovementSpeed = 8f + level * 2f;
        towerName = "FlameThrowerTower";
        baseCost = 800;
        towerDescription = "Produces a beam of fire towards enemies within a short range tower that has high DPS. Can only target ground units.";

    }

    protected override void ShootProjectile()
    {
        if (target == null)
        {
            return;
        }
        if ((target.transform.position - this.transform.position).magnitude > range)
        {
            target = null;
            return;
        }
        if (Time.time > lastAttackTime + attackSpeed)
        {
            GameObject proj = Instantiate(projectile, this.transform.position, new Quaternion());
            Vector2 differenceVector = target.transform.position - this.transform.position;
            differenceVector = differenceVector / differenceVector.magnitude;
            proj.GetComponent<Rigidbody2D>().AddForce(differenceVector * projMovementSpeed);
            proj.GetComponent<Rigidbody2D>().gravityScale = 0f;
            int realDamage = Mathf.FloorToInt(flameDamage);
            float leftOver = flameDamage - realDamage;
            realDamage += Random.Range(0, 1) <= leftOver ? 1 : 0;
            Debug.Log(realDamage);
            proj.SendMessage("SetDamage", realDamage);
            lastAttackTime = Time.time;
        }
    }

}
