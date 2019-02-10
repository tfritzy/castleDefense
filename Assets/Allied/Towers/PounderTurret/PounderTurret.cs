using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounderTurret : Tower {

    private Animator animator;

    public override void Initialization()
    {
        level = GameControl.control.pounderTowerLevel;
        projectileDamage = 4 + level * 2;
        range = 3 + level / 3;
        canShootAir = false;
        isRangedTower = false;
        attackSpeed = 7 * Mathf.Pow(.8f, level);
        animator.speed = 7 / attackSpeed;
        towerName = "PounderTower";
        baseCost = 500;
        towerDescription = "Pounds the ground inflicting heavy damage on nearby enemies. Has very slow attack speed and cannot target air units.";

    }

    // Doesn't need to fire projectiles.
    protected override void ShootProjectile()
    {
        return;
    }

    public void Pound()
    {
        if (animator == null)
            animator = this.GetComponent<Animator>();
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
