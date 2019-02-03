using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ArrowBarrageUpgrade : Upgrade{
	public ArrowBarrageUpgrade(){
		this.name = "Upgrade Arrow Barrage Ability";
		this.buttonName = "ArrowBarrage";
		this.isAbility = true;
		SetDescription ();
	}

	public override void SetDescription(){
		this.description = "> +1 Damage per Arrow\n> +0.1s duration\n> +1 Arrows per Second";
		if (GetLevel () == 9) {
			this.description += "\n> Arrows target enemies (probably OP)";
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
		GameControl.control.seekingArrowBarrageLevel += 1;
	}

	public override int GetLevel(){
		return GameControl.control.seekingArrowBarrageLevel;
	}
}