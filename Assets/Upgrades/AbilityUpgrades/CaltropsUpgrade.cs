using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CaltropsUpgrade : Upgrade{
	public CaltropsUpgrade(){
		this.name = "Upgrade Caltrops Ability";
		this.buttonName = "Caltrops";
		this.isAbility = true;
		SetDescription ();
	}

	public override void SetDescription(){
		this.description = "> +1 Damage per Caltrop\n> +1 Caltrops";
		if (GetLevel () == 9) {
			this.description += "\n> Caltrops poison enemies";
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
		GameControl.control.caltropsLevel += 1;
	}

	public override int GetLevel(){
		return GameControl.control.caltropsLevel;
	}
}