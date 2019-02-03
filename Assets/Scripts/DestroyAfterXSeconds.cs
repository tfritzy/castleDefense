using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour {

	private float instantiationTime;
	public float x;

	// Use this for initialization
	void Start () {
		instantiationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > instantiationTime + x) {
			Destroy (this.gameObject);
		}
	}
}
