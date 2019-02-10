using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmSellTower : MonoBehaviour {

    public int slot;

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }

    public void ConfirmSell()
    {
        GameObject.Find("TowerManager").SendMessage("SellTower", slot);
        Destroy(this.gameObject);
    }

    public void DeclineSell()
    {
        Destroy(this.gameObject);
    }
}
