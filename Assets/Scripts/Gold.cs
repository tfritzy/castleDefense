using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : collectable {

    public override void GiveValue()
    {
        GameControl.control.AddGold(this.value);
        GameObject.Find("GoldLabel").GetComponent<Text>().text = (GameControl.control.gold / 100).ToString();
        GameObject.Find("SilverLabel").GetComponent<Text>().text = (GameControl.control.gold % 100 / 10).ToString();
        GameObject.Find("BronzeLabel").GetComponent<Text>().text = (GameControl.control.gold % 100 % 10).ToString();

    }

    public override void SetTargetLabel()
    {
        this.isGoingToLabel = true;
        if (this.value == 1)
        {
            this.targetLabel = GameObject.Find("BronzeLabel");
            this.collectionSoundEffectName = "acqBronze";
        }
        else if (this.value == 10)
        {
            this.targetLabel = GameObject.Find("SilverLabel");
            this.collectionSoundEffectName = "acqSilver";
        }
        else
        {
            this.targetLabel = GameObject.Find("GoldLabel");
            this.collectionSoundEffectName = "acqGold";
        }
    }
}
