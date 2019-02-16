using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {

    public ItemType type;

    public abstract ItemType GetItemType();

    public int level;
    public int rarity;
    public float attackSpeed;
    public int damage;
    public float goldIncrease;
    public float magicFind;
    public float weaponDropChance;
    public int levelReqReduction;
    public float critHitChance;
    public float critHitDamage;
    public int castleHealth;
    public float range;

    public float cooldownReduction;
    public float xp;

    public override string ToString()
    {
        string outputString = "";
        outputString += " Level: " + level + "\n";
        outputString += " Rarity: " + rarity + ",";
        outputString += damage > 0f ? " Damage: " + damage + "," : "";
        outputString += attackSpeed > 0f ? " Attack Speed: " + attackSpeed + "," : "";
        outputString += goldIncrease > 0f ? " Gold Increase: " + goldIncrease + "," : "";
        outputString += xp > 0f ? " XP: " + xp + "," : "";
        outputString += magicFind > 0f ? " Magic Find: " + magicFind + "," : "";
        outputString += critHitChance> 0f ? " Crit Hit Chance: " + critHitChance+ "," : "";
        outputString += critHitDamage> 0f ? " Crit Hit Damage: " + critHitDamage+ "," : "";
        outputString += castleHealth> 0 ? " CastleHealth: " + castleHealth + "," : "";
        return outputString;
    }

    public Color32 GetColor()
    {
        if (rarity == 4)
            return new Color32(0, 255, 255, 255);
        else if (rarity == 3)
            return new Color32(200, 145, 0, 255);
        else if (rarity == 2)
            return new Color32(60, 120, 216, 255);
        else if (rarity == 1)
            return new Color32(182, 215, 168, 255);
        else
            return new Color32(217, 217, 217, 255);
    }
    
}

public enum ItemType
{
    Weapon, Shield, Boots, Leggings, Chest, Helm, Bracers, Gloves, Amplifier, EnergyCell
}
