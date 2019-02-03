using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegesus : Enemy {


	public override int GetHealth(){
		return 150;
	}

	protected override void SetValues(){
		this.goldMultiplier = 3f;
		this.soulGemDropChance = 5;
		this.isRangedAttacker = true;
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (50); // attack Damage
		vals.Add (1);  // cooldown
		return vals;
	}
}
