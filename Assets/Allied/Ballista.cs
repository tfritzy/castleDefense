using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour {

	public float cooldown;
	private float lastAttackTime;
	private Camera cam;

	public float arrowVelocity;
	public int damage;
	public int pierce;
	public float loadProgress;
	public float maxPower;
	private Vector3 lastTouch;

	private GameObject crank;
	private GameObject bow;
	public GameObject bolt;
	private GameObject boltInst;

	public int extraShotsNeeded;
	private float lastExtraShotTime;
	private Vector3 fireDirection;
    private int extraBoltDamage;

	private readonly Vector2 BOLT_START_LOCATION = new Vector2(6.5f, .2f);
    public bool isLevelGoing = false;
	// Use this for initialization
	void Start () {
		lastAttackTime = Time.time;
		cam = Camera.main;

        //minimum time between shots.
		cooldown = .50f;

		pierce = (int)(1 + GameControl.control.pierceLevel);

        // Starts out at 4 seconds to full power. 
        maxPower = GetReloadTime();

        this.bow = this.transform.Find("bow").gameObject;
        this.crank = bow.transform.Find ("crank").gameObject;
		
		this.boltInst = null;

	}

    public static float GetReloadTime()
    {
        return Mathf.Max(4f * (Mathf.Pow(.95f, (float)GameControl.control.ballistaCooldown)), .1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (isLevelGoing)
        {
            // fire an extra bolt if needed
            if (Time.time > lastExtraShotTime + .1f && extraShotsNeeded > 0)
            {
                GameObject extraBolt = Instantiate(this.bolt);
                Vector3 boltPos = this.transform.position;
                boltPos.z = 0;
                extraBolt.transform.position = boltPos;
                extraBolt.GetComponent<Rigidbody2D>().velocity = fireDirection;
                extraBolt.SendMessage("SetDamage", this.extraBoltDamage);
                extraBolt.SendMessage("SetPierce", pierce);
                //change the parent of the bolt so that it doens't keep rotating with the bow. 
                extraBolt.transform.parent = GameObject.Find("base").transform;
                extraBolt.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                extraBolt.GetComponent<Rigidbody2D>().gravityScale = 1f;
                lastExtraShotTime = Time.time;
                extraShotsNeeded -= 1;
            }

            // load progress on bolt
            
            
            loadProgress = Mathf.Min((Time.time - lastAttackTime) / maxPower, 1.0f);
            if (this.boltInst == null)
            {
                this.boltInst = Instantiate(bolt, bow.transform);
                this.boltInst.transform.position = this.transform.position;
                this.boltInst.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }

            this.boltInst.transform.localPosition = new Vector2(6.5f + -1f * loadProgress * 4f, .2f);

            //rotate the crank
            if (loadProgress != 1.0f) { 
                Vector3 crankRotation = crank.transform.rotation.eulerAngles;
                crankRotation.z += 5f;
                crank.transform.eulerAngles = new Vector3(crankRotation.x, crankRotation.y, crankRotation.z);
            }
            Vector3 touchLocation = Vector3.zero;
            if (Input.touchCount > 0)
            {
                touchLocation = cam.ScreenToWorldPoint((Vector3)Input.GetTouch(0).position);
            }
            if (Input.GetMouseButton(0))
            {
                touchLocation = cam.ScreenToWorldPoint((Vector3)Input.mousePosition);
            }
            lastTouch = touchLocation;

             
            if ((Input.GetMouseButton(0) || Input.touchCount > 0) )
            {
                Vector2 touchDiff = (Vector2)lastTouch - (Vector2)bow.transform.position;
                Quaternion newRotation = transform.rotation;
                newRotation.z = Mathf.Rad2Deg * Mathf.Atan(touchDiff.y / touchDiff.x);
                bow.transform.eulerAngles = new Vector3(newRotation.x, newRotation.y, newRotation.z);
                this.boltInst.transform.eulerAngles = new Vector3(newRotation.x, newRotation.y, newRotation.z);

                // fire bolt!
                if (loadProgress > 0 && Time.time > lastAttackTime + .2f)
                {
                    if (lastTouch != Vector3.zero)
                    {
                        Vector2 fireDirection = (Vector2)(lastTouch - this.transform.position);
                        fireDirection = (fireDirection / fireDirection.magnitude) * arrowVelocity * (loadProgress + .4f) * 2;
                        
                        Vector3 boltPos = this.transform.position;
                        boltPos.z = 0;
                        boltInst.transform.position = boltPos;
                        boltInst.GetComponent<Rigidbody2D>().velocity = fireDirection;
                        this.extraBoltDamage = GetDamage();
                        boltInst.SendMessage("SetDamage", extraBoltDamage);
                        boltInst.SendMessage("SetPierce", pierce);
                        boltInst.GetComponent<Rigidbody2D>().gravityScale = 1f;
                        //change the parent of the bolt so that it doens't keep rotating with the bow. 
                        boltInst.transform.parent = GameObject.Find("base").transform;
                        boltInst.transform.localScale = new Vector3(1f, 1f, 1f);
                        extraShotsNeeded = GameControl.control.extraBallistaArrows;
                        lastExtraShotTime = Time.time;
                        this.fireDirection = fireDirection;
                        this.boltInst = null;
                    }
                }
                lastAttackTime = Time.time;
                loadProgress = 0f;
            }

        }
	}

    private int GetDamage()
    {
        return (int)Mathf.Max(1,  (GetMaxDamage() + GameControl.control.castleInhabitantBuff )* loadProgress) ;
    }

    public static int GetMaxDamage()
    {
        return (10 + GameControl.control.ballistaDamage);
    }

    public void SetIsLevelGoing(bool value)
    {
        this.isLevelGoing = value;

        // Load new values if the level is starting again.
        if (value == true)
        {
            Start();
        }
    }
}
