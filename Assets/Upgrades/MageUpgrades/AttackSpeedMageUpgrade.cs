using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedMageUpgrade : Upgrade {

    public AttackSpeedMageUpgrade()
    {
        this.name = "Attack Speed";
        this.buttonName = "AttackSpeed";
        this.description = "Increase Mage's Attack Speed by 5%";
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
        int y = GameControl.control.mageAttackSpeed / 5;
        return Mathf.Max((int)(.2f * y * y + y), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.mageAttackSpeed / 5;
    }

    public override void LevelUp()
    {
        GameControl.control.mageAttackSpeed += 5;
    }
}
