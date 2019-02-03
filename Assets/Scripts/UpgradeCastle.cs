using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCastle : MonoBehaviour {
	/**
	private Color white = new Color(1,1,1,1);
	private Color black = new Color(.25f,.25f,.25f,1);

	public string purchaseAction;
	public string selectedButton;
	private Upgrade selectedUpgrade;

	private Upgrade ballistaCooldownUpgrade = new BallistaCooldownUpgrade();
	private Upgrade ballistaDamageUpgrade = new BallistaDamageUpgrade();
	private Upgrade ballistaPierceUpgrade = new BallistaPierceUpgrade();
	private Upgrade ballistaAdditionalUpgrade = new AdditionalBallistaUprade();
	private Upgrade repairWorkerUpgrade = new RepairWorkerUprade();
	private Upgrade fortificationUpgrade = new FortificationUpgrade();
	private Upgrade carpentryUpgrade = new CarpentryUpgrade();
	private Upgrade foundryUpgrade = new FoundryUpgrade();
	private Upgrade cannonUpgrade = new CannonUpgrade();
	private Upgrade sentryTowerUpgrade = new SentryTowerUpgrade();
	private Upgrade sentryTowerDamageUpgrade = new SentryTowerDamage();
	private Upgrade sentryTowerAttackSpeedUpgrade = new SentryTowerAttackSpeed();
	//private Upgrade caltropsUpgrade = new CaltropsUpgrade();
	private Upgrade flamingOilUpgrade = new FlamingOilUpgrade();
	private Upgrade barrierupgrade = new DefensiveBarrierUpgrade();

	Upgrade[] upgrades;
	void Start(){

		upgrades = new Upgrade[] {ballistaCooldownUpgrade, ballistaDamageUpgrade, ballistaPierceUpgrade, ballistaAdditionalUpgrade,
			repairWorkerUpgrade, fortificationUpgrade, carpentryUpgrade, foundryUpgrade, sentryTowerUpgrade, sentryTowerDamageUpgrade, sentryTowerAttackSpeedUpgrade,
			caltropsUpgrade, flamingOilUpgrade, barrierupgrade, cannonUpgrade};
		for (int i = 0; i < upgrades.Length; i++) {
			upgrades [i].Setup ();
			upgrades[i].SetupLists ();
		}

		foundryUpgrade.next.Add (ballistaDamageUpgrade);
		foundryUpgrade.next.Add (ballistaCooldownUpgrade);

		ballistaCooldownUpgrade.next.Add (ballistaPierceUpgrade);
		ballistaCooldownUpgrade.prev.Add (foundryUpgrade);

		ballistaDamageUpgrade.next.Add (ballistaPierceUpgrade);
		ballistaDamageUpgrade.prev.Add (foundryUpgrade);

		ballistaPierceUpgrade.prev.Add (ballistaDamageUpgrade);
		ballistaPierceUpgrade.prev.Add (ballistaCooldownUpgrade);

		ballistaAdditionalUpgrade.next.Add (cannonUpgrade);
		ballistaAdditionalUpgrade.prev.Add (ballistaPierceUpgrade);

		cannonUpgrade.prev.Add (ballistaAdditionalUpgrade);
		cannonUpgrade.prev.Add (barrierupgrade);

		carpentryUpgrade.next.Add (repairWorkerUpgrade);
		carpentryUpgrade.next.Add (fortificationUpgrade);

		repairWorkerUpgrade.next.Add (caltropsUpgrade);
		repairWorkerUpgrade.next.Add (flamingOilUpgrade);
		repairWorkerUpgrade.prev.Add (carpentryUpgrade);

		caltropsUpgrade.prev.Add (repairWorkerUpgrade);

		flamingOilUpgrade.prev.Add (repairWorkerUpgrade);
		flamingOilUpgrade.next.Add (barrierupgrade);

		barrierupgrade.next.Add (cannonUpgrade);
		barrierupgrade.prev.Add (flamingOilUpgrade);

		fortificationUpgrade.prev.Add (carpentryUpgrade);
		fortificationUpgrade.next.Add (sentryTowerUpgrade);

		sentryTowerUpgrade.next.Add (sentryTowerDamageUpgrade);
		sentryTowerUpgrade.next.Add (sentryTowerAttackSpeedUpgrade);
		sentryTowerUpgrade.prev.Add (fortificationUpgrade);

		sentryTowerDamageUpgrade.prev.Add (sentryTowerUpgrade);

		sentryTowerAttackSpeedUpgrade.prev.Add (sentryTowerUpgrade);

		SetInteractable ();
	}

	void SetInteractable(){
		for (int i = 0; i < upgrades.Length; i++) {

			bool setArrow = true;
			foreach (Upgrade upgrade in upgrades[i].next) {
				foreach (Upgrade nprev in upgrade.prev) {
					if (nprev.GetLevel() < 1) {
						setArrow = false;
					}
				}
			}

			if (!setArrow) {
				if (upgrades [i].arrow != null) {
					upgrades [i].arrow.GetComponent<Image> ().color = black;
				}

			} else {
				if (upgrades [i].arrow != null) {
					upgrades [i].arrow.GetComponent<Image> ().color = white;
				}
			}

			if (upgrades [i].criterium1() == null && upgrades [i].criterium3()== null || upgrades[i].IsMaxLevel()) {
				GameObject.Find (upgrades [i].name).GetComponent<Image> ().color = white;
			} else {
				GameObject.Find (upgrades [i].name).GetComponent<Image> ().color = black;
				if (upgrades [i].arrow != null) {
				}
			}
		}
	}

	void SelectButton(){

		string[] errors = new string[3];
		errors [0] = selectedUpgrade.criterium1 ();
		errors [1] = selectedUpgrade.criterium2 ();
		errors [2] = selectedUpgrade.criterium3 ();
		SetExplanation (selectedUpgrade.description, selectedUpgrade.GetLevel() + "", selectedUpgrade.GetCost().ToString(),errors);
	}

	void Cannon(){
		selectedUpgrade = cannonUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetCannon(){
		selectedUpgrade = cannonUpgrade;
	}

	void Fortification(){
		selectedUpgrade = fortificationUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetFortification(){
		selectedUpgrade = fortificationUpgrade;
	}

	void Foundry(){
		selectedUpgrade = foundryUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetFoundry(){
		selectedUpgrade = foundryUpgrade;
	}

	void Carpentry(){
		selectedUpgrade = carpentryUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetCarpentry(){
		selectedUpgrade = carpentryUpgrade;
	}

	void Fortificiation(){
		selectedUpgrade = fortificationUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetFortificiation(){
		selectedUpgrade = fortificationUpgrade;
	}
		
	void RepairWorker(){
		selectedUpgrade = repairWorkerUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetRepairWorker(){
		selectedUpgrade = repairWorkerUpgrade;
	}

	void AdditionalBallista(){
		selectedUpgrade = ballistaAdditionalUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetAdditionalBallista(){
		selectedUpgrade = ballistaAdditionalUpgrade;
	}

	void BallistaPierce(){
		selectedUpgrade = ballistaPierceUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetBallistaPierce(){
		selectedUpgrade = ballistaPierceUpgrade;
	}

	void BallistaDamage(){
		selectedUpgrade = ballistaDamageUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetBallistaDamage(){
		selectedUpgrade = ballistaDamageUpgrade;
	}

	void BallistaCooldown(){
		selectedUpgrade = ballistaCooldownUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetBallistaCooldown(){
		selectedUpgrade = ballistaCooldownUpgrade;
	}

	void DefensiveBarrier(){
		selectedUpgrade = barrierupgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetDefensiveBarrier(){
		selectedUpgrade = barrierupgrade;
	}

	void FlamingOil(){
		selectedUpgrade = flamingOilUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetFlamingOil(){
		selectedUpgrade = caltropsUpgrade;
	}

	void Caltrops(){
		selectedUpgrade = caltropsUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}
	void SetCaltrops(){
		selectedUpgrade = caltropsUpgrade;
	}

	void SentryTower(){
		selectedUpgrade = sentryTowerUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetSentryTower(){
		selectedUpgrade = sentryTowerUpgrade;
	}
		
	void SetSentryTowerDamage(){
		selectedUpgrade = sentryTowerDamageUpgrade;
	}

	void SentryTowerDamage(){
		selectedUpgrade = sentryTowerDamageUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SentryTowerAttackSpeed(){
		selectedUpgrade = sentryTowerAttackSpeedUpgrade;
		this.selectedButton = selectedUpgrade.SetUI ();
		SelectButton ();
	}

	void SetSentryTowerAttackSpeed(){
		selectedUpgrade = sentryTowerDamageUpgrade;
	}


	void SetSelectedButton(string name){
		Debug.Log ("The selected button is now " + name);
		this.selectedButton = name;
	}



	void Purchase(){
		GameObject.Find(selectedButton).SendMessage("BuyIt");
		SetInteractable ();
		SelectButton();
		SetGoldLabel ();
	}

	void BuyIt(){
		selectedUpgrade.Purchase ();
		SelectButton ();
		SetGoldLabel ();
	}

	void SetPurchaseAction(string purchaseAction){
		this.purchaseAction = purchaseAction;
	}
		


	void SetExplanation(string name, string level, string cost, string[] errors){
		Transform expl = GameObject.Find ("Canvas").transform.Find("Upgrade Explanation");
		expl.Find ("upgradeName").GetComponent<Text> ().text = name;
		expl.Find ("level").GetComponent<Text> ().text = level;
		expl.Find ("cost").GetComponent<Text> ().text = cost;
		SetErrors (errors);
	}

	void SetErrors(string[] errors){

		for (int i = 0; i < 3; i++) {
			GameObject.Find ("requirement" + i.ToString ()).GetComponent<Text> ().text = "";
		}

		int count = 0; 
		for (int i = 0; i < 3; i++) {
			if (errors [i] != null) {
				GameObject.Find ("requirement" + count.ToString ()).GetComponent<Text> ().text = errors [i];
				count += 1;
				Debug.Log (errors [i]);
			}
		}
		if (count > 0) {
			GameObject.Find ("Purchase").GetComponent<Button> ().interactable = false;
		} 
		if (count == 0){
			GameObject.Find ("Purchase").GetComponent<Button> ().interactable = true;
		}
		if (selectedUpgrade.GetCost () == 0) {
			GameObject.Find ("Purchase").GetComponent<Button> ().interactable = false;
		}
	}
	void SetGoldLabel(){
		GameObject.Find ("GoldCountLabel").GetComponent<Text> ().text = GameControl.control.gold.ToString();
	}
}

class SentryTowerUpgrade : Upgrade{
	public SentryTowerUpgrade(){
		this.name = "SentryTower";
		this.description = "An additional sentry tower";
	}

	public override void Setup(){
		Debug.Log ("Setup Sentry Tower");
		this.arrow = GameObject.Find ("stt");
	}
	public override string criterium1(){
		if (GameControl.control.castleHealthLevel < 1) {
			return "Fortification must be above level 1";
		} else {
			return null;
		}
	}
	public override string criterium2(){
		return this.CanPurchase();
	}
	public override string criterium3(){
		if (GameControl.control.sentryTowerCount < 4) {
			return null;
		} else {
			return "You have the 4 sentry maxiumum.";
		}
	}
	public override int GetCost(){
		return 1000 + 1000*GetLevel();
	}
	public override void LevelUp ()
	{
		GameControl.control.sentryTowerCount += 1;
	}
	public override int GetLevel(){
		return GameControl.control.sentryTowerCount;
	}
	public override bool IsMaxLevel(){
		Debug.Log ("Sentry tower max level");
		if (GameControl.control.sentryTowerCount > 3) {
			return true;
		} else {
			return false;
		}
	}
}

class SentryTowerDamage : Upgrade{
	public SentryTowerDamage(){
		this.name = "SentryTowerDamage";
		this.description = "Increase Sentry Damage by 1";
	}
	public override string criterium1(){
		if (GameControl.control.sentryTowerCount < 1) {
			return "You must have at least one Sentry Tower First";
		} else {
			return null;
		}
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 300 + 300*GetLevel();
	}
	public override void LevelUp ()
	{
		GameControl.control.sentryTowerDamage += 1;
	}
	public override int GetLevel(){
		return GameControl.control.sentryTowerDamage;
	}
}

class SentryTowerAttackSpeed : Upgrade{
	public SentryTowerAttackSpeed(){
		this.name = "SentryTowerAttackSpeed";
		this.description = "Increase Sentry Tower attack speed by 10%";
	}
	public override string criterium1(){
		if (GameControl.control.sentryTowerCount < 1) {
			return "You must have at least one sentry.";
		} else {
			return null;
		}
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 400 + 400*GetLevel();
	}
	public override void LevelUp ()
	{
		GameControl.control.sentryTowerAttackSpeed += 1;
	}
	public override int GetLevel(){
		return GameControl.control.sentryTowerAttackSpeed;
	}
}

/*
class CaltropsUpgrade : Upgrade{
	public CaltropsUpgrade(){
		this.name = "Caltrops";
		this.description = "Caltrops are better and are thrown more often";
	}
	public override string criterium1(){
		if (GameControl.control.repairerCount < 1) {
			return "Must have at least one repair worker";
		} else {
			return null;
		}
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 1000 + 1000*GetLevel();
	}
	public override void LevelUp ()
	{
		GameControl.control.caltropsLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.caltropsLevel;
	}
}


class FlamingOilUpgrade : Upgrade{
	public FlamingOilUpgrade(){
		this.name = "FlamingOil";
		this.description = "Improve Ability recharge speed, damage and duration";
	}
	public override string criterium1(){
		if (GameControl.control.repairerCount < 1) {
			return "Must have at least two repair workers";
		} else {
			return null;
		}
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("fd");
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 400 + 400*GetLevel();
	}
	public override void LevelUp ()
	{
		GameControl.control.flamingOilLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.flamingOilLevel;
	}
}

class DefensiveBarrierUpgrade : Upgrade{
	public DefensiveBarrierUpgrade(){
		this.name = "DefensiveBarrier";
		this.description = "Improve health, damage, and cooldown of ability";
	}
	public override string criterium1(){
		return null;
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("dac");
	}
	public override string criterium3(){
		if (GameControl.control.flamingOilLevel < 1) {
			return "Flaming Oil must be above level 1";
		} else {
			return null;
		}
	}
	public override int GetCost(){
		return 2000 + 2000*GetLevel();
	}
	public override void LevelUp ()
	{
		GameControl.control.barrierLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.barrierLevel;
	}
}

class BallistaCooldownUpgrade : Upgrade{
	public BallistaCooldownUpgrade(){
		this.name = "BallistaCooldown";
		this.description = "Ballista charges power faster";
	}
	public override string criterium1(){
		if (!GameControl.control.hasFoundry) {
			return "Foundry must be unlocked";
		} else {
			return null;
		}
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("bbb");
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 500 + 500 * GameControl.control.ballistaCooldownLevel;
	}
	public override void LevelUp ()
	{
		GameControl.control.ballistaCooldownLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.ballistaCooldownLevel;
	}

}

class BallistaDamageUpgrade : Upgrade{
	public BallistaDamageUpgrade(){
		this.name = "BallistaDamage";
		this.description = "Ballista deals more damage";
	}
	public override string criterium1(){
		if (!GameControl.control.hasFoundry) {
			return "Foundry must be unlocked";
		} else {
			return null;
		}
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("fbb");
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 500 + GameControl.control.ballistaUpgradeLevel*500;
	}
	public override void LevelUp ()
	{
		GameControl.control.ballistaUpgradeLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.ballistaUpgradeLevel;
	}
}

class BallistaPierceUpgrade : Upgrade{
	public BallistaPierceUpgrade(){
		this.name = "BallistaPierce";
		this.description = "Ballista bolts pierce 1 more enemy";
	}
	public override string criterium1(){
		if (GameControl.control.ballistaUpgradeLevel < 1) {
			return "Ballista Power must be at least level 1";
		} else {
			return null;
		}
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("ba");
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		if (GameControl.control.ballistaCooldownLevel < 1) {
			return "Ballista Cooldown must be at least level 1";
		} else {
			return null;
		}
	}
	public override int GetCost(){
		return 1200 + GameControl.control.pierceLevel*1200;
	}
	public override void LevelUp ()
	{
		GameControl.control.pierceLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.pierceLevel;
	}
}

class AdditionalBallistaUprade : Upgrade{
	public AdditionalBallistaUprade(){
		this.name = "AdditionalBallista";
		this.description = "Construct an extra ballista";
	}
	public override string criterium1(){
		if (GameControl.control.pierceLevel < 1) {
			return "Ballista Pierce must be at least level 1";
		} else {
			return null;
		}
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("dac");
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 4000 + GameControl.control.ballistaCount*4000;
	}
	public override void LevelUp ()
	{
		GameControl.control.ballistaCount += 1;
	}
	public override int GetLevel(){
		return GameControl.control.ballistaCount;
	}
}

class RepairWorkerUprade : Upgrade{
	public RepairWorkerUprade(){
		this.name = "RepairWorker";
		this.description = "Hire an additional repair worker";
	}
	public override string criterium1(){
		if (!GameControl.control.hasCarpentrySchool) {
			return "Carpentry School must be constructed";
		} else {
			return null;
		}
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("hcf");
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 500 + GameControl.control.repairerCount * 500;
	}
	public override void LevelUp ()
	{
		//GameControl.control.repairerCount += 1;
	}
	public override int GetLevel(){
		return GameControl.control.repairerCount;
	}
}

class FortificationUpgrade : Upgrade{
	public FortificationUpgrade(){
		this.name = "Fortification";
		this.description = "Increase castle health by 1000";
	}
	public override string criterium1(){
		if (!GameControl.control.hasCarpentrySchool) {
			return "Carpentry School must be constructed";
		} else {
			return null;
		}
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("fs");
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		return 150 + GameControl.control.castleHealthLevel * 150;
	}
	public override void LevelUp ()
	{
		GameControl.control.castleHealthLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.castleHealthLevel;
	}
}

class CarpentryUpgrade : Upgrade{
	public CarpentryUpgrade(){
		this.name = "Carpentry";
		this.description = "Construct a school of carpentry";
	}
	public override string criterium1(){
		return null;
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("cfh");
	}
	public override int GetCost(){
		if (!GameControl.control.hasCarpentrySchool) {
			return 300;
		} else {
			return 0;
		}
	}
	public override void LevelUp ()
	{
		GameControl.control.hasCarpentrySchool = true;
	}
	public override int GetLevel(){
		if (!GameControl.control.hasCarpentrySchool) {
			return -1;
		} else {
			return 1;
		}
	}
}

class FoundryUpgrade : Upgrade{
	public FoundryUpgrade(){
		this.name = "Foundry";
		this.description = "Construct a foundry";
	}
	public override void Setup(){
		this.arrow = GameObject.Find ("fbb");
	}
	public override string criterium1(){
		return null;
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		return null;
	}
	public override int GetCost(){
		if (!GameControl.control.hasFoundry) {
			return 300;
		} else {
			return 0;
		}
	}
	public override void LevelUp ()
	{
		GameControl.control.hasFoundry = true;
	}
	public override int GetLevel(){
		if (!GameControl.control.hasFoundry) {
			return -1;
		} else {
			return 1;
		}
	}
}


class CannonUpgrade : Upgrade{
	public CannonUpgrade(){
		this.name = "Cannon";
		this.description = "Construct/Upgrade a heckin' cannon";
	}
	public override string criterium1(){
		if (GameControl.control.barrierLevel > 0) {
			return null;
		} else {
			return "Defensive barrier must be unlocked.";
		}
	}
	public override string criterium2(){
		return this.CanPurchase ();
	}
	public override string criterium3(){
		if (GameControl.control.ballistaCount > 0) {
			return null;
		} else {
			return "Additional Ballista must be unlocked.";
		}
	}
	public override int GetCost(){
		return 6000 + GameControl.control.cannonLevel * 6000;
	}
	public override void LevelUp ()
	{
		GameControl.control.cannonLevel += 1;
	}
	public override int GetLevel(){
		return GameControl.control.cannonLevel;
	}
}


	*/
}