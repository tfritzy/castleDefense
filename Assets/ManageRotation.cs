using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRotation : MonoBehaviour {

    private Rigidbody2D rb;
    float lastRotCheck;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > lastRotCheck + .5f)
        {
            Quaternion newRotation = transform.rotation;
            newRotation.z = Mathf.Rad2Deg * Mathf.Atan(rb.velocity.y / rb.velocity.x);
            this.transform.eulerAngles = new Vector3(newRotation.x, newRotation.y, newRotation.z);
        }
    }
}
