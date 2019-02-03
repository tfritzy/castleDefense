using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour {

	bool isReloading;
	public bool isReloadingX;

	private RectTransform reloadBar;
	private Image reloadImg;
	float reloadTime;
	float progress;
	float startTime;
	// Use this for initialization
	void Start () {
		reloadBar = this.gameObject.GetComponent<RectTransform> ();
		reloadImg = reloadBar.GetComponent<Image> ();
		isReloadingX = true;
		reloadImg.color = new Color (0, 255, 0, .5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isReloading) {
			Vector3 currentScale = reloadBar.localScale;
			float percentage = ((Time.time - startTime) / reloadTime);
			if (percentage < .2f) {
				reloadImg.color = new Color (255, 0, 0, .5f);
			}
			if (percentage >= .2f && percentage < .6f) {
				reloadImg.color = new Color (255, 255, 0, .5f);
			}
			if (percentage >= .6f) {
				reloadImg.color = new Color (0, 255, 0, .5f);
			}

			if (isReloadingX) {
				currentScale.x = percentage;
			} else {
				currentScale.y = percentage;
			}
			reloadBar.localScale = currentScale;
			if (Time.time > startTime + reloadTime) {
				isReloading = false;
			}
		}
	}

	public void StartRelaod(){
		this.isReloading = true;
		startTime = Time.time;
		this.progress = 0;
	}

	public void SetReloadTime(float time){
		this.reloadTime = time;
	}

	public void ReloadByY(){
		isReloadingX = false;
	}

}
