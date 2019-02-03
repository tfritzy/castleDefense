using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageMageUpgrade : Upgrade {

    public AttackDamageMageUpgrade()
    {
        this.name = "Attack Damage";
        this.buttonName = "AttackDamage";
        this.description = "Increase Mage Attack Damage by 2";
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
        int y = GameControl.control.extraMageDamage;
        return Mathf.Max((int)((.18f * y * y + y * 4)), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.extraMageDamage / 2;
    }

    public override void LevelUp()
    {
        GameControl.control.extraMageDamage += 2;
    }
}
