using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Enemy{
	public override int GetHealth(){
		return 5;
	}

	protected override void SetValues(){
		this.goldMultiplier = .5f;
		this.soulGemDropChance = .5f;
		this.maxSpawnRate = 1f;
    }

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (20);
		vals.Add (3);
		return vals;
	}

}
