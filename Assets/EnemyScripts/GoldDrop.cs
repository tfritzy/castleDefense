using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldDrop : MonoBehaviour {

	private int value;
	public GameObject sparkle;
	public GameObject coinLettering;
	public float instantiationTime;
	public GameObject coin;
	public Sprite bronzeCoin;
	public Sprite silverCoin;
	public Sprite goldCoin;
	private int goldCount;
	private int silverCount;
	private int bronzeCount;

	// Use this for initialization
	void Start () {
		instantiationTime = Time.time;

	}


	void SetValue(int amount){
		this.value = amount;
		int goldCount = amount / 100;
		amount = amount % 100;
		int silverCount = amount / 10;
		amount = amount % 10;
		int bronzeCount = amount;
        if (goldCount == 0 && silverCount == 0 && bronzeCount == 0)
        {
            Destroy(this.gameObject);
            return;
        }
		GameObject instCoin = null;
		for (int i = 0; i < goldCount; i++) {
			instCoin = Instantiate (coin);
			instCoin.SendMessage ("SetValue", 100);
			instCoin.GetComponentInChildren<SpriteRenderer> ().sprite = goldCoin;
			instCoin.transform.localScale = new Vector3 (.3f, .3f, .3f);
			Vector3 oldLoc = this.transform.position;
			oldLoc.z = -10;
			instCoin.transform.position = oldLoc;

		}
		for (int i = 0; i < silverCount; i++) {
			instCoin = Instantiate (coin);
			instCoin.SendMessage ("SetValue", 10);
			instCoin.GetComponentInChildren<SpriteRenderer> ().sprite = silverCoin;
			instCoin.transform.localScale = new Vector3 (.23f, .23f, .23f);
			instCoin.transform.position = this.transform.position;
			Vector3 oldLoc = this.transform.position;
			oldLoc.z = -5;
			instCoin.transform.position = oldLoc;
		}
		for (int i = 0; i < bronzeCount; i++) {
			instCoin = Instantiate (coin);
			instCoin.SendMessage ("SetValue", 1);
			instCoin.GetComponentInChildren<SpriteRenderer> ().sprite = bronzeCoin;
			instCoin.transform.position = this.transform.position;
		}
		instCoin.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (-3f, 3f), Random.Range (-3f, 3f));

		Destroy (this.gameObject);




	}
}
