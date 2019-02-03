using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Upgrade{

	public string description;
	public string name;
	public string buttonName;
	public abstract int GetCost();
	public abstract string criterium1 ();
	public abstract string criterium2 ();
	public abstract string criterium3 ();
	public abstract void LevelUp();
	public abstract int GetLevel();
    public string errorMessage;

	public bool isAbility;

	public List<Upgrade> next;
	public List<Upgrade> prev;

    private Color invisible = new Color(1, 1, 1, 0);
    private Color visible = new Color(1, 1, 1, 1);
	public virtual void SetupLists(){
		this.next = new List<Upgrade> ();
		this.prev = new List<Upgrade> ();
	}

	public virtual void Setup(){
		
	}

    /*
     *  This method is called in SetupUI and is used by 
     *  children classes so that they can do custom ui
     *  updating. 
     * 
     * */
    public virtual void ExtraUISetup()
    {

    }




	public virtual bool IsMaxLevel(){
		return false;
	}
	public GameObject arrow;


	public virtual void SetDescription(){

	}

	public string CanPurchase(){
		if (this.isAbility) {
			if (GameControl.control.soulGemCount >= GetLevel()) {
				return null;
			} else {
				return "You have insufficient soul gems";
			}
		} else {

            // Right now I'm only calling criterium 1 and 3 because 2 was classically used to call CanPurchase()
            // And I don't feeling like going through all my upgrade classes and removing it. 
            if (criterium1() != "")
            {
                return criterium1();
            }
            else if (criterium3() != "")
            {
                return criterium3();
            }
            if (GameControl.control.gold >= GetCost ()) {
				return null;
			} else {
				return "You have insufficient gold";
			}
		}
	}

	public bool Purchase(){

		if (this.isAbility) {
			if (GameControl.control.soulGemCount >= GetLevel()) {
				GameControl.control.AddSoulGems (-1 * GetLevel());
				LevelUp ();
				return true;
			} else {
				return false;
			}
		} else {
			if (CanPurchase() == null) {
                GameControl.control.totalGold += GetCost();
                GameControl.control.AddGold (-1 * GetCost ());

				LevelUp ();
				GameControl.control.save ();
				return true;
			} else {
                SetDescription();
                return false;
			}
		}
		
	}

    private void SetArcherStats()
    {
        GameObject statContainer = GameObject.Find("ArcherStats");
        statContainer.transform.Find("CountValue").gameObject.GetComponent<Text>().text = GameControl.control.archerCount.ToString();
        statContainer.transform.Find("AttackSpeedValue").gameObject.GetComponent<Text>().text = (decimal.Round((decimal) (8f / (1f + (float)GameControl.control.archerAttackSpeed / 100f)), 1)) + "s";
        statContainer.transform.Find("DamageValue").gameObject.GetComponent<Text>().text = (4 + GameControl.control.archerDamageLevel).ToString();
        statContainer.transform.Find("CritChanceValue").gameObject.GetComponent<Text>().text = GameControl.control.archerCritChance + "%";
        statContainer.transform.Find("CritDamageValue").gameObject.GetComponent<Text>().text = (200 + GameControl.control.archerCritDamage) + "%";
        statContainer.transform.Find("DoubleFireValue").gameObject.GetComponent<Text>().text = GameControl.control.archerExtraArrowChance + "%";
        statContainer.transform.Find("AbilityPowerValue").gameObject.GetComponent<Text>().text = (100 + GameControl.control.archerAbilityPower) + "%";
        
    }

    private void SetMageStats()
    {
        GameObject statContainer = GameObject.Find("MageStats");
        statContainer.transform.Find("CountValue").gameObject.GetComponent<Text>().text = GameControl.control.mageCount.ToString();
        statContainer.transform.Find("AttackSpeedValue").gameObject.GetComponent<Text>().text = (100 + GameControl.control.mageAttackSpeed) + "%";
        statContainer.transform.Find("DamageValue").gameObject.GetComponent<Text>().text = (4 + GameControl.control.extraMageDamage).ToString();
        statContainer.transform.Find("SlowAmountValue").gameObject.GetComponent<Text>().text = GameControl.control.mageSlow + "%";
        statContainer.transform.Find("SplashRadiusValue").gameObject.GetComponent<Text>().text = (float)(GameControl.control.mageSplashRadius * .2f) + "m";
        statContainer.transform.Find("AbilityPowerValue").gameObject.GetComponent<Text>().text = (100 + GameControl.control.mageAbilityPower) + "%";
    }

    private void SetCastleStats()
    {
        GameObject statContainer = GameObject.Find("CastleStats");
        statContainer.transform.Find("HealthValue").GetComponent<Text>().text = (GameControl.control.castleHealth + 5000).ToString();
        statContainer.transform.Find("ExtraGoldValue").GetComponent<Text>().text = (GameControl.control.extraGoldPercentage).ToString();
        statContainer.transform.Find("InhabitantBuffValue").GetComponent<Text>().text = (GameControl.control.castleInhabitantBuff).ToString();
        statContainer.transform.Find("RepairSpeedValue").GetComponent<Text>().text = (GameControl.control.repairSpeed).ToString();

    }

    private void SetBallistaStats()
    {
        GameObject statContainer = GameObject.Find("BallistaStats");
        statContainer.transform.Find("PierceCountValue").GetComponent<Text>().text = (GameControl.control.pierceLevel + 1).ToString();
        statContainer.transform.Find("BleedChanceValue").GetComponent<Text>().text = (GameControl.control.bleedChance).ToString() + "%";
        statContainer.transform.Find("DamageValue").GetComponent<Text>().text = (Ballista.GetMaxDamage()).ToString();
        statContainer.transform.Find("BoltCountValue").GetComponent<Text>().text = (GameControl.control.extraBallistaArrows+1).ToString();
        statContainer.transform.Find("ReloadTimeValue").GetComponent<Text>().text = (decimal.Round((decimal)Ballista.GetReloadTime(),1)).ToString() + " s";
        statContainer.transform.Find("BallistaCountValue").GetComponent<Text>().text = (GameControl.control.ballistaCount + 1).ToString();
    }

    public string SetUI(){
		SetDescription ();

        // Find ui elements
        Image goldImg = GameObject.Find("goldImageCost").GetComponent<Image>();
        Image silverImg = GameObject.Find("silverImageCost").GetComponent<Image>();
        Image bronzeImg = GameObject.Find("bronzeImageCost").GetComponent<Image>();
        Image soulGemImg = GameObject.Find("soulGemImage").GetComponent<Image>();
        Text soulGemLabelUI = GameObject.Find("SoulGemLabel").GetComponent<Text>();
        Text goldText = GameObject.Find("goldCost").GetComponent<Text>();
        Text silverText = GameObject.Find("silverCost").GetComponent<Text>();
        Text bronzeText = GameObject.Find("bronzeCost").GetComponent<Text>();
        Text purchaseButtonText = GameObject.Find("PurchaseUI").GetComponent<Text>();
        Text levelLabel = GameObject.Find("LevelValue").GetComponent<Text>();
        Text upgradeDescription = GameObject.Find("UpgradeDescription").GetComponent<Text>();
        Text title = GameObject.Find("UpgradeTitle").GetComponent<Text>();
        GameObject purchaseButton = GameObject.Find("Purchase");
        

        // Setup labels in the description box
        levelLabel.text = GetLevel().ToString();
        upgradeDescription.text = this.description;
        title.text = this.name;
        soulGemLabelUI.text = GameControl.control.soulGemCount.ToString();

        // Set the text on the purchase button
        purchaseButton.GetComponent<Button>().interactable = (CanPurchase() == null);
        purchaseButtonText.text = CanPurchase() == null ? "Purchase" : CanPurchase();

        if (this.isAbility)
        {
            // Set the current level label to this abilites level
            GameObject.Find(this.buttonName + "LevelLabel").GetComponent<Text>().text = GetLevel().ToString();
            GameObject.Find(this.buttonName + "LevelLabelA").GetComponent<Text>().text = GetLevel().ToString();
            GameObject.Find(this.buttonName).GetComponent<Button>().interactable = (GetLevel() > 0);
            bronzeText.text = GetLevel().ToString();
            goldImg.color = invisible;
            bronzeImg.color = invisible;
            silverImg.color = invisible;
            soulGemImg.color = visible;

            
        } else
        {
            // Calculate how many of each coin the upgrade costs
            int cost = GetCost();
            int goldCoins = cost / 100;
            int silverCoins = cost % 100 / 10;
            int bronzeCoins = cost % 100 % 10;

            // Set the text for each gold cost label
            goldText.text = (goldCoins > 0) ? goldCoins.ToString() : "";
            silverText.text = (silverCoins > 0) ? silverCoins.ToString() : "";
            bronzeText.text = (bronzeCoins > 0) ? bronzeCoins.ToString() : "";

            // Set Colors of labels
            goldImg.color = (goldCoins > 0) ? visible : invisible;
            silverImg.color = (silverCoins > 0) ? visible : invisible;
            bronzeImg.color = (bronzeCoins > 0) ? visible : invisible;
            soulGemImg.color = invisible;

        }

        // Set the Statistics sections for each class
        SetArcherStats();
        SetMageStats();
        SetCastleStats();
        SetBallistaStats();

        // Perform UI Setup that is custom to children classes.
        ExtraUISetup();

        // I don't know why I return name here... But I don't want to remove it or find out why.
        return this.name;
	}


}
