using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBangAbility : MonoBehaviour {


	public GameObject flashBangArrow;
	private float instantiationTime;
	public float cooldown;
	public float lastFireTime;
	private float timeUntilExplosion = 2.0f;
	private bool going;
	private GameObject arrow;

	private Button button;

	void Start(){
		button = this.GetComponent<Button> ();
		if (GameControl.control.blizzardAbilityLevel > 0) {
			button.interactable = true;
		} else {
			button.interactable = false;
		}
	}

	void Go() {

		arrow = Instantiate (flashBangArrow);
		arrow.GetComponent<Rigidbody2D> ().velocity = new Vector2 (7, 10);
		instantiationTime = Time.time;
		going = true;
		this.GetComponent<Button> ().interactable = false;

	}
		
	// Update is called once per frame
	void Update () {
		if (going) {
			if (Time.time > instantiationTime + timeUntilExplosion && GameControl.control.blizzardAbilityLevel > 0) {
				Destroy (arrow);
				GameObject[] objects = GameObject.FindGameObjectsWithTag ("Enemy");
				for (int i = 0; i < objects.Length; i++) {
					objects [i].SendMessage ("SetParalyzeDuration", 5.0f);
					objects [i].SendMessage ("Paralyze");
				}
				lastFireTime = Time.time;
				going = false;
				button.interactable = false;
			}
		} else if (!button.IsInteractable() && GameControl.control.blizzardAbilityLevel > 0 && Time.time > lastFireTime + cooldown) {
			button.interactable = true;
		}
	}
}
