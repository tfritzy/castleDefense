using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardBody : MonoBehaviour {

	private float PI = Mathf.PI;
	public GameObject icicle;
	private float slowAmount;
	private int damage;
	private int icicleDamage;

	private int iciclesPerVolley = 16;
	private float[] icicleStartAngles;
	private float icicleVelocity = 5;
	// Timing variables
	private float lastIcicleVolley;

	// Use this for initialization
	void Start () {
		// We'll try different values.
		this.GetComponent<Rigidbody2D> ().angularVelocity = 120f;

		this.damage = (int)((10 + 8 * GameControl.control.blizzardAbilityLevel)  *(1f + GameControl.control.mageAbilityPower / 100f));

		if (GameControl.control.blizzardAbilityLevel > 9) {
			this.iciclesPerVolley = 24;
		} else {
			this.iciclesPerVolley = 12;
		}

		this.icicleDamage = (int)((4 + GameControl.control.blizzardAbilityLevel) * (1f + GameControl.control.mageAbilityPower / 100f));

		icicleStartAngles = new float[iciclesPerVolley];

		for (int i = 0; i < iciclesPerVolley; i++) {
			icicleStartAngles [i] = PI / (iciclesPerVolley / 2) * i;
		}

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time > lastIcicleVolley + .2f) 
		{
			for (int i = 0; i < iciclesPerVolley; i++) {
				GameObject icicleInst = Instantiate (this.icicle);
				icicleInst.SendMessage ("SetDamage", this.icicleDamage);
				float r = 1f;

				// x = r * cos (startAngle + thisRotationAngle)
				float x = r * Mathf.Cos(icicleStartAngles[i] + this.transform.rotation.z);
				// y = r * sin (startAngle + thisRotationAngle)
				float y = r * Mathf.Sin(icicleStartAngles[i] + this.transform.rotation.z);

				icicleInst.transform.position = new Vector3 (x, y, 0) + this.transform.position;

				// Set icicle velocity.
				int indexForIcicleDirection = i - 1;
				if (indexForIcicleDirection < 0) {
					indexForIcicleDirection = 7;
				}
					
				float shiftedX = x * Mathf.Cos (PI / 4) + y * -Mathf.Sin (PI / 4);
				float shiftedY = x * Mathf.Sin (PI / 4) + y * Mathf.Cos (PI / 4);

				Rigidbody2D icicleInstRb = icicleInst.GetComponent<Rigidbody2D> ();
				Vector2 velocityVector = new Vector2 (shiftedX, shiftedY);
				velocityVector = velocityVector / velocityVector.magnitude;
				icicleInstRb.velocity = velocityVector * this.icicleVelocity + this.GetComponent<Rigidbody2D>().velocity;

				// Set Icicle rotation
				Quaternion newRotation = icicleInst.transform.rotation;
				if (icicleInstRb.velocity.x > 0) {
					newRotation.z = Mathf.Rad2Deg * Mathf.Atan (icicleInstRb.velocity.y / icicleInstRb.velocity.x);
				} else {
					newRotation.z = Mathf.Rad2Deg * Mathf.Atan (icicleInstRb.velocity.y / icicleInstRb.velocity.x) + 180f;
				}

				icicleInst.transform.eulerAngles = new Vector3 (newRotation.x, newRotation.y, newRotation.z);
			}
			this.lastIcicleVolley = Time.time;
		}
	}
}
