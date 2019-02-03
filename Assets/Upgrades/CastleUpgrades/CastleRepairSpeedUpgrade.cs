using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleRepairSpeedUpgrade : Upgrade {

    public CastleRepairSpeedUpgrade()
    {
        this.name = "Repair Speed";
        this.buttonName = "RepairSpeed";
        this.description = "Increase Castle Repair Speed by 2 Health/Second";
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
        return Mathf.Max((int)(.2f * y * y), 1);
    }

    public override int GetLevel()
    {
        return GameControl.control.repairSpeed/2;
    }

    public override void LevelUp()
    {
        GameControl.control.repairSpeed += 2;
    }
}
