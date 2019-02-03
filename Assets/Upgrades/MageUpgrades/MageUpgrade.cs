using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MageUpgrade : Upgrade{
	public MageUpgrade(){
		this.name = "Upgrade Mages";

		SetDescription ();
	}
    private void SetDescription()
    {
        this.description = "";
        // every tenth level add an archer.
        if ((GetLevel()) % 10 == 0)
        {
            this.description += "> +1 Mages\n";
        }
        // Unlock arrow barrage at level 3
        if (GetLevel() == 2)
        {
            this.description += "> Unlocks Fire Wave\n";
        }
        // Unlock minigun at level 9
        if (GetLevel() == 9)
        {
            this.description += "> Unlocks Blizzard\n";
        }

        if (GetLevel() % 5 == 0)
        {
            this.description += "> +3 Damage per Bolt\n";
        }
        if (GetLevel() % 5 == 1)
        {
            this.description += "> +3% Attack Speed\n";
        }
        if (GetLevel() % 5 == 2)
        {
            this.description += "> +3 Damage per Bolt\n";
        }
        if (GetLevel() % 5 == 3)
        {
            this.description += "> +2 Damage per Bolt\n";
        }
        if (GetLevel() % 5 == 4)
        {
            this.description += "> +3% Attack Speed\n> +1 Soul Gems";
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
        int y = GameControl.control.mageLevel;
        return Mathf.Max((int)(.2f * x * y), 2);
    }
	public override void LevelUp ()
	{
        // every tenth level add an archer.
        if ((GetLevel()) % 10 == 0)
        {
            GameControl.control.mageCount += 1;
        }
        // Unlock arrow barrage at level 3
        if (GetLevel() == 2)
        {
            GameControl.control.fireWaveLevel += 1;
        }
        // Unlock minigun at level 9
        if (GetLevel() == 9)
        {
            GameControl.control.blizzardAbilityLevel += 1;
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
		if (GetLevel()% 5 == 4) {
			Level5Upgrade ();
		}

		GameControl.control.mageLevel += 1;
		GameControl.control.totalUpgradeLevel += 1;
		SetDescription ();
	}
	private void Level1Upgrade(){
        GameControl.control.extraMageDamage += 3;
	}
	private void Level2Upgrade(){
        GameControl.control.mageAttackSpeed += 3;
	}
	private void Level3Upgrade(){
		GameControl.control.extraMageDamage += 3;

	}
	private void Level4Upgrade(){
		GameControl.control.extraMageDamage += 2;
	}
	private void Level5Upgrade(){
		GameControl.control.mageAttackSpeed += 3;
		GameControl.control.AddSoulGems(1);
	}

	public override int GetLevel(){
		return GameControl.control.mageLevel;
	}
}