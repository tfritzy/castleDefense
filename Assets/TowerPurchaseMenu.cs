using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPurchaseMenu : MonoBehaviour {

    public Tower selectedTowerScript;
    public GameObject selectedTower;
    public GameObject towerDetailsPage;

    public int slot;

    private GameObject towerDetailsPageInst;
    
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Vector2.zero;
            if (Input.touchCount > 0)
                touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            else
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diffVect = touchPos - (Vector2)this.transform.position;
            if (diffVect.magnitude > 6f)
                Destroy(this.gameObject);

        }
    }
    public void CloseMenu()
    {
        Destroy(GameObject.Find(this.name));
    }

    public void SetArrowTower()
    {
        this.selectedTowerScript = new ArrowTower();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }

    public void SetBarracks()
    {
        this.selectedTowerScript = new Barracks();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }

    public void SetFireBoltTower()
    {
        this.selectedTowerScript = new FireBoltTower();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }

    public void SetFlakTower()
    {
        this.selectedTowerScript = new FlakTower();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }

    public void SetFlameTower()
    {
        this.selectedTowerScript = new FlameThrowerTower();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }

    public void SetPounderTower()
    {
        this.selectedTowerScript = new PounderTurret();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }

    public void SetTeslaTower()
    {
        this.selectedTowerScript = new TeslaTower();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }

    public void SetTorrentTower()
    {
        this.selectedTowerScript = new TorrentTower();
        selectedTowerScript.Initialization();
        FillDetailsPage();
    }


    void FillDetailsPage()
    {
        Vector2 offset = Vector2.zero;
        if (this.transform.position.x > 0)
            offset = new Vector2(-5f, 0);
        else
            offset = new Vector2(5f, 0);
        if (this.transform.position.y < 0)
        {
            offset = offset + new Vector2(0, 4);
        }
        if (towerDetailsPageInst != null)
            Destroy(towerDetailsPageInst);
        towerDetailsPageInst = Instantiate(towerDetailsPage,
                                            (Vector2)this.transform.position + offset,
                                            new Quaternion(),
                                            GameObject.Find("UI").transform);
        towerDetailsPageInst.transform.Find("TowerDPSValue").GetComponent<Text>().text =
            (selectedTowerScript.projectileDamage /
            selectedTowerScript.attackSpeed).ToString();
        towerDetailsPageInst.transform.Find("TowerRangeValue").GetComponent<Text>().text =
            (selectedTowerScript.range * 10f + "m").ToString();
        towerDetailsPageInst.transform.Find("TowerCostValue").GetComponent<Text>().text =
            (selectedTowerScript.GetCost()).ToString();
        towerDetailsPageInst.transform.Find("TowerDescription").GetComponent<Text>().text =
            (selectedTowerScript.towerDescription).ToString();
        towerDetailsPageInst.transform.Find("TowerName").GetComponent<Text>().text =
            (selectedTowerScript.towerName).ToString();
        towerDetailsPageInst.SendMessage("SetSlot", slot);
        towerDetailsPageInst.SendMessage("SetSelectedTower", selectedTowerScript);
    }

    public void SetSlot(int slot)
    {
        this.slot = slot;
    }

}
