using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ExplosiveShotUpgrade : Upgrade{
	public ExplosiveShotUpgrade(){
		this.name = "Upgrade Explosive Shot Ability";
		this.buttonName = "ExplosiveShot";
		this.isAbility = true;
		SetDescription ();
	}

	private void SetDescription(){
		this.description = "> +5 Damage\n> +.1m radius";
		if (GetLevel () == 9) {
			this.description += "\n> Primary Explosion Triggers multiple secondary explosions";
		}
	}

	public override string criterium1(){
		return "";
	}
	public override void Setup(){
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return "";
	}
	public override int GetCost(){
		return GetLevel();
	}
	public override void LevelUp ()
	{
		GameControl.control.explosiveShotLevel += 1;

	}

	public override int GetLevel(){
		return GameControl.control.explosiveShotLevel;
	}
}