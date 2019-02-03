using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultSounds : MonoBehaviour {

	public void Death()
    {
        AudioManager.manager.Play("catapultDeath");
    }

    public void Attack()
    {
        AudioManager.manager.Play("catapultFire");
    }
}
