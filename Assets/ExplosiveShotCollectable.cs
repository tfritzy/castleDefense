using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExplosiveShotCollectable : collectable {

    public override void GiveValue()
    {
        GameControl.control.explosiveShotLevel += 1;
        GameObject.Find("ExplosiveShotLevelLabelA").GetComponent<Text>().text = GameControl.control.explosiveShotLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("ExplosiveShotA");
    }
}
