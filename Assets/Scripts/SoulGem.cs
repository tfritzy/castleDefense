using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoulGem : collectable {

    public override void GiveValue()
    {
        GameControl.control.soulGemCount += 1;
        GameObject.Find("SoulGemLabel").GetComponent<Text>().text = (GameControl.control.soulGemCount).ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("SoulGemLabel");
        this.isGoingToLabel = true;
    }
}
