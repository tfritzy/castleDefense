using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage1 : Enemy{

	protected override void SetValues(){
		this.isRangedAttacker = true;
		this.soulGemDropChance = 5;
	}

	public override int GetHealth(){
		return (int)(10 * Mathf.Pow (1.045f, (float)GameControl.control.gameLevel));
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (5 + GameControl.control.gameLevel * 2);
		vals.Add (2);
		return vals;
	}
}
