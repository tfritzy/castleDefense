using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTowerMenu : MonoBehaviour {

    public GameObject towerMenu;
    private GameObject towerMenuInst;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenMenu()
    {
        if (towerMenuInst == null) {
            towerMenuInst = Instantiate(
                towerMenu,
                this.transform.position, 
                new Quaternion(), 
                GameObject.Find("UI").transform);
        }  
    }
}
