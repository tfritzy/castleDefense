using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public int gameLevel;
	public int gold;
    public int totalGold;
    public int totalDamageDone;
    public int soulGemsAcquired;
    public int totalEnemiesKilled;
	public int soulGemCount;
	public int totalUpgradeLevel;

    // Variables to know the average damage dealt.
    public int damageDealtThisLevel;
    public int damageCount;
    public int averageDamage;
    public bool autoplay;

	//castle
	public int castleLevel;
	public int castleInhabitantBuff;
	public int repairSpeed;
	public int castleHealthLevel;
	public int castleHealth;
	public int castleCapacity;
    public int extraGoldPercentage;

	//ballista
	public int ballistaUpgradeLevel;
	public int ballistaCooldown;
	public int ballistaDamage;
	public int pierceLevel;
	public int extraBallistaArrows;
    public int ballistaCount;
    public int bleedChance;

	//Abilities
	public int fanOfBoltsLevel;
	public int explosiveShotLevel;
	public int minigunLevel;
	public int caltropsLevel;
	public int barrierLevel;
	public int blizzardAbilityLevel;
	public int seekingArrowBarrageLevel;
	public int fireWaveLevel;

	//archer guild
	public int archerLevel;
	public int archerCount;
	public int archerAttackSpeed;
	public int arrowLevel;
	public int arrowDamage;
	public int archerDamageLevel;
	public int archerExtraArrowChance;
    public int archerCritChance;
    public int archerCritDamage;
    public int archerAbilityPower;


	//mage guild
	public int extraMageProjectileChance;
	public int mageAttackSpeed;
	public int mageLevel;
	public int mageCount;
	public int extraMageDamage;
    public int mageSlow;
    public int mageSplashRadius;
    public int mageAbilityPower;

    // Tower levels
    public int arrowTowerLevel;
    public int teslaTowerLevel;
    public int barracksLevel;
    public int pounderTowerLevel;
    public int torrentTowerLevel;
    public int flakTowerLevel;
    public int axeTowerLevel;
    public int fireBoltTowerlevel;

	void Awake(){
		if (control == null) {
			control = this;
			DontDestroyOnLoad (this.gameObject);
		} else if (control != this){
			Destroy (this.gameObject);
		}
		load ();
	}
		

	public void AddSoulGems(int count){
		GameControl.control.soulGemCount += count;
		GameObject.Find ("SoulGemLabel").GetComponent<Text> ().text = (GameControl.control.soulGemCount).ToString ();
	}

	public void AddGold(int amount){
		this.gold += amount;
        
		GameObject.Find ("GoldLabel").GetComponent<Text> ().text = (GameControl.control.gold / 100).ToString ();
		GameObject.Find ("SilverLabel").GetComponent<Text> ().text = (GameControl.control.gold % 100 / 10).ToString ();
		GameObject.Find ("BronzeLabel").GetComponent<Text> ().text = (GameControl.control.gold % 100 % 10).ToString ();
        GameObject.Find("TotalGold").GetComponent<Text>().text = GameControl.control.totalGold.ToString();
	}
		
	public void save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();

		data.gameLevel = GameControl.control.gameLevel;
		data.gold = GameControl.control.gold;
		data.castleLevel = GameControl.control.castleLevel;
		data.ballistaCooldown = GameControl.control.ballistaCooldown;
		data.ballistaDamage = GameControl.control.ballistaDamage;
		data.castleHealth = GameControl.control.castleHealth;
		data.pierceLevel = GameControl.control.pierceLevel;
		data.totalUpgradeLevel = GameControl.control.totalUpgradeLevel;
        data.extraGoldPercentage = GameControl.control.extraGoldPercentage;
        data.ballistaCount = GameControl.control.ballistaCount;
        data.bleedChance = GameControl.control.bleedChance;
        data.totalGold = GameControl.control.totalGold;
        data.totalDamageDone = GameControl.control.totalDamageDone;
        data.totalSoulGems = GameControl.control.soulGemsAcquired;
        data.totalEnemiesKilled = GameControl.control.totalEnemiesKilled;

		data.barrierLevel = GameControl.control.barrierLevel;
		data.soulGemCount = GameControl.control.soulGemCount;
		data.castleCapacity = GameControl.control.castleCapacity;

		//abilities
		data.fanOfBoltsLevel = GameControl.control.fanOfBoltsLevel;
		data.fireWaveLevel = GameControl.control.fireWaveLevel;
		data.caltropsLevel = GameControl.control.caltropsLevel;
		data.minigunLevel = GameControl.control.minigunLevel;
		data.castleInhabitantBuff = GameControl.control.castleInhabitantBuff;
		data.repairSpeed = GameControl.control.repairSpeed;
		data.ballistaUpgradeLevel = GameControl.control.ballistaUpgradeLevel;
		data.blizzardAbilityLevel = GameControl.control.blizzardAbilityLevel;

		//archer guild
		data.archerCount = GameControl.control.archerCount;
		data.archerLevel = GameControl.control.archerLevel;
		data.castleHealthLevel = GameControl.control.castleHealthLevel;
		data.arrowLevel = GameControl.control.arrowLevel;
        data.archerCritChance = GameControl.control.archerCritChance;
        data.archerCritDamage = GameControl.control.archerCritDamage;
        data.archerAbilityPower = GameControl.control.archerAbilityPower;
        data.archerDamageLevel = GameControl.control.archerDamageLevel;

		//paralysis
		data.extraBallistaArrows = GameControl.control.extraBallistaArrows;
		data.arrowDamage = GameControl.control.arrowDamage;

		data.explosiveShotLevel = GameControl.control.explosiveShotLevel;
		data.seekingArrowBarrageLevel = GameControl.control.seekingArrowBarrageLevel;

		//poison
		data.archerAttackSpeed = GameControl.control.archerAttackSpeed;

		//magic guild
		data.mageCount = GameControl.control.mageCount;
		data.mageLevel = GameControl.control.mageLevel;
        data.mageSlow = GameControl.control.mageSlow;
        data.mageSplashRadius = GameControl.control.mageSplashRadius;
        data.mageAbilityPower = GameControl.control.mageAbilityPower;
        data.extraMageDamage = GameControl.control.extraMageDamage;
        data.mageAttackSpeed = GameControl.control.mageAttackSpeed;

        // tower levels
        data.arrowTowerLevel = GameControl.control.arrowTowerLevel;
        data.teslaTowerLevel = GameControl.control.teslaTowerLevel;
        data.barracksLevel = GameControl.control.barracksLevel;
        data.pounderTowerLevel = GameControl.control.pounderTowerLevel;
        data.torrentTowerlevel = GameControl.control.torrentTowerLevel;
        data.flakTowerLevel = GameControl.control.flakTowerLevel;
        data.axeTowerLevel = GameControl.control.axeTowerLevel;
        data.fireBoltTowerlevel = GameControl.control.fireBoltTowerlevel;

        Debug.Log("Gamecontrol save");
		bf.Serialize (file, data);
		file.Close ();
	}

	public void reset(){
		GameControl.control = null;
		GameControl.control = new GameControl ();
		GameControl.control.save ();
	}

	public void load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			this.gameLevel = data.gameLevel;
			this.gold = 0;
			this.AddGold (data.gold);
			this.soulGemCount = data.soulGemCount;
			this.totalUpgradeLevel = data.totalUpgradeLevel;
            this.ballistaCount = data.ballistaCount;
            this.bleedChance = data.bleedChance;
            this.totalGold = data.totalGold;
            this.totalDamageDone = data.totalDamageDone;
            this.totalEnemiesKilled = data.totalEnemiesKilled;
            this.soulGemsAcquired = data.totalSoulGems;

			//castle
			this.castleLevel = data.castleLevel;
			this.castleInhabitantBuff = data.castleInhabitantBuff;
			this.repairSpeed = data.repairSpeed;
			this.ballistaUpgradeLevel = data.ballistaUpgradeLevel;
			this.ballistaCooldown = data.ballistaCooldown;
			this.ballistaDamage = data.ballistaDamage;
			this.castleHealthLevel = data.castleHealthLevel;
			this.castleHealth = data.castleHealth;
			this.pierceLevel = data.pierceLevel;
			this.castleCapacity = data.castleCapacity;
            this.extraGoldPercentage = data.extraGoldPercentage;

			//abilities
			this.fanOfBoltsLevel = data.fanOfBoltsLevel;
			this.minigunLevel = data.minigunLevel;
			this.barrierLevel = data.barrierLevel;
			this.explosiveShotLevel = data.explosiveShotLevel;
			this.seekingArrowBarrageLevel = data.seekingArrowBarrageLevel;
			this.blizzardAbilityLevel = data.blizzardAbilityLevel;
			this.fireWaveLevel = data.fireWaveLevel;
			this.caltropsLevel = data.caltropsLevel;
            this.archerAbilityPower = data.archerAbilityPower;

            if (this.explosiveShotLevel == 0)
            {
                this.explosiveShotLevel = 1;
            }

			//archer guild
			this.archerLevel = data.archerLevel;
			this.archerCount = data.archerCount;
			this.arrowLevel = data.arrowLevel;
            this.archerCritChance = data.archerCritChance;
            this.archerCritDamage = data.archerCritDamage;
            this.archerDamageLevel = data.archerDamageLevel;


			//paralysis
			this.extraBallistaArrows = data.extraBallistaArrows;
			this.arrowDamage = data.arrowDamage;

			//poison
			this.archerAttackSpeed = data.archerAttackSpeed;

			//mage guild
			this.mageCount = data.mageCount;
			this.mageAttackSpeed = data.mageAttackSpeed;
			this.mageLevel = data.mageLevel;
            this.mageSplashRadius = data.mageSplashRadius;
            this.mageSlow = data.mageSlow;
            this.mageAbilityPower = data.mageAbilityPower;
            this.extraMageDamage = data.extraMageDamage;
            this.mageAttackSpeed = data.mageAttackSpeed;

            // Tower levels
            this.arrowTowerLevel = data.arrowTowerLevel;
            this.teslaTowerLevel = data.teslaTowerLevel;
            this.barracksLevel = data.barracksLevel;
            this.pounderTowerLevel = data.pounderTowerLevel;
            this.torrentTowerLevel = data.torrentTowerlevel;
            this.flakTowerLevel = data.flakTowerLevel;
            this.axeTowerLevel = data.axeTowerLevel;
            this.fireBoltTowerlevel = data.fireBoltTowerlevel;

            if (this.gameLevel == 0) {
				this.gameLevel = 1;
			}
		} else {
			Debug.Log ("File Does not exist :( ");
		}
	}
		
	
}

[Serializable]
class PlayerData{

    public int gameLevel;
    public int gold;
    public int soulGemCount;
    public int totalUpgradeLevel;
    public int totalDamageDone;
    public int totalSoulGems;
    public int totalEnemiesKilled;

    //castle
    public int castleLevel;
    public int castleInhabitantBuff;
    public int repairSpeed;
    public int castleHealthLevel;
    public int castleHealth;
    public int castleCapacity;
    public int extraGoldPercentage;
    public int totalGold;

    //ballista
    public int ballistaUpgradeLevel;
    public int ballistaCooldown;
    public int ballistaDamage;
    public int pierceLevel;
    public int extraBallistaArrows;
    public int ballistaCount;
    public int bleedChance;

    //Abilities
    public int fanOfBoltsLevel;
    public int explosiveShotLevel;
    public int minigunLevel;
    public int caltropsLevel;
    public int barrierLevel;
    public int blizzardAbilityLevel;
    public int seekingArrowBarrageLevel;
    public int fireWaveLevel;

    //archer guild
    public int archerLevel;
    public int archerCount;
    public int archerAttackSpeed;
    public int arrowLevel;
    public int arrowDamage;
    public int archerDamageLevel;
    public int archerExtraArrowChance;
    public int archerCritChance;
    public int archerCritDamage;
    public int archerAbilityPower;


    //mage guild
    public int extraMageProjectileChance;
    public int mageAttackSpeed;
    public int mageLevel;
    public int mageCount;
    public int extraMageDamage;
    public int mageSplashRadius;
    public int mageSlow;
    public int mageAbilityPower;

    // Tower Levels
    public int arrowTowerLevel;
    public int teslaTowerLevel;
    public int barracksLevel;
    public int pounderTowerLevel;
    public int torrentTowerlevel;
    public int flakTowerLevel;
    public int axeTowerLevel;
    public int fireBoltTowerlevel;
}