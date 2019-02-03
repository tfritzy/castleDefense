using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ArcherUpgrade : Upgrade{
	public ArcherUpgrade(){
		this.name = "Upgrade Archer";

		SetDescription ();
	}
	private void SetDescription(){
        this.description = "";
        // every tenth level add an archer.
        if ((GetLevel()) % 10 == 0)
        {
            this.description += "> +1 Archers\n";
        }
        // Unlock arrow barrage at level 3
        if (GetLevel() == 2)
        {
            this.description += "> Unlocks Arrow Barrage\n";
        }
        // Unlock minigun at level 9
        if (GetLevel() == 9)
        {
            this.description += "> Unlock Minigun Ability\n";
        }

        if (GetLevel() % 5 == 0) {
            this.description += "> +1 Damage per Arrow\n";
        }
		if (GetLevel() % 5 == 1) {
            this.description += "> +8% Archer Attack Speed\n";
        }
		if (GetLevel() % 5 == 2) {
			this.description += "> +1 Damage per Arrow\n";
		}
		if (GetLevel() % 5 == 3) {
            this.description += "> +2 Damage per Arrow\n";
		}
		if (GetLevel() % 5 == 4) {
			this.description += "> +8% Archer Attack Speed\n> +1 Soul Gems";
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
        int y = GameControl.control.archerLevel;
        return Mathf.Max((int)(.2f * x * y), 2);
    }
	public override void LevelUp ()
	{
        // Unlock abilities and add archers
        if (GetLevel() == 2)
        {
            GameControl.control.seekingArrowBarrageLevel += 1;
        }
        if (GetLevel() == 9)
        {
            GameControl.control.minigunLevel += 1;
        }
        if ((GetLevel()) % 10 == 0)
        {
            GameControl.control.archerCount += 1;
        }

        if (GetLevel() % 5 == 0) {
			Level1Upgrade ();
		}
		if (GetLevel() % 5 == 1) {
			Level2Upgrade ();
		}
		if (GetLevel() % 5 == 2) {
			Level3Upgrade ();
		}
		if (GetLevel() % 5 == 3) {
			Level4Upgrade ();
		}
		if (GetLevel() % 5 == 4) {
			Level5Upgrade ();
		}
		GameControl.control.archerLevel += 1;
		GameControl.control.totalUpgradeLevel += 1;
		SetDescription ();
	}
	private void Level1Upgrade(){
		
		GameControl.control.archerDamageLevel += 1;
	}
	private void Level2Upgrade(){
		GameControl.control.archerAttackSpeed += 8;
	}
	private void Level3Upgrade(){
		GameControl.control.archerDamageLevel += 1;
	}
	private void Level4Upgrade(){
		GameControl.control.archerDamageLevel += 2;
	}
	private void Level5Upgrade(){
		GameControl.control.archerAttackSpeed += 8;
		GameControl.control.AddSoulGems(1);
	}

	public override int GetLevel(){
		return GameControl.control.archerLevel;
	}
}