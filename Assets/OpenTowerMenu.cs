using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTowerMenu : MonoBehaviour {

    public GameObject towerMenu;
    private GameObject towerMenuInst;

    public int slot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }

    public void OpenMenu()
    {
        Vector2 offset = Vector2.zero;
        if (this.transform.position.y < -1f)
        {
            offset += new Vector2(0, 2);
        }
        if (towerMenuInst == null) {
            towerMenuInst = Instantiate(
                towerMenu,
                this.transform.position + (Vector3)offset, 
                new Quaternion(), 
                GameObject.Find("UI").transform);
            towerMenuInst.SendMessage("SetSlot", slot);
        }  
    }
}
