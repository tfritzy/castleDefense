using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorrentTower : Tower {

    public override void Initialization()
    {
        base.Initialization();
        projectileDamage = 10 + level;
        range = 6 + level;
        canShootAir = false;
        canShootGround = true;
        isRangedTower = true;
        attackSpeed = 1 * Mathf.Pow(.8f, level);
        towerName = "TorrentTower";
        baseCost = 450;
        towerDescription = "Fires arcane bolts into the air that come crashing down upon enemies. Deals high damage within small explosion radius. Can only target ground units.";

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
