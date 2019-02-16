using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollItem : MonoBehaviour {

    public GameObject itemContainer;

    private List<Action> itemAttributeSetters = new List<Action>();
    private int[] itemAttributeWeights;
    private Item[] itemTypes;
    private int[] itemTypeWeights;

    private Item itemToBeRolled;

    private int attackSpeedAttributeStartlevel = 5;
    private int castleHealthStartLevel = 10;
    private int goldFfindAttributeStartLevel = 10;
    private int critHitChanceStartLevel = 20;
    private int critHitDamageStartLevel = 20;
    private int magicFindAttributeStartLevel = 30;
    private int xpIncreaseAttributeStartLevel = 30;

    public void Start()
    {
        itemAttributeSetters.Add(() => RollDamage(itemToBeRolled));
        itemAttributeSetters.Add(() => RollAttackSpeed(itemToBeRolled));
        itemAttributeSetters.Add(() => RollGoldFind(itemToBeRolled));
        itemAttributeSetters.Add(() => RollMagicFind(itemToBeRolled));
        itemAttributeSetters.Add(() => RollXPIncrease(itemToBeRolled));
        itemAttributeSetters.Add(() => RollCritHitChance(itemToBeRolled));
        itemAttributeSetters.Add(() => RollCritHitDamage(itemToBeRolled));
        itemAttributeSetters.Add(() => RollCastleHealth(itemToBeRolled));
    }

    public void SpawnItem(Vector3 location)
    {
        Item newItem = GenerateItem(UnityEngine.Random.Range(1, 100));
        GameObject inst = Instantiate(itemContainer, location, new Quaternion(), null);
        inst.GetComponent<SpriteRenderer>().color = newItem.GetColor();
        inst.SendMessage("SetItem", newItem);
    }

    public Item GenerateItem(int level)
    {
        SetAttributeWeights(level);
        ResetItemTypes();
        itemToBeRolled = GetRandomItemType();
        itemToBeRolled.level = level;
        RollItemRarity();
        RollStats();
        return itemToBeRolled;
    }

    private void ResetItemTypes()
    {
        itemTypeWeights = new int[] { 1, 1, 1, 1 };
        itemTypes = new Item[] { new Helm(), new Bow(), new Chest(), new Boots() };
    }

    private void SetAttributeWeights(int level)
    {
        itemAttributeWeights = new int[] {
            3 + level / 10, // damage
            level >= attackSpeedAttributeStartlevel ? 1 + level / 20: 0, 
            level >= goldFfindAttributeStartLevel ? 1 + level / 20 : 0, 
            level >= magicFindAttributeStartLevel ? 1 + level / 20 : 0, 
            level >= xpIncreaseAttributeStartLevel ? 1 + level / 20 : 0, 
            level >= critHitChanceStartLevel ? 1 + level / 20 : 0, 
            level >= critHitDamageStartLevel ? 1 + level / 20 : 0, 
            level >= castleHealthStartLevel ? 1 + level / 20 : 0, 
        };
    }

    private void RollItemRarity()
    {
        float roll = UnityEngine.Random.Range(0, 100);
        if (roll >= 99.5f)
            itemToBeRolled.rarity = 4;
        else if (roll >= 97.5f)
            itemToBeRolled.rarity = 3;
        else if (roll >= 90f)
            itemToBeRolled.rarity = 2;
        else if (roll >= 70f)
            itemToBeRolled.rarity = 1;
        else
            itemToBeRolled.rarity = 0;
    }

    public Item GetRandomItemType()
    {
        if (itemTypes.Length != itemTypeWeights.Length)
            Debug.LogError("There's an issue with the item weights!");

        int totalWeight = 0;
        foreach( int weight in itemTypeWeights)
        {
            totalWeight += weight;
        }
    
        int roll = UnityEngine.Random.Range(1, totalWeight+1);
        int weightCounter = 0;
        for (int i = 0; i < itemTypeWeights.Length; i++)
        {
            weightCounter += itemTypeWeights[i];
            if (weightCounter >= roll)
            {
                return itemTypes[i];
            }
            
        }
        return null;
    }

    private void RollAStat()
    {
        int totalWeight = 0;
        foreach (int weight in itemAttributeWeights)
            totalWeight += weight;

        int roll = UnityEngine.Random.Range(1, totalWeight + 1);

        int weightCounter = 0;
        for (int i = 0; i < itemAttributeWeights.Length; i++)
        {
            weightCounter += itemAttributeWeights[i];

            if (weightCounter >= roll && itemAttributeWeights[i] != 0)
            {
                itemAttributeSetters[i]();
                itemAttributeWeights[i] = 0;
                return;
            }
            
        }
    }

    private void RollStats()
    {
        int attributeCount = GetNumAttributes(itemToBeRolled);
        if (attributeCount > itemAttributeWeights.Length)
            attributeCount = itemAttributeWeights.Length;
        for (int i = 0; i < attributeCount; i++)
            RollAStat();
    }

    private int GetNumAttributes(Item item)
    {
        int attrCount = (int)Mathf.Ceil(item.level / 20f);
        
        if (item.rarity == 2)
            attrCount += 1;
        if (item.rarity == 3 || item.rarity == 4)
            attrCount += 2;
        return attrCount;
    }

    private void RollDamage(Item item)
    {
        item.damage = Mathf.Max(1, (int)(
            UnityEngine.Random.Range(2f * Mathf.Pow(1.089f, item.level) * .9090909f,
            2f * Mathf.Pow(1.089f, item.level) * 1.09090909f)));
    }

    private void RollAttackSpeed(Item item)
    {
        float maxValue = item.level * .4f;
        item.attackSpeed = Mathf.Max(0, UnityEngine.Random.Range(maxValue * .6f, maxValue));
        item.attackSpeed = Mathf.Round(item.attackSpeed);
    }

    private void RollGoldFind(Item item)
    {
        float maxValue = item.level;
        item.goldIncrease = Mathf.Round(UnityEngine.Random.Range(maxValue * .7f, maxValue));
    }

    private void RollMagicFind(Item item)
    {
        float maxValue = item.level * .1f;
        item.magicFind = Mathf.Round(UnityEngine.Random.Range(maxValue * .7f, maxValue));
    }

    private void RollXPIncrease(Item item)
    {
        float maxValue = item.level * 2;
        item.xp = Mathf.Round(UnityEngine.Random.Range(maxValue * .7f, maxValue));
    }

    private void RollCritHitChance(Item item)
    {
        float maxValue = item.level * (7.0f / 100f);
        item.critHitChance = Mathf.Round(UnityEngine.Random.Range(maxValue * .7f, maxValue));
    }

    private void RollCritHitDamage(Item item)
    {
        float maxValue = item.level;
        item.critHitDamage = Mathf.Round(UnityEngine.Random.Range(maxValue * .7f, maxValue));
    }

    private void RollCastleHealth(Item item)
    {
        float maxValue = item.level * 10;
        item.castleHealth = (int)Mathf.Round(UnityEngine.Random.Range(maxValue * .7f, maxValue));
    }

}
