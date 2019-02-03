using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgradeAttackSpeed : Upgrade {

    public ArcherUpgradeAttackSpeed()
    {
        this.name = "Attack Speed";
        this.buttonName = "AttackSpeed";
        this.description = "Increase Archer Attack Speed by 5%";
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
        return Mathf.Max(GetLevel() * 8, 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.archerAttackSpeed / 5;
    }

    public override void LevelUp()
    {
        GameControl.control.archerAttackSpeed += 5;
    }
}
