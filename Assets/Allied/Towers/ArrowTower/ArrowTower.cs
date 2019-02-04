using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower {

    protected override void Initialization() {
        level = GameControl.control.arrowTowerLevel;
        projectileDamage = 5 + level * 3;
        range = 4f + level;
        canShootAir = true;
        canShootGround = true;
        isRangedTower = true;
        attackSpeed = 3f * Mathf.Pow(.95f, level);
        projMovementSpeed = 10f + level * 2f;
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
