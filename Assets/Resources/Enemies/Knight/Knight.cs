using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy {

	public override int GetHealth(){
		return 40;
	}

	protected override void SetValues(){
		this.goldMultiplier = 2f;
		this.soulGemDropChance = 4;
		this.maxSpawnRate = 4f;
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (50);
		vals.Add (2);
		return vals;
	}

}
