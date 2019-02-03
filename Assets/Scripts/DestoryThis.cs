using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryThis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void DestroyThisObject(){
        if (this.transform.parent != null)
        {
            Destroy(this.transform.parent.gameObject);
        } else
        {
            Destroy(this.transform.gameObject);
        }

	}

    void DestroyThisWithoutDestroyParent()
    {
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
