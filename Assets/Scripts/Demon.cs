using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy {

    public Demon()
    {
        this.isFlyer = true;

    }

    public override int GetHealth()
    {
        return 20;
    }

    protected override void SetValues()
    {
        this.goldMultiplier = 1.5f;
        this.soulGemDropChance = .5f;
        this.isRangedAttacker = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        this.isFlyer = true;
    }

    public override List<int> GetAttack()
    {
        List<int> vals = new List<int>();
        vals.Add(50);
        vals.Add(2);
        return vals;
    }

}
