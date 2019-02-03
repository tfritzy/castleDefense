using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FanOfBoltsUpgrade : Upgrade{
	public FanOfBoltsUpgrade(){
		this.name = "Upgrade Fan Of Bolts Ability";
		this.buttonName = "FanOfBolts";
		this.isAbility = true;
		SetDescription ();
	}

	public override void SetDescription(){
		this.description = "> +1 Damage per Bolt\n> +2 Bolts";
		if (GetLevel () == 9) {
			this.description += "\n> The fan makes a second pass";
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
		GameControl.control.fanOfBoltsLevel += 1;
	}

	public override int GetLevel(){
		return GameControl.control.fanOfBoltsLevel;
	}
}