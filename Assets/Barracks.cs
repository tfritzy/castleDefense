using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Tower {

    private int soldierAttackDamage;
    private int soldierHealth;
    public GameObject soldier;

    protected override void Initialization()
    {
        level = GameControl.control.barracksLevel;
        attackSpeed = 15f * Mathf.Pow(.9f, level);
        soldierAttackDamage = 2 + level;
        soldierHealth = 300 + level * 30;
        lastAttackTime = -1f * attackSpeed + 3f;
    }

    protected override void ShootProjectile()
    {
        if (Time.time < lastAttackTime + attackSpeed)
            return;
        GameObject inst = Instantiate(soldier, this.transform.position, new Quaternion());
        inst.SendMessage("SetDamage", soldierAttackDamage);
        inst.SendMessage("SetHealth", soldierHealth);
        lastAttackTime = Time.time;
        inst.name = "FriendlyMob";
    }

}
