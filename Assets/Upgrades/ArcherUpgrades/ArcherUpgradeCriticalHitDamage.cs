using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgradeCriticalHitDamage : Upgrade {

    public ArcherUpgradeCriticalHitDamage()
    {
        this.name = "Crit Hit Damage";
        this.buttonName = "CritDamage";
        this.description = "Increase Archer Crit Damage Chance by 10%";
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
        return y * 5;
    }

    public override int GetLevel()
    {
        return GameControl.control.archerCritDamage / 10;
    }

    public override void LevelUp()
    {
        GameControl.control.archerCritDamage += 10;
    }
}
