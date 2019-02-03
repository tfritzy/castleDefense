using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMage : Enemy
{

    public IceMage()
    {
        this.isFlyer = false;

    }

    public override int GetHealth()
    {
        return 25;
    }

    protected override void SetValues()
    {
        this.goldMultiplier = 2f;
        this.soulGemDropChance = 1.5f;
        this.isRangedAttacker = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        this.isFlyer = true;
        this.rangedProjStartLocation = new Vector3(-.2f, 1f, -.2f);
    }

    public override List<int> GetAttack()
    {
        List<int> vals = new List<int>();
        vals.Add(75);
        vals.Add(2);
        return vals;
    }

}
