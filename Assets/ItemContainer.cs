using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : collectable {

    public Item item;

    private GameObject itemManager;

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public override void GiveValue()
    {
        if (itemManager == null)
            itemManager = GameObject.Find("ItemManager");
        itemManager.SendMessage("AddItemToTempStorage", item);
    }

    public override void SetTargetLabel()
    {
        targetLabel = GameObject.Find("Castle");
    }
}
