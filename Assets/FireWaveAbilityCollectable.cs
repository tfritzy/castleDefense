using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireWaveAbilityCollectable : collectable {

    public override void GiveValue()
    {
        GameControl.control.fireWaveLevel += 1;
        GameObject.Find("FireWaveLevelLabelA").GetComponent<Text>().text = GameControl.control.fireWaveLevel.ToString();
        GameControl.control.save();
    }

    public override void SetTargetLabel()
    {
        this.targetLabel = GameObject.Find("FireWaveA");
    }
}
