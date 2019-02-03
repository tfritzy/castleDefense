using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMageUpgrade : Upgrade {

    public SplashMageUpgrade()
    {
        this.name = "Splash Raidus";
        this.buttonName = "SplashRadius";
        this.description = "Increase Mage Basic Attack Splash Radius by .2m";
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
        int y = GameControl.control.mageSplashRadius;
        return Mathf.Max((int)(.8f * y * y + 5), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.mageSplashRadius;
    }

    public override void LevelUp()
    {
        GameControl.control.mageSplashRadius += 1;
    }
}
