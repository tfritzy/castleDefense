using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour {

	/*
	public string sumarizeWhat;

	private RectTransform levelBar;
	private GameObject levelText;
	public int lastLevel;
	public int lastXp;
	public int currentLevel;
	public int currentXp;
	private int delta;
	private bool isSumarizing;
	private float updateFrequency;
	private float lastUpdateTime;
	private bool isSumarizingGold;
	float startTime;


	// Use this for initialization
	void Start () {
		if (sumarizeWhat == "Archer") {
			lastLevel = GameControl.control.lastArcherLevel;
			lastXp = GameControl.control.lastArcherXp;
			currentLevel = GameControl.control.archerLevel;
			currentXp = GameControl.control.archerXp;
			delta = (int)((float)Castle.GetNextLevelXp (lastLevel)*.05f);
			levelBar = transform.Find ("XpBar").gameObject.GetComponent<RectTransform>();
			levelText = transform.Find ("LevelText").gameObject;
			UpdateCastleBar();
		}
		if (sumarizeWhat == "Castle") {
			lastLevel = GameControl.control.lastCastleLevel;
			lastXp = GameControl.control.lastCastleXp;
			currentLevel = GameControl.control.castleLevel;
			currentXp = GameControl.control.castleXp;
			delta = (int)((float)Castle.GetNextLevelXp (lastLevel)*.05f);
			levelBar = transform.Find ("XpBar").gameObject.GetComponent<RectTransform>();
			levelText = transform.Find ("LevelText").gameObject;
			UpdateCastleBar();
		}
		if (sumarizeWhat == "Gold") {
			levelText = transform.Find ("GoldLabel").gameObject;
			lastLevel = GameControl.control.lastGold;
			currentLevel = GameControl.control.gold;
		}
		startTime = Time.time;
		updateFrequency = .05f;
		lastUpdateTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (isSumarizing && Time.time > startTime + 1.0f) {
			if (sumarizeWhat == "Archer" || sumarizeWhat == "Castle") {
				if (Time.time > lastUpdateTime + updateFrequency) {
					if (lastLevel < currentLevel || lastXp < currentXp) {
						lastXp += delta;
						if (lastXp > GetNextLevelXp (lastLevel)) {
							lastLevel += 1;
							lastXp = 0;
							delta = (int)((float)GetNextLevelXp (lastLevel)*.03f);
						}
						UpdateCastleBar ();
					} else {
						Debug.Log ("Done!");
						isSumarizing = false;
						this.transform.parent.gameObject.SendMessage ("" + sumarizeWhat + "Finished");
					}
					lastUpdateTime = Time.time;
				}
			}

			if (sumarizeWhat == "Gold") {
				if (lastLevel < currentLevel) {
					int amount = (int)((currentLevel - lastLevel) * .01f);
					if (amount == 0){
						amount = 1;
					}
					lastLevel += amount;
					levelText.GetComponent<Text> ().text = "Gold " + lastLevel;
				}
				if (lastLevel >= currentLevel){
					this.transform.parent.gameObject.SendMessage ("GoldFinished");
					this.isSumarizing = false;
					Debug.Log ("Finished the gold sir");
				}
			}
		}
			
	}

	private void Sumarize(){
		isSumarizing = true;
	}
	*/
		



}
