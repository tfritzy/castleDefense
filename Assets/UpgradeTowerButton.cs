using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerButton : MonoBehaviour {

    public int slot;
    public GameObject levelUpPage;

	public void OpenMenu()
    {
        GameObject levelUpPageInst = Instantiate(levelUpPage, new Vector3(0, 0, 0), new Quaternion(), GameObject.Find("UI").transform);
        levelUpPageInst.SendMessage("SetSlot", slot);
        FillDetails(levelUpPageInst.transform);
        Destroy(this.gameObject);
    }

    public void FillDetails(Transform page)
    {
        Tower tower = GameObject.Find("TowerManager").GetComponent<TowerManager>().GetTowerInSlot(slot);
        GameObject.Find("TowerManager").SendMessage("SetFocusedTower", slot);
        tower.Initialization();
        page.Find("TowerName").GetComponent<Text>().text = tower.towerName;
        page.Find("PrevLevelValue").GetComponent<Text>().text = tower.level.ToString();
        page.Find("PrevDamageValue").GetComponent<Text>().text = tower.projectileDamage.ToString();
        page.Find("PrevRangeValue").GetComponent<Text>().text = tower.range.ToString() + "m";
        page.Find("PrevCooldownValue").GetComponent<Text>().text = tower.attackSpeed.ToString() + "s";
        page.Find("PurchaseButton").Find("UpgradeCostVal").GetComponent<Text>().text = tower.LevelUpCost().ToString();
        GameControl.control.towerLevels[slot] += 1;
        tower.Initialization();
        page.Find("NextDamageValue").GetComponent<Text>().text = tower.projectileDamage.ToString();
        page.Find("NextRangeValue").GetComponent<Text>().text = tower.range.ToString() + "m";
        page.Find("NextCooldownValue").GetComponent<Text>().text = tower.attackSpeed.ToString() + "s";
        page.Find("NextLevelValue").GetComponent<Text>().text = (tower.level).ToString();
        GameControl.control.towerLevels[slot] -= 1;
        tower.Initialization();
    }

    public void LevelUpTower()
    {
        GameObject.Find("TowerManager").SendMessage("LevelUpTower");
        OpenMenu();
    }

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }
}
