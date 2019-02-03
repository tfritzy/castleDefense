using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomber : Enemy {

	protected override void SetValues(){
		this.goldMultiplier = 1f;
		this.soulGemDropChance = 3;
		this.maxSpawnRate = 3f;
	}

	public override int GetHealth(){
		return 20;
	}

	public override List<int> GetAttack(){
		List<int> vals = new List<int> ();
		vals.Add (500);
		vals.Add (0);
		return vals;
	}

	protected override void attack(){
        float distance = target.GetComponent<Collider2D>().Distance(this.GetComponent<Collider2D>()).distance;
		if (distance <= attackRange) {
			target.SendMessage ("TakeDamage", this.attackDamage);
            this.transform.Find("Global_CTRL").GetComponent<Animator>().SetBool("isDead", true);
            this.attackDamage = 0;
		}
	}


}
