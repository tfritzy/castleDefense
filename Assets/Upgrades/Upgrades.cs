using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour {

	private Upgrade selectedUpgrade;


    // Old upgrades...
	private Upgrade castleUpgrade;
	private Upgrade archerUpgrade;
	private Upgrade mageUpgrade;
	private Upgrade ballistaUpgrade;

    // Archer Upgrades
    private Upgrade archerAttackDamageUpgrade;
    private Upgrade archerAttackSpeedUpgrade;
    private Upgrade archerNewUpgrade;
    private Upgrade archerCritChanceUpgrade;
    private Upgrade archerCritDamageUpgrade;
    private Upgrade archerExtraArrowUpgrade;

    // Mage Upgrades
    private Upgrade mageAttackDamageUpgrade;
    private Upgrade mageAttackSpeedUpgrade;
    private Upgrade mageNewUpgrade;
    private Upgrade mageAbilityPowerUpgrade;
    private Upgrade mageSplashDamageUpgrade;
    private Upgrade mageSlowUpgrade;

    // Castle Upgrades
    private Upgrade castleHealthUpgrade;
    private Upgrade castleExtraGoldUpgrade;
    private Upgrade castleInhabitantBuffUpgrade;
    private Upgrade castleRepairSpeedUpgrade;

    // Ballista Upgrades
    private Upgrade ballistaDamageUpgrade;
    private Upgrade ballistaAdditionalUpgrade;
    private Upgrade ballistaAdditionalBoltUpgrade;
    private Upgrade ballistaBleedChanceUpgrade;
    private Upgrade ballistaReloadSpeedUpgrade;
    private Upgrade ballistaPierceUpgrade;

    // Colors
    private static Color castleRed = new Color(208 / 255f, 2 / 255f, 26 / 255f);
    private static Color ballistaYellow = new Color(245 / 255f, 166 / 255f, 35 / 255f);
    private static Color magePurple = new Color(144 / 255f, 19 / 255f, 254 / 255f);
    private static Color archerGreen = new Color(65 / 255f, 117 / 255f, 5 / 255f);

    // UI Labels
    private Image header;
    private GameObject upgradeBox;

    //images
    public Sprite castleImage;
	public Sprite archerImage;
	public Sprite mageImage;
	public Sprite ballistaImage;
	public Sprite explosiveShotImage;
	public Sprite fanOfBoltsImage;
	public Sprite defensiveBarrierImage;
	public Sprite caltropsImage;
	public Sprite arrowHailIMage;
	public Sprite minigunImage;
	public Sprite fireWaveImage;
	public Sprite blizzardImage;

	//abilities
	private Upgrade explosiveShotUpgrade;
	private Upgrade fanOfBoltsUpgrade;
	private Upgrade defensiveBarrierUpgrade;
	private Upgrade caltropsUpgrade;
	private Upgrade arrowBarrageUpgrade;
	private Upgrade minigunUpgrade;
	private Upgrade fireWaveUpgrade;
	private Upgrade blizzardUpgrade;
	private Upgrade[] abilities;

	// Use this for initialization
	void Start () {

        // Initialize Old Upgrades
		castleUpgrade = new CastleUpgrade ();
		archerUpgrade = new ArcherUpgrade ();
		mageUpgrade = new MageUpgrade ();
		ballistaUpgrade = new BallistaUpgrade ();


        // Initialize Ability Upgrades
		explosiveShotUpgrade = new ExplosiveShotUpgrade ();
		fanOfBoltsUpgrade = new FanOfBoltsUpgrade ();
		defensiveBarrierUpgrade = new DefensiveBarrierUpgrade ();
		caltropsUpgrade = new CaltropsUpgrade ();
		arrowBarrageUpgrade = new ArrowBarrageUpgrade ();
		minigunUpgrade = new MinigunUpgrade ();
		fireWaveUpgrade = new FireWaveUpgrade ();
		blizzardUpgrade = new BlizzardUpgrade ();

        // Initialize Archer Upgrades
        this.archerAttackDamageUpgrade = new ArcherUpgradeAttackDamage();
        this.archerAttackSpeedUpgrade = new ArcherUpgradeAttackSpeed();
        this.archerNewUpgrade = new ArcherUpgradeNewArcher();
        this.archerCritChanceUpgrade = new ArcherUpgradeCriticalHitChance();
        this.archerCritDamageUpgrade = new ArcherUpgradeCriticalHitDamage();
        this.archerExtraArrowUpgrade = new ArcherUpgradeExtraArrow();

        // Initialize Mage Upgrades
        this.mageNewUpgrade = new NewMageUpgrade();
        this.mageAttackSpeedUpgrade = new AttackSpeedMageUpgrade();
        this.mageAttackDamageUpgrade = new AttackDamageMageUpgrade();
        this.mageAbilityPowerUpgrade = new AbilityPowerMageUpgrade();
        this.mageSplashDamageUpgrade = new SplashMageUpgrade();
        this.mageSlowUpgrade = new SlowMageUpgrade();

        // Initialize Castle Upgrades
        this.castleExtraGoldUpgrade = new CastleExtraGoldUpgrade();
        this.castleHealthUpgrade = new CastleHealthUpgrade();
        this.castleInhabitantBuffUpgrade = new CastleInhabitantBuffUpgrade();
        this.castleRepairSpeedUpgrade = new CastleRepairSpeedUpgrade();

        // Initialize Ballista Upgrades
        this.ballistaAdditionalBoltUpgrade = new BallistaAdditionalBoltUpgrade();
        this.ballistaAdditionalUpgrade = new BallistaAdditionalBallistaUpgrade();
        this.ballistaBleedChanceUpgrade = new BallistaBleedChanceUpgrade();
        this.ballistaReloadSpeedUpgrade = new BallistaFasterReloadUpgrade();
        this.ballistaPierceUpgrade = new BallistaAdditionalPierceUpgrade();
        this.ballistaDamageUpgrade = new BallistaDamageUpgrade();

        abilities = new Upgrade[] {explosiveShotUpgrade, fanOfBoltsUpgrade, defensiveBarrierUpgrade, caltropsUpgrade, 
			arrowBarrageUpgrade, minigunUpgrade, fireWaveUpgrade, blizzardUpgrade};

		for (int i = 0; i < abilities.Length; i++) {
			abilities [i].SetUI ();
		}

		this.selectedUpgrade = castleUpgrade;	
		selectedUpgrade.SetUI ();

        this.header = GameObject.Find("Header").GetComponent<Image>();
        this.upgradeBox = GameObject.Find("Upgrade Desc");

        Castle();
	}

	private void NextLevel(){
		GameObject.Find ("LevelManager").SendMessage ("Go");

	}

    private void StyleUpgradeBox(Color color)
    {
        Text[] textBoxes = this.upgradeBox.GetComponentsInChildren<Text>();
        foreach(Text text in textBoxes)
        {
            if (text.name != "PurchaseUI")
            {
                text.color = color;
            }
        }
    }

	private void Reset(){
		GameControl.control.reset ();
	}

	private void ExplosiveShot(){
		this.selectedUpgrade = explosiveShotUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.explosiveShotImage;
        StyleUpgradeBox(ballistaYellow);

	}

	private void FanOfBolts(){
		this.selectedUpgrade = fanOfBoltsUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.fanOfBoltsImage;
        StyleUpgradeBox(ballistaYellow);
    }

	private void DefensiveBarrier(){
		this.selectedUpgrade = defensiveBarrierUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.defensiveBarrierImage;
        StyleUpgradeBox(castleRed);
    }

	private void Caltrops(){
		this.selectedUpgrade = caltropsUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.caltropsImage;
        StyleUpgradeBox(castleRed);
    }

	private void ArrowBarrage(){
		this.selectedUpgrade = arrowBarrageUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.arrowHailIMage;
        StyleUpgradeBox(archerGreen);
    }

	private void Minigun(){
		this.selectedUpgrade = minigunUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.minigunImage;
        StyleUpgradeBox(archerGreen);
    }

	private void FireWave(){
		this.selectedUpgrade = fireWaveUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.fireWaveImage;
        StyleUpgradeBox(magePurple);
    }
	private void Blizzard(){
		this.selectedUpgrade = blizzardUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.blizzardImage;
        StyleUpgradeBox(magePurple);
    }


	private void Ballista(){
		this.selectedUpgrade = ballistaUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.ballistaImage;
        StyleUpgradeBox(ballistaYellow);

    }

	private void Castle(){
		this.selectedUpgrade = castleUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.castleImage;
        StyleUpgradeBox(castleRed);
    }

	private void Archer(){
		this.selectedUpgrade = archerUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.archerImage;
        StyleUpgradeBox(archerGreen);
    }

	private void Mage(){
		this.selectedUpgrade = mageUpgrade;
		selectedUpgrade.SetUI ();
		GameObject.Find ("UpgradeImg").GetComponent<Image> ().sprite = this.mageImage;
        StyleUpgradeBox(magePurple);
    }

    // New Mage Upgrade System
    private void RaiseMageMenu()
    {
        GameObject.Find("MageSelect").GetComponent<Animator>().SetTrigger("GoUp");
        this.selectedUpgrade = mageAttackDamageUpgrade;
        selectedUpgrade.SetUI();
        StyleUpgradeBox(magePurple);
        StyleForMage();
    }

    private void LowerMageMenu()
    {
        GameObject.Find("MageSelect").GetComponent<Animator>().SetTrigger("GoDown");
    }

    private void StyleForMage()
    {
        selectedUpgrade.SetUI();
        GameObject.Find("UpgradeImg").GetComponent<Image>().sprite = this.mageImage;
        StyleUpgradeBox(magePurple);
    }

    private void SelectMageAttackSpeed()
    {
        this.selectedUpgrade = mageAttackSpeedUpgrade;
        StyleForMage();
    }

    private void SelectMageAttackDamage()
    {
        this.selectedUpgrade = mageAttackDamageUpgrade;
        StyleForMage();
    }

    private void SelectMageAbilityPower()
    {
        this.selectedUpgrade = mageAbilityPowerUpgrade;
        StyleForMage();
    }

    private void SelectMageNew()
    {
        this.selectedUpgrade = mageNewUpgrade;
        StyleForMage();
    }

    private void SelectMageSplash()
    {
        this.selectedUpgrade = mageSplashDamageUpgrade;
        StyleForMage();
    }

    private void SelectMageSlow()
    {
        this.selectedUpgrade = mageSlowUpgrade;
        StyleForMage();
    }



    // New Archer Upgrade System
    private void RaiseArcherMenu()
    {
        GameObject.Find("ArcherSelect").GetComponent<Animator>().SetTrigger("GoUp");
        this.selectedUpgrade = archerAttackDamageUpgrade;
        selectedUpgrade.SetUI();
        StyleUpgradeBox(archerGreen);
        StyleForArcher();
    }

    private void LowerArcherMenu()
    {
        GameObject.Find("ArcherSelect").GetComponent<Animator>().SetTrigger("GoDown");
    }

    private void StyleForArcher()
    {
        selectedUpgrade.SetUI();
        GameObject.Find("UpgradeImg").GetComponent<Image>().sprite = this.archerImage;
        StyleUpgradeBox(archerGreen);
    }


    private void SelectArcherAttackSpeed()
    {
        this.selectedUpgrade = archerAttackSpeedUpgrade;
        StyleForArcher();
    }

    private void SelectArcherAttackDamage()
    {
        this.selectedUpgrade = archerAttackDamageUpgrade;
        StyleForArcher();
    }


    private void SelectArcherCritChance()
    {
        this.selectedUpgrade = archerCritChanceUpgrade;
        StyleForArcher();
    }

    private void SelectArcherNew()
    {
        this.selectedUpgrade = archerNewUpgrade;
        StyleForArcher();
    }

    private void SelectArcherCritDamage()
    {
        this.selectedUpgrade = archerCritDamageUpgrade;
        StyleForArcher();
    }

    private void SelectArcherExtraArrow()
    {
        this.selectedUpgrade = archerExtraArrowUpgrade;
        StyleForArcher();
    }

    // New Upgrade System Castle
    private void RaiseCastleMenu()
    {
        GameObject.Find("CastleSelect").GetComponent<Animator>().SetTrigger("GoUp");
        this.selectedUpgrade = archerAttackDamageUpgrade;
        StyleForCastle();
    }

    private void LowerCastleMenu()
    {
        GameObject.Find("CastleSelect").GetComponent<Animator>().SetTrigger("GoDown");
    }

    private void StyleForCastle()
    {
        selectedUpgrade.SetUI();
        GameObject.Find("UpgradeImg").GetComponent<Image>().sprite = this.castleImage;
        StyleUpgradeBox(castleRed);
    }

    private void SelectCastleHealth()
    {
        this.selectedUpgrade = castleHealthUpgrade;
        StyleForCastle();
    }

    private void SelectCastleExtraGold()
    {
        this.selectedUpgrade = castleExtraGoldUpgrade;
        StyleForCastle();
    }

    private void SelectCastleInhabitantBuff()
    {
        this.selectedUpgrade = castleInhabitantBuffUpgrade;
        StyleForCastle();
    }

    private void SelectCastleRepairSpeed()
    {
        this.selectedUpgrade = castleRepairSpeedUpgrade;
        StyleForCastle();
    }

    // New Ballista Upgrades

    private void RaiseBallistaMenu()
    {
        GameObject.Find("BallistaSelect").GetComponent<Animator>().SetTrigger("GoUp");
        this.selectedUpgrade = archerAttackDamageUpgrade;
        StyleForBallista();
    }

    private void LowerBallistaMenu()
    {
        GameObject.Find("BallistaSelect").GetComponent<Animator>().SetTrigger("GoDown");
    }

    private void StyleForBallista()
    {
        selectedUpgrade.SetUI();
        GameObject.Find("UpgradeImg").GetComponent<Image>().sprite = this.ballistaImage;
        StyleUpgradeBox(ballistaYellow);
    }

    private void SelectBallistaDamage()
    {
        this.selectedUpgrade = ballistaDamageUpgrade;
        StyleForBallista();
    }

    private void SelectBallistaAdditionalBolt()
    {
        this.selectedUpgrade = ballistaAdditionalBoltUpgrade;
        StyleForBallista();
    }

    private void SelectBallistaReload()
    {
        this.selectedUpgrade = ballistaReloadSpeedUpgrade;
        StyleForBallista();
    }

    private void SelectBallistaAdditional()
    {
        this.selectedUpgrade = ballistaAdditionalUpgrade;
        StyleForBallista();
    }

    private void SelectBallistaAdditionalPierce()
    {
        this.selectedUpgrade = ballistaPierceUpgrade;
        StyleForBallista();
    }

    private void SelectBallistaBleedChance()
    {
        this.selectedUpgrade = ballistaBleedChanceUpgrade;
        StyleForBallista();
    }




    // Update is called once per frame
    void Update () {
		
	}

	void Purchase(){
		selectedUpgrade.Purchase ();
		for (int i = 0; i < abilities.Length; i++) {
			abilities [i].SetUI ();
		}
		Debug.Log ("Selected upgrade setting ui is: " + selectedUpgrade.buttonName);
		selectedUpgrade.SetUI ();
		GameControl.control.save ();
		Debug.Log ("Saved!");
	}
}

