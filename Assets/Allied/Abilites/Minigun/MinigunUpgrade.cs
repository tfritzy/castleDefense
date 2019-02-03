using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MinigunUpgrade : Upgrade{
	public MinigunUpgrade(){
		this.name = "Upgrade Minigun Ability";
		this.buttonName = "Minigun";
		this.isAbility = true;
		SetDescription ();
	}

	public override void SetDescription(){
		this.description = "> +5 Damage";
		if (GetLevel () == 9) {
			this.description += "\n> Arrows Explode!";
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
		GameControl.control.minigunLevel += 1;
	}

	public override int GetLevel(){
		return GameControl.control.minigunLevel;
	}
}