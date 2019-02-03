using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FireWaveUpgrade : Upgrade{
	public FireWaveUpgrade(){
		this.name = "Upgrade Fire Wave Ability";
		this.buttonName = "FireWave";
		this.isAbility = true;
		SetDescription ();
	}

	public override void SetDescription(){
		this.description = "> +5 Damage";
		if (GetLevel () == 9) {
			this.description += "\n> A second wave moves vertically!";
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
		GameControl.control.fireWaveLevel += 1;
	}

	public override int GetLevel(){
		return GameControl.control.fireWaveLevel;
	}
}