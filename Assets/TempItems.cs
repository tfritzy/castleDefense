using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempItems : MonoBehaviour {

    public List<Item> tempItemStorage;

	// Use this for initialization
	void Start () {
        tempItemStorage = new List<Item>();
	}

    public void AddItemToTempStorage(Item item)
    {
        tempItemStorage.Add(item);
        foreach(Item i in tempItemStorage)
        {
            Debug.Log(i);
        }
    }

}
