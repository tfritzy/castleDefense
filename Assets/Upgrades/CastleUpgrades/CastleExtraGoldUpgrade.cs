using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleExtraGoldUpgrade : Upgrade {

    public CastleExtraGoldUpgrade()
    {
        this.name = "Extra Gold";
        this.buttonName = "ExtraGold";
        this.description = "Increase Gold Dropped by Enemies by 5%";
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
        return Mathf.Max((int)(.3f * y * y), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.extraGoldPercentage / 5;
    }

    public override void LevelUp()
    {
        GameControl.control.extraGoldPercentage += 5;
    }
}
