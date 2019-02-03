using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Enemy {

	public override int GetHealth(){
		return 150;
	}

	protected override void SetValues(){
		this.goldMultiplier = 2f;
		this.soulGemDropChance = 4;
		this.isRangedAttacker = true;
		this.maxSpawnRate = 3f;
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (50);
		vals.Add (3);
		return vals;
	}
}
