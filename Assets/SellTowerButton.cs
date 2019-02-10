using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellTowerButton : MonoBehaviour {

    public int slot;
    public GameObject confirmSellMenu;

	void SetSlot(int slot)
    {
        this.slot = slot;
    }

    public void Sell()
    {
        GameObject confirmInst = Instantiate(confirmSellMenu, Vector3.zero, new Quaternion(), GameObject.Find("UI").transform);
        confirmInst.transform.Find("AreYouSureText").GetComponent<Text>().text = 
            "Are you sure you want to sell the tower for " + 
            GameObject.Find("TowerManager").GetComponent<TowerManager>().GetSellPrice(slot) + " gold?";
        confirmInst.SendMessage("SetSlot", slot);
    }
}
