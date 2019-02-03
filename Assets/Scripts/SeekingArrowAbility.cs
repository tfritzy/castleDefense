using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SeekingArrowAbility : MonoBehaviour {

	private bool isGoing;

	private float startTime;
	public float cooldown = 15f;
	private float lastFireTime;

	public GameObject arrow;
	private GameObject[] targets;
	private int arrowsFired;
	private int numberOfArrowsToFire = 20;

	private float timeBetweenArrows = .05f;
	private float lastArrowFireTime;

	private Button button;
	// Use this for initialization
	void Start () {
		this.button = this.GetComponent<Button> ();
		if (GameControl.control.seekingArrowBarrageLevel > 0) {
			button.interactable = true;
		} else {
			button.interactable = false;
		}

		SetLevelLabel ();


	}

	public void SetLevelLabel(){
		GameObject.Find ("salevel").GetComponent<Text> ().text = GameControl.control.seekingArrowBarrageLevel.ToString ();
		if (GameControl.control.archerCount < 3) {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isGoing) {
			if (arrowsFired <= numberOfArrowsToFire && Time.time > lastArrowFireTime + timeBetweenArrows) {
				targets = GameObject.FindGameObjectsWithTag ("Enemy");
				GameObject arrowClone = Instantiate (arrow);
				if (targets.Length > 0) {
					GameObject target = targets [Random.Range (0, targets.Length - 1)];
					arrowClone.transform.position = new Vector3 (Random.Range (-17.0f, 17.0f), 10.0f, 0.0f);
					arrowClone.SendMessage ("SetDestination", (Vector2)(target.transform.position));
					arrowClone.SendMessage ("SetDamage", (1 * (1 + GameControl.control.arrowLevel * .1f)) * 3);
					arrowsFired += 1;
					lastArrowFireTime = Time.time;
					button.interactable = false;
				}

			}

			if (arrowsFired > numberOfArrowsToFire) {
				isGoing = false;
				lastFireTime = Time.time;
				arrowsFired = 0;
				button.interactable = false;
			}
		} else if (!button.IsInteractable() && GameControl.control.seekingArrowBarrageLevel > 0 && Time.time > lastFireTime + cooldown) {
			button.interactable = true;
		}
	}

	void Go(){

		this.isGoing = true;
			

	}
}
