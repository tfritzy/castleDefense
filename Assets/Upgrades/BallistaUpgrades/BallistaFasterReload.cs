using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaFasterReloadUpgrade : Upgrade {

    public BallistaFasterReloadUpgrade()
    {
        this.name = "Ballista Faster Reload";
        this.buttonName = "FasterReload";
        this.description = "Increase Reload Speed by 5%";
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
        return Mathf.Max((int)(.3f * y * y), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.ballistaCooldown;
    }

    public override void LevelUp()
    {
        GameControl.control.ballistaCooldown += 1;
    }
}
