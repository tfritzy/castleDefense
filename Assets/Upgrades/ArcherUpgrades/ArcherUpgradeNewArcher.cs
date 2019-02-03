using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgradeNewArcher : Upgrade {

    public ArcherUpgradeNewArcher()
    {
        this.name = "New Archer";
        this.buttonName = "NewArcher";
        this.description = "Acquire An Additional Archer";
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
        int y = GameControl.control.archerCount;
        if (y == 0)
        {
            return 10;
        }
        return y * 10;
    }

    public override int GetLevel()
    {
        return GameControl.control.archerCount;
    }

    public override void LevelUp()
    {
        GameControl.control.archerCount += 1;

    }
}
