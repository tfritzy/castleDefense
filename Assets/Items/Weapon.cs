using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public override ItemType GetItemType()
    {
        return ItemType.Weapon;
    }

    public abstract WeaponType GetWeaponType();
}

public enum WeaponType
{
    Bow, Staff, Sword
}
