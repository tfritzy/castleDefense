using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgradeAttackDamage : Upgrade {

    public ArcherUpgradeAttackDamage()
    {
        this.name = "Attack Damage";
        this.buttonName = "AttackDamage";
        this.description = "Increase Archer Basic Attack Damage by 1";
    }

    public override string criterium1()
    {
        return "";
    }


    public override string criterium2()
    {
        return this.CanPurchase();
    }

    public override string criterium3()
    {
        return "";
    }

    public override int GetCost()
    {
        int y = GetLevel();
        return 10 * (y + 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.archerDamageLevel;
    }

    public override void LevelUp()
    {
        GameControl.control.archerDamageLevel += 1;
    }

}
