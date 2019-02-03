using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaBleedChanceUpgrade : Upgrade {

    public BallistaBleedChanceUpgrade()
    {
        this.name = "Bleed";
        this.buttonName = "Bleed";
        this.description = "Increase chance to inflict bleed by 5% (inflict 50% damage over 6 seconds)";
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
        return Mathf.Max((int)(.2f * y * y) * 5, 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.bleedChance/5;
    }

    public override void LevelUp()
    {
        GameControl.control.bleedChance += 5;
    }
}
