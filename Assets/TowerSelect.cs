using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour {

    public GameObject tower;

    public GameObject sellButton;
    public GameObject buyButton;
    public GameObject inventoryButton;
    public GameObject levelUpButton;

    private int buttonDistFromThis = 2;
    private List<GameObject> buttons;

	// Use this for initialization
	void Start () {
        buttons = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (buttons.Count > 0)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                Vector2 touchPos = Vector2.zero;
                if (Input.touchCount > 0)
                    touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                else
                    touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 diffVect = touchPos - (Vector2)this.transform.position;
                if (diffVect.magnitude > 4f)
                    CloseMenu();

            }
        }
	}

    public void OnClick()
    {
        if (buttons.Count == 0)
            OpenMenu();
        else
            CloseMenu();
    }

    private void OpenMenu()
    {

        buttons.Add(Instantiate(inventoryButton,
            this.transform.position + new Vector3(buttonDistFromThis, 0, 0),
            new Quaternion(), this.transform));
        buttons.Add(Instantiate(levelUpButton,
            this.transform.position + new Vector3(0, buttonDistFromThis, 0),
            new Quaternion(), this.transform));
        if (this.tower == null)
            buttons.Add(Instantiate(buyButton,
                this.transform.position + new Vector3(-buttonDistFromThis, 0, 0),
                new Quaternion(), this.transform));
        else
            buttons.Add(Instantiate(sellButton,
                this.transform.position + new Vector3(-buttonDistFromThis, 0, 0),
                new Quaternion(), this.transform));
    }

    private void CloseMenu()
    {
        while(buttons.Count > 0)
        {
            Destroy(buttons[buttons.Count - 1]);
            buttons.RemoveAt(buttons.Count - 1);
        }
    }
}
