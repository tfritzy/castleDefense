using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaltropsAbilityCollectable : collectable {

    public override void GiveValue()
    {
        GameControl.control.caltropsLevel += 1;
        GameObject.Find("CaltropsLevelLabelA").GetComponent<Text>().text = GameControl.control.caltropsLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("CaltropsA");
    }
}
