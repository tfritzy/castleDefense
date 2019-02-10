using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTower : MonoBehaviour {

    public Tower selectedTowerScript;

    public int slot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Purchase()
    {
        Debug.Log(GameControl.control.towers[slot]);
        if (GameControl.control.towers[slot] != null && GameControl.control.towers[slot] != "")
        {
            Debug.Log("The tower slot was already full!");
            return;
        }
        int price = selectedTowerScript.GetCost();
        Debug.Log("Tower Name: " + selectedTowerScript.towerName);
        if (GameControl.control.gold >= price)
        {
            GameControl.control.gold -= price;
            GameControl.control.towers[slot] = selectedTowerScript.towerName;
            GameControl.control.towerLevels[slot] = 0;
            GameControl.control.save();
            GameObject.Find("TowerManager").SendMessage("ConstructTower", slot);
        }
        else
        {
            Debug.Log("Not enough gold!");
            return;
        }
        Destroy(this.gameObject);
    }

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }

    public void SetSelectedTower(Tower tower)
    {
        this.selectedTowerScript = tower;
    }

    public void Close()
    {
        Destroy(this.gameObject);
    }
}
