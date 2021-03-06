using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierAbility : MonoBehaviour {

	private float instantiationTime;
	public float cooldown = 30f;
	public float lastFireTime;
	public GameObject wall;
	private Button button;
	private int wallDPS;
	private int wallHealth;
	private Text cooldownText;

	void Start(){
		button = this.GetComponent<Button> ();
		this.wallDPS = GameControl.control.barrierLevel * 1 + 1;
		this.wallHealth = 500 + GameControl.control.barrierLevel * 200;
        this.cooldown = GetCooldown();
		this.lastFireTime = -1f * cooldown - 1;
		this.cooldownText = GameObject.Find ("DefensiveBarrierTimer").GetComponent<Text> ();
		if (GameControl.control.barrierLevel > 0) {
			button.interactable = true;
		} else {
			button.interactable = false;
		}
	}

    float GetCooldown()
    {
        return 30f;
    }

	void Go() {
		
		GameObject alreadyWall = GameObject.Find ("Barrier");
		if (alreadyWall != null) {
			alreadyWall.SendMessage ("LevelUp");
		} else {
			GameObject wallInst = Instantiate (wall);
			Vector2 castleLocation = GameObject.Find ("Castle").transform.position;
			Vector2 wallLoc = new Vector2 (castleLocation.x + 14f, castleLocation.y - 3.25f);
			wallInst.transform.position = (Vector3)wallLoc;
			wallInst.SendMessage ("AddHealth", this.wallHealth);
			wallInst.SendMessage ("SetDamage", this.wallDPS);
		}
			
		lastFireTime = Time.time;
		button.interactable = false;
	}

	// Update is called once per frame
	void Update () {
		if (GameControl.control.barrierLevel > 0) {
			if (Time.time > lastFireTime + this.cooldown) {
				this.GetComponent<Button> ().interactable = true;
			} else {
				this.GetComponent<Button> ().interactable = false;
			}
		} else {
			this.GetComponent<Button> ().interactable = false;
		}

		float timeRemaining = (this.cooldown - (Time.time - this.lastFireTime));
		if (timeRemaining > 0) {
			this.cooldownText.GetComponent<Text> ().text = ((int)timeRemaining).ToString ();
		} else {
			this.cooldownText.GetComponent<Text> ().text = "";
		}

	}
}
