using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaAdditionalPierceUpgrade : Upgrade {

    public BallistaAdditionalPierceUpgrade()
    {
        this.name = "Additional Pierce";
        this.buttonName = "AdditionalPierce";
        this.description = "Ballista Bolts Pierce an Additonal Enemy";
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
        return Mathf.Max((int)(y * y+1) * 50, 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.pierceLevel;
    }

    public override void LevelUp()
    {
        GameControl.control.pierceLevel += 1;
    }
}
