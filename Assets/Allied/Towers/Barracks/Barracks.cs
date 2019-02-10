using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Tower {

    private int soldierAttackDamage;
    private int soldierHealth;
    public GameObject soldier;


    public override void Initialization()
    {
        level = GameControl.control.barracksLevel;
        attackSpeed = 15f * Mathf.Pow(.9f, level);
        soldierAttackDamage = 2 + level;
        soldierHealth = 300 + level * 30;
        lastAttackTime = -1f * attackSpeed + 3f;
        towerName = "BarracksTower";
        baseCost = 400;
        towerDescription = "Trains allied soldiers to fight for your caslte. Soldiers only attack ground units.";

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
