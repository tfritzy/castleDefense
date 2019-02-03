using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireWave : MonoBehaviour {

	public GameObject wave;

	private int waveDamage;
	public float cooldown = 30f;
	private float lastCastTime;
	private int caltropCount;

	private Text cooldownTimerLabel;

	private bool isDisabled;
    private Vector2 lastSpawnLoc;
    private float lastSpawnTime;
    private float timeBetweenSpawns = .2f;
    private bool isGoing;

	private void Go(){
		if (Time.time > lastCastTime + cooldown) {
            this.isGoing = true;
			lastCastTime = Time.time;
            this.cooldown = 30;
		}
	}

	void Start(){
		this.lastCastTime = -100f;
        this.waveDamage = (int)((GameControl.control.fireWaveLevel * 1 + 2) * (1f + GameControl.control.mageAbilityPower / 100f));
		this.cooldownTimerLabel = GameObject.Find ("FireWaveTimer").GetComponent<Text> ();
        GameObject ground = GameObject.Find("Ground");
        this.lastSpawnLoc = new Vector3(GameObject.Find("Castle").transform.position.x, ground.transform.position.y + ground.GetComponent<BoxCollider2D>().size.y, 0f);
	}

	
	// Update is called once per frame
	void Update () {
        if (isGoing)
        {
            if (Time.time > lastSpawnTime + timeBetweenSpawns)
            {
                GameObject waveInst = Instantiate(this.wave);
                waveInst.transform.position = lastSpawnLoc;
                waveInst.SendMessage("SetDamage", this.waveDamage);
                lastSpawnLoc.x += 3f;
                lastSpawnTime = Time.time;
                if (lastSpawnLoc.x > 25)
                {
                    GameObject ground = GameObject.Find("Ground");
                    this.lastSpawnLoc = new Vector3(GameObject.Find("Castle").transform.position.x, ground.transform.position.y + ground.GetComponent<BoxCollider2D>().size.y, 0f);
                    this.isGoing = false;
             
                }
            }
        } else
        {
            if (GameControl.control.fireWaveLevel > 0)
            {
                if (Time.time > lastCastTime + this.cooldown)
                {
                    this.cooldownTimerLabel.text = "";
                    this.GetComponent<Button>().interactable = true;
                }
                else
                {
                    this.GetComponent<Button>().interactable = false;
                    this.cooldownTimerLabel.text = (this.cooldown - (int)(Time.time - this.lastCastTime)).ToString();

                }
            } else
            {
                this.GetComponent<Button>().interactable = false;
            }
        }
	}
}
