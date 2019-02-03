using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMageUpgrade : Upgrade {

    public SlowMageUpgrade()
    {
        this.name = "Slow";
        this.buttonName = "Slow";
        this.description = "Increase Mage's Slow Effect by 3%";
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
        return GameControl.control.mageSlow / 3;
    }

    public override void LevelUp()
    {
        GameControl.control.mageSlow += 3;
    }
}
