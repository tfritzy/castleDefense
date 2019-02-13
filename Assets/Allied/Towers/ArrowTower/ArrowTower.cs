using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowTower : Tower {

    

    public override void Initialization() {
        base.Initialization();
        projectileDamage = 5 + level * 3;
        range = 4f + level;
        canShootAir = true;
        canShootGround = true;
        isRangedTower = true;
        attackSpeed = 3f * Mathf.Pow(.95f, level);
        projMovementSpeed = 10f + level * 2f;
        towerName = "Arrow Tower";
        baseCost = 450;
        towerDescription = "Long range, medium attack speed tower that fires arrows. Can target both air and ground units.";

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
            proj.GetComponent<Rigidbody2D>().velocity = differenceVector * projMovementSpeed;
            proj.GetComponent<Rigidbody2D>().gravityScale = 0f;
            proj.SendMessage("SetDamage", projectileDamage);
            lastAttackTime = Time.time;
        }
    }
}
