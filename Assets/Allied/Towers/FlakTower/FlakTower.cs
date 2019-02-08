using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakTower : Tower {


    protected override void Initialization()
    {
        level = GameControl.control.flakTowerLevel;
        projectileDamage = 12 + level;
        range = 12 + level;
        canShootAir = true;
        canShootGround = false;
        isRangedTower = true;
        attackSpeed = 4 * Mathf.Pow(.8f, level);
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
            proj.SendMessage("SetDamage", projectileDamage);
            proj.SendMessage("SetTarget", target);
            lastAttackTime = Time.time;
        }
    }
}
