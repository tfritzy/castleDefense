using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : Enemy {

	protected override void SetValues(){
		this.maxSpawnRate = 3f;
	}

	public override int GetHealth(){
		return 10;
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (5 + GameControl.control.gameLevel * 2);
		vals.Add (2);
		return vals;
	}
}
