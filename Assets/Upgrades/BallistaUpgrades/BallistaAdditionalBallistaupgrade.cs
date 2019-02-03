using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaAdditionalBallistaUpgrade : Upgrade {

    public BallistaAdditionalBallistaUpgrade()
    {
        this.name = "Additional Ballista";
        this.buttonName = "AdditionalBallista";
        this.description = "Purchase an Additional Ballista";
    }

    public override string criterium1()
    {
        if (GameControl.control.ballistaCount < 3)
        {
            return "";
        } else
        {
            return "You have the maximum number of ballistas";
        }
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
        return Mathf.Max((int)(y * y + 2) * 200 + 200, 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.ballistaCount;
    }

    public override void LevelUp()
    {
        GameControl.control.ballistaCount+= 1;
    }
}
