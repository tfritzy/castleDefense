using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlizzardAbilityCollectable : collectable {

    public override void GiveValue()
    {
        GameControl.control.blizzardAbilityLevel += 1;
        GameObject.Find("BlizzardLevelLabelA").GetComponent<Text>().text = GameControl.control.blizzardAbilityLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("BlizzardA");
    }
}
