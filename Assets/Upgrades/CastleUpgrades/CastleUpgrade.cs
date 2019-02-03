using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CastleUpgrade : Upgrade{
	public CastleUpgrade(){
		this.name = "Upgrade Castle";

		SetDescription ();
	}
	private void SetDescription(){
		if (GameControl.control.castleLevel % 5 == 0) {
			this.description = "> Unlocks Defensive Barrier \n> +100 Castle Health";
		}
		if (GameControl.control.castleLevel % 5 == 1) {
			this.description = "> + 2 Capacity";
		}
		if (GameControl.control.castleLevel % 5 == 2) {
			this.description = "> +2 Repair Speed";
		}
		if (GameControl.control.castleLevel % 5 == 3) {
			this.description = "> +150 Castle Health \n> +1 Capactiy";
		}
		if (GameControl.control.castleLevel % 5 == 4) {
			this.description = "> +1 Damage to all Castle Inhabitants\n> +1 Repair Speed\n> +1 Soul Gems";
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
        int x = GameControl.control.totalUpgradeLevel;
        int y = GameControl.control.castleLevel;
        return Mathf.Max((int)(.2f * x * y), 2);
    }
	public override void LevelUp ()
	{
		if (GameControl.control.castleLevel % 5 == 0) {
			Level1Upgrade ();
		}
		if (GameControl.control.castleLevel % 5 == 1) {
			Level2Upgrade ();
		}
		if (GameControl.control.castleLevel % 5 == 2) {
			Level3Upgrade ();
		}
		if (GameControl.control.castleLevel % 5 == 3) {
			Level4Upgrade ();
		}
		if (GameControl.control.castleLevel % 5 == 4) {
			Level5Upgrade ();
		}

		GameControl.control.castleLevel += 1;
		GameControl.control.totalUpgradeLevel += 1;
		SetDescription ();
	}
	private void Level1Upgrade(){
		if (GetLevel() == 0) {
			GameControl.control.barrierLevel += 1;
		}
		GameControl.control.castleHealth += 100;

	}
	private void Level2Upgrade(){
		GameControl.control.castleCapacity += 2;
	}
	private void Level3Upgrade(){
		GameControl.control.repairSpeed += 2;
	}
	private void Level4Upgrade(){
		GameControl.control.castleHealth += 150;
		GameControl.control.castleCapacity += 1;
	}
	private void Level5Upgrade(){
		if (GetLevel() == 9) {
			GameControl.control.caltropsLevel += 1;
		}
		GameControl.control.repairSpeed += 1;
		GameControl.control.AddSoulGems(1);
		GameControl.control.castleInhabitantBuff += 1;
	}

	public override int GetLevel(){
		return GameControl.control.castleLevel;
	}
}