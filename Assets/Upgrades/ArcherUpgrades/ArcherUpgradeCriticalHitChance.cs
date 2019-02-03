using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgradeCriticalHitChance : Upgrade {

    public ArcherUpgradeCriticalHitChance()
    {
        this.name = "Crit Hit Chance";
        this.buttonName = "CritChance";
        this.description = "Increase Archer Crit Hit Chance by 1%";
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
        float y = GameControl.control.archerCritChance / 100f;
        return Mathf.Max((int)((y * y) / .005f), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.archerCritChance;
    }

    public override void LevelUp()
    {
        GameControl.control.archerCritChance += 1;
    }
}
