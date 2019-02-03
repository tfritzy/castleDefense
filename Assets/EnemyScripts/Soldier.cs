using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : Enemy {
	public override int GetHealth(){
		return 12;
	}

	protected override void SetValues(){
		this.goldMultiplier = .75f;
		this.soulGemDropChance = 1;
		this.isRangedAttacker = true;
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (30);
		vals.Add (2);
		return vals;
	}

}
