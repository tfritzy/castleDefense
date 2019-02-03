using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealthUpgrade : Upgrade {

    public CastleHealthUpgrade()
    {
        this.name = "Health";
        this.buttonName = "Health";
        this.description = "Increase Castle Health by 100";
    }

    public override void ExtraUISetup()
    {
        GameObject.Find("Castle").SendMessage("Start");
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
        return Mathf.Max((int)(.1f * y * y), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.castleHealth / 100;
    }

    public override void LevelUp()
    {
        GameControl.control.castleHealth += 100;
    }
}
