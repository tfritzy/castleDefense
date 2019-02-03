using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBarrageCollectable : collectable {

    public override void GiveValue()
    {
        GameControl.control.seekingArrowBarrageLevel += 1;
        GameObject.Find("ArrowBarrageLevelLabelA").GetComponent<Text>().text = GameControl.control.seekingArrowBarrageLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("ArrowBarrageA");
    }

}
