using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapSoundEffect : MonoBehaviour {

    public void Flap()
    {
        AudioManager.manager.Play("flap");
    }

    public void Death()
    {
        AudioManager.manager.Play("demonDeath");
    }
}
