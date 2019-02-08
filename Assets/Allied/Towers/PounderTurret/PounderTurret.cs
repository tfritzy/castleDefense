using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounderTurret : Tower {

    private Animator animator;

    protected override void Initialization()
    {
        level = GameControl.control.pounderTowerLevel;
        projectileDamage = 4 + level * 2;
        range = 2 + level / 3;
        canShootAir = false;
        isRangedTower = false;
        attackSpeed = 7 * Mathf.Pow(.8f, level);
        animator = this.GetComponent<Animator>();
        animator.speed = 7 / attackSpeed;
    }

    // Doesn't need to fire projectiles.
    protected override void ShootProjectile()
    {
        return;
    }

    public void Pound()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, range);
        foreach (Collider2D hit in hits)
        {
            if (hit.tag != "Enemy")
                continue;
            if (hit.GetComponent<Enemy>().isFlyer)
                continue;
            hit.SendMessage("TakeDamage", projectileDamage);
        }
    }
}
