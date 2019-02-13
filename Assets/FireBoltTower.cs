using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoltTower : Tower {

    private float projExplodeRadius;

    public override void Initialization()
    {
        base.Initialization();
        projectileDamage = 15 + level * 4;
        range = 7 + level;
        canShootAir = true;
        canShootGround = true;
        isRangedTower = true;
        attackSpeed = 6 * Mathf.Pow(.95f, level);
        projMovementSpeed = 25f;
        projExplodeRadius = Mathf.Min(.5f + level / 4, 4f);
        towerName = "FireBoltTower";
        baseCost = 600;
        towerDescription = "Shoots fire bolts that have small explosions on impact. Can attack both air and ground units";

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
            differenceVector += target.GetComponent<Rigidbody2D>().velocity * differenceVector.magnitude / projMovementSpeed;
            differenceVector = differenceVector / differenceVector.magnitude;
            proj.GetComponent<Rigidbody2D>().velocity = differenceVector * projMovementSpeed;
            proj.GetComponent<Rigidbody2D>().gravityScale = 0f;
            proj.SendMessage("SetDamage", projectileDamage);
            proj.SendMessage("SetRadius", projExplodeRadius);
            lastAttackTime = Time.time;
        }
    }
}
