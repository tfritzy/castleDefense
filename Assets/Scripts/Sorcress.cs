using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcress : Enemy {
	
    public Sorcress()
    {
        this.isFlyer = true;

    }

	public override int GetHealth(){
		return 40;
	}

	protected override void SetValues(){
		this.goldMultiplier = 2.25f;
		this.soulGemDropChance = 2;
		this.isRangedAttacker = true;
		this.GetComponent<Rigidbody2D> ().gravityScale = 0f;
        this.isFlyer = true;
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (100);
		vals.Add (2);
		return vals;
	}

}
