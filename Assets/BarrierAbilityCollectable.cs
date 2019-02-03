using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierAbilityCollectable : collectable {

    public override void GiveValue()
    {
        GameControl.control.barrierLevel += 1;
        GameObject.Find("DefensiveBarrierLevelLabelA").GetComponent<Text>().text = GameControl.control.barrierLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("DefensiveBarrierA");
    }
}
