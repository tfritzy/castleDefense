using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaDamageUpgrade : Upgrade {

    public BallistaDamageUpgrade()
    {
        this.name = "Damage";
        this.buttonName = "Damage";
        this.description = "Increase Damage of Fully Charged Ballista by 1";
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
        return Mathf.Max((int)(.5f * y * y + 5) * 2, 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.ballistaDamage;
    }

    public override void LevelUp()
    {
        GameControl.control.ballistaDamage += 1;
    }
}
