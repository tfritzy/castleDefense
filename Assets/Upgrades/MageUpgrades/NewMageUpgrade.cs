using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMageUpgrade : Upgrade {

    public NewMageUpgrade()
    {
        this.name = "New Mage";
        this.buttonName = "NewMage";
        this.description = "Get Another Mage";
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
        int y = GameControl.control.mageCount;
        return y * y * 25 + 5;
    }

    public override int GetLevel()
    {
        return GameControl.control.mageCount;
    }

    public override void LevelUp()
    {
        GameControl.control.mageCount += 1;
    }
}
