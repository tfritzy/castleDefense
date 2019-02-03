using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FanOfBoltsCollectable : collectable {
    public override void GiveValue()
    {
        GameControl.control.fanOfBoltsLevel += 1;
        GameObject.Find("FanOfBoltsLevelLabelA").GetComponent<Text>().text = GameControl.control.fanOfBoltsLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("FanOfBoltsA");
    }
}
