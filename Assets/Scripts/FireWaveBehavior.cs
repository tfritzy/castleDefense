using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaveBehavior : MonoBehaviour {

	public int damage;
	private float movementSpeed;
	private bool movesHorizontally;
	private Vector2 offSet;
	private Hashtable recentHits;
    float lastZChangeTime;
	private void SetDamage(int damage){
		this.damage = damage;
	}

	// Use this for initialization
	void Start () {
		this.offSet = new Vector2 (2f, 12f);
		recentHits = new Hashtable ();
        AudioManager.manager.Play("fireWave");
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > lastZChangeTime + .5f)
        {
            Vector3 curPos = this.transform.position;
            curPos.z -= .1f;
            this.transform.position = curPos;
        }
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("SetLastAttacker", "Mage");
            collision.gameObject.SendMessage("TakeDamage", this.damage);
        }
    }




}
