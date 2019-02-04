using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorrentTower : Tower {

    protected override void Initialization()
    {
        level = GameControl.control.torrentTowerLevel;
        projectileDamage = 10 + level;
        range = 6 + level;
        canShootAir = false;
        canShootGround = true;
        isRangedTower = true;
        attackSpeed = 1 * Mathf.Pow(.8f, level);
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
