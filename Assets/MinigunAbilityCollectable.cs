using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigunAbilityCollectable : collectable {

    public override void GiveValue()
    {
        GameControl.control.minigunLevel += 1;
        GameObject.Find("MinigunLevelLabelA").GetComponent<Text>().text = GameControl.control.minigunLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("MinigunA");
    }

}