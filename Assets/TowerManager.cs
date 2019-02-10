using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    Dictionary<string, GameObject> towers;

    public GameObject arrowTower;
    public GameObject barracks;
    public GameObject fireBoltTower;
    public GameObject flakTower;
    public GameObject flameThrowerTower;
    public GameObject pounderTower;
    public GameObject teslaTower;
    public GameObject torrentTower;

    public GameObject[] towerInsts;

    public GameObject sellMenu;

	// Use this for initialization
	void Start () {
        towers = new Dictionary<string, GameObject>();
        arrowTower.GetComponent<Tower>().Initialization();
        barracks.GetComponent<Tower>().Initialization();
        fireBoltTower.GetComponent<Tower>().Initialization();
        flakTower.GetComponent<Tower>().Initialization();
        flameThrowerTower.GetComponent<Tower>().Initialization();
        pounderTower.GetComponent<Tower>().Initialization();
        teslaTower.GetComponent<Tower>().Initialization();
        torrentTower.GetComponent<Tower>().Initialization();

        towers.Add(arrowTower.GetComponent<Tower>().towerName, arrowTower);
        towers.Add(barracks.GetComponent<Tower>().towerName, barracks); 
        towers.Add(fireBoltTower.GetComponent<Tower>().towerName, fireBoltTower);
        towers.Add(flakTower.GetComponent<Tower>().towerName, flakTower);
        towers.Add(flameThrowerTower.GetComponent<Tower>().towerName, flameThrowerTower);
        towers.Add(pounderTower.GetComponent<Tower>().towerName, pounderTower);
        towers.Add(teslaTower.GetComponent<Tower>().towerName, teslaTower);
        towers.Add(torrentTower.GetComponent<Tower>().towerName, torrentTower);

        towerInsts = new GameObject[8];

        ConstructAllTowers();
    } 

    // Update is called once per frame
    void Update () {
		
	}

    void SellTower(int slot)
    {
        if (GameControl.control.towers[slot] == null || GameControl.control.towers[slot] == "")
        {
            Debug.Log("There was not tower in that slot");
            return;
        }

        Tower tower = towerInsts[slot].GetComponent<Tower>();
        int sellValue = tower.GetSellValue();
        GameControl.control.AddGold(sellValue);
        GameControl.control.towers[slot] = "";
        GameControl.control.towerLevels[slot] = 0;
        Destroy(towerInsts[slot]);
        GameControl.control.save();
    }

    public int GetSellPrice(int slot)
    {
        if (towerInsts[slot] == null)
        {
            Debug.Log("There is not tower in slot " + slot + " to get the price for");
            return -1;
        }
        return towerInsts[slot].GetComponent<Tower>().GetSellValue();
    }

    public void ConstructTower(int slot)
    {
        if (GameControl.control.towers[slot] == null || GameControl.control.towers[slot] == "")
        {
            Debug.Log("There's no tower in slot " + slot + " to construct");
            return;
        }
        if (!towers.ContainsKey(GameControl.control.towers[slot])){
            Debug.Log("I do not know which tower this is: " + GameControl.control.towers[slot]);
        }
        if (towerInsts[slot] != null)
            Destroy(towerInsts[slot]);
        Vector3 position = GameObject.Find("SelectTowerButton " + (slot+1)).transform.position;
        GameObject instTower = null;
        towers.TryGetValue(GameControl.control.towers[slot], out instTower);
        GameObject newTower = Instantiate(instTower, position, new Quaternion(), null);
        towerInsts[slot] = newTower;
    }

    public void ConstructAllTowers()
    {
        for (int i = 0; i < 8; i++)
        {
            ConstructTower(i);
        }
    }

}
