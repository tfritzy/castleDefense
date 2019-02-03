using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DefensiveBarrierUpgrade : Upgrade{
	public DefensiveBarrierUpgrade(){
		this.name = "Upgrade Barrier Ability";
		this.buttonName = "DefensiveBarrier";
		this.isAbility = true;
		SetDescription ();
	}

	public override void SetDescription(){
		this.description = "> +50 Health\n> +1 DPS";
		if (GetLevel () == 9) {
			this.description += "\n> Barrier Spews fire periodically";
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
		GameControl.control.barrierLevel += 1;
	}

	public override int GetLevel(){
		return GameControl.control.barrierLevel;
	}
}