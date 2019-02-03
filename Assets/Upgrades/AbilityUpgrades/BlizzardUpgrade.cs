using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BlizzardUpgrade : Upgrade{
	public BlizzardUpgrade(){
		this.name = "Upgrade Blizzard Ability";
		this.buttonName = "Blizzard";
		this.isAbility = true;
		SetDescription ();
	}

	public override void SetDescription(){
		this.description = "> +1 Damage per Ice Spike\n> +8 Damage to Main Body";
		if (GetLevel () == 9) {
			this.description += "\n> Blizzard fires twice as many projectiles";
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
		GameControl.control.blizzardAbilityLevel += 1;
	}

	public override int GetLevel(){
		return GameControl.control.blizzardAbilityLevel;
	}
}