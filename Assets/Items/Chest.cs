using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Item
{
    public override ItemType GetItemType()
    {
        return ItemType.Chest;
    }
}
