using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaAdditionalBoltUpgrade : Upgrade {

    public BallistaAdditionalBoltUpgrade()
    {
        this.name = "Additional Bolt";
        this.buttonName = "AdditionalBolt";
        this.description = "Ballista Fires an extra Bolt per Firing";
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
        return Mathf.Max((int)(y * y + 1) * 250 + 100, 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.extraBallistaArrows;
    }

    public override void LevelUp()
    {
        GameControl.control.extraBallistaArrows += 1;
    }
}
