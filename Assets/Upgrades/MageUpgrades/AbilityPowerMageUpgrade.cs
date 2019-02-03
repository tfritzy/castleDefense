using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPowerMageUpgrade : Upgrade {

    public AbilityPowerMageUpgrade()
    {
        this.name = "Ability Power";
        this.buttonName = "AbilityPower";
        this.description = "Increase Mage Ability Power by 10%";
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
        return Mathf.Max((int)(.2f * y * y + y), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.mageAbilityPower / 10;
    }

    public override void LevelUp()
    {
        GameControl.control.mageAbilityPower += 10;
    }
}
