using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour {

	public int health;
	public int maxHealth;
	private float lastRepairTime;
	public int nextLevelXp;
	private bool isDead;
	public GameObject gameOverScreen;
    public string lastAttacker;

	// Use this for initialization
	void Start () {
		maxHealth = 2000 + GameControl.control.castleHealth;
		health = maxHealth;

		UpdateHealthBar ();
	}
	
	// Update is called once per frame
	void Update () {
		repair ();
	}

	void TakeDamage(int amount){
		health -= amount;
		UpdateHealthBar ();
		if (health <= 0 && isDead == false) {
            this.health = 0;
			isDead = true;
			GameLose ();
		}

	}

    private void SetLastAttacker(string attackerName)
    {
        this.lastAttacker = attackerName;
    }

	private void GameLose(){

        GameObject.Find("LevelManager").SendMessage("EndLevel");
        this.isDead = false;

	}

	void repair(){
		if (health < maxHealth) {
			if (Time.time > lastRepairTime + .2f){
				int amount = GameControl.control.repairSpeed;
				health += amount;
				if (health > maxHealth) {
					health = maxHealth;
				}
				lastRepairTime = Time.time;
				UpdateHealthBar ();
			}
		}
	}

	void UpdateHealthBar(){
		Transform healthbar = GameObject.Find ("CastleHealthBar").transform;
		Transform healthText = healthbar.transform.Find ("CastleHealthCanvas").Find("CastleHealth");
		healthText.gameObject.GetComponent<Text> ().text = "" + this.health + "/" + this.maxHealth;
		Transform greenbar = healthbar.Find ("Greenbar");
		Image green = greenbar.GetComponent<Image> ();
		Vector3 newScale = green.rectTransform.localScale;
		newScale.x = (float)((float)health / (float)maxHealth);
		green.rectTransform.localScale = newScale;
	}
		



}
