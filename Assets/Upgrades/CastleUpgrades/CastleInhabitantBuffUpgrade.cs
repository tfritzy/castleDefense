using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleInhabitantBuffUpgrade : Upgrade {

    public CastleInhabitantBuffUpgrade()
    {
        this.name = "Inhabitant Buff";
        this.buttonName = "InhabitantBuff";
        this.description = "Increase Damage of All Castle Inhabitants by 1";
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
        return Mathf.Max((int)(.4f * y * y), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.castleInhabitantBuff;
    }

    public override void LevelUp()
    {
        GameControl.control.castleInhabitantBuff += 1;
    }
}
