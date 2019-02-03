using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BallistaUpgrade : Upgrade{
	public BallistaUpgrade(){
		this.name = "Upgrade Ballista";
		SetDescription ();
	}
	private void SetDescription(){
        this.description = "";
        // Do the extra bolt and extra pirce stuff first because it's the most interesting
        if (GetLevel() % 10 == 0 && (GetLevel() / 10) % 2 == 0 && GetLevel() != 0)
        {
            this.description += "> +1 Bolt per Firing\n";
        }
        if (GetLevel() % 10 == 0 && (GetLevel() / 10) % 2 == 1)
        {
            this.description += "> +1 Enemies Pirced by each Bolt\n";
        }
        if (GetLevel() == 2)
        {
            this.description += "> Unlocks Explosive Shot\n";
        }
        if (GetLevel() == 9)
        {
            this.description += "> Unlock Fan Of Bolts\n";
        }

        if (GetLevel() % 5 == 0) {

			this.description += "> +1 Damage per Bolt\n";
			
		}
		if (GetLevel() % 5 == 1) {
			this.description += "> 5% Faster Charge Speed\n";
		}
		if (GetLevel() % 5 == 2) {
			this.description += "> +1 Damage per Bolt\n";
		}
		if (GetLevel() % 5 == 3) {   
            this.description += "> +1 Damage per Bolt\n> 3% Faster Charge Speed";
        }
		if (GetLevel() % 5 == 4) {
			
			this.description += "> +1 Damage per Bolt\n> +1 Soul Gem\n";
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
        // Cost equation is y = .14 * x^3
        int x = GameControl.control.totalUpgradeLevel;
        int y = GameControl.control.ballistaUpgradeLevel;
        return Mathf.Max((int)(.2f * x * y), 2);
    }
	public override void LevelUp ()
	{
        // Do the extra bolt and extra pirce stuff
        if (GetLevel() % 10 == 0 && (GetLevel() / 10) % 2 == 0 && GetLevel() != 0)
        {
            GameControl.control.extraBallistaArrows += 1;
        }
        if (GetLevel() % 10 == 0 && (GetLevel() / 10) % 2 == 1)
        {
            GameControl.control.pierceLevel += 1;
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

		GameControl.control.ballistaUpgradeLevel += 1;
		GameControl.control.totalUpgradeLevel += 1;
		SetDescription ();
	}
	private void Level1Upgrade(){
		GameControl.control.ballistaDamage += 1;
	}
	private void Level2Upgrade(){
		GameControl.control.ballistaCooldown += 5;
	}
	private void Level3Upgrade(){
        GameControl.control.ballistaDamage += 1;
    }
	private void Level4Upgrade(){
		GameControl.control.ballistaDamage += 1;
        GameControl.control.ballistaCooldown += 3;
        if (GetLevel() == 3)
        {
            GameControl.control.explosiveShotLevel += 1;
        }
    }
	private void Level5Upgrade(){
		GameControl.control.AddSoulGems(1);
        GameControl.control.ballistaDamage += 1;
		if (GetLevel() == 9) {
			GameControl.control.fanOfBoltsLevel += 1;
		}
	}

	public override int GetLevel(){
		return GameControl.control.ballistaUpgradeLevel;
	}
}