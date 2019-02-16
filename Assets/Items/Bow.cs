using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public override WeaponType GetWeaponType()
    {
        return WeaponType.Bow;
    }
}
