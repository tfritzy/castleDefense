using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgradeExtraArrow : Upgrade {

    public ArcherUpgradeExtraArrow()
    {
        this.name = "Extra";
        this.buttonName = "ExtraArrow";
        this.description = "Increase Chance That Archer Fires an Extra Arrow by 3%";
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
        return Mathf.Max((int)(.4f * y * y), 2);
    }

    public override int GetLevel()
    {
        return GameControl.control.archerExtraArrowChance / 3;
    }

    public override void LevelUp()
    {
        GameControl.control.archerExtraArrowChance += 3;
    }
}
