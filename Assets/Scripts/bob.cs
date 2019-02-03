using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bob : MonoBehaviour {

	private Rigidbody2D rb;
	private bool goingUp;
	float frequency = 3f;
	float amplitude = 1.5f;
	float lastCheck;
	public GameObject sparkle;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.touchCount > 0) {
			if ((Input.GetTouch (0).position - (Vector2)this.transform.position).magnitude < 2.0f) {
				GameObject sparkleInst = Instantiate (sparkle);
				sparkleInst.transform.position = this.transform.position;
				Debug.Log("" + this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length / this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).speed);
				Destroy (sparkleInst, this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length / this.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).speed);
				RewardGold ();

			}
		}


		if (((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)this.transform.position).magnitude < 2.0f) {
			GameObject sparkleInst = Instantiate (sparkle);
			sparkleInst.transform.position = this.transform.position;
			Destroy (sparkleInst, sparkleInst.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length / sparkleInst.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).speed);
			RewardGold ();
		}

	}

	void RewardGold(){
		GameControl.control.soulGemCount += 1;
		Destroy (this.gameObject);
		GameObject.Find ("SoulGemLabel").GetComponent<Text> ().text = GameControl.control.soulGemCount.ToString ();
		GameControl.control.save ();
	}
}
