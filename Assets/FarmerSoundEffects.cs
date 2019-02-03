using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerSoundEffects : MonoBehaviour {

    public void Step()
    {
        AudioManager.manager.Play("smallStep");
    }

    public void Die()
    {
        AudioManager.manager.Play("humanDie");
    }
}
