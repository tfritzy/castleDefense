using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using UnityEngine.UI;

/*

 * When switch back to level playing:
 * 	1. Abilities needs to go back to working.
 *  2. all upgrade ui needs to go away. 
 *  3. enemies need to spawn again. 
 * 
 * */

public class LevelManager: MonoBehaviour {

	public int level;
	public float enemyHealthFactor;

	public GameObject upgradeUI;

	// timing
	public float enemyTimeSpawn = .5f;
	private float levelTimeLength = 120f;
	private float levelEndTime;
	private float levelStartTime;
	private float lastEnemyUpdateTime;
	public bool levelOver = false;

	public bool isLevelGoing;

	public GameObject archer;
	private GameObject canvas;
	public Text goldCountText;

	// Enemies
	public GameObject farmer;
	public GameObject catapult;
	public GameObject soldier;
	public GameObject knight;
	public GameObject demon;
	public GameObject suicideBomber;
	public GameObject witch;
	public int howManyLevelsBetweenWhenNewEnemiesStartAppearing = 5;
	public GameObject[] enemies;

	public List<float> timeRemainingUntilNextSpawnForEachEnemy;
	public float[] baseSpawnRatesForEachEnemy;

	private bool isMovingUI;
	private bool isMovingUIUp;

	private static LevelManager manager;
	public GameObject allyMage;
    public GameObject ballista;
	private float lastMoveTime;
    private int numberOfUniquePacksSpawned;
    private int numberOfUniquePacksToSpwan;
    private float timeBetweenUniquePacks;
    private float lastUniqueSpawnTime;
    public GameObject newEnemyLabel;

	private GameObject upgradeCanvas;
	private float groundLevel;

    private float packOneSpawnTime;
    private float packTwoSpawnTime;
    public GameObject youDied;
    public bool isDead;
    private float deathTime;

    private Animator lightSourceAnimator;

	void Start(){
		if (manager == null) {
			manager = this;
			this.isLevelGoing = false;
            SetLevelGoingForBallistas(false);

		} else {
			Destroy (this.gameObject);
		}
	}


	void LevelSetup(){

		enemies = new GameObject[]{ farmer, soldier, demon, suicideBomber, catapult, knight, witch};
		GameControl.control.load ();
		this.level = GameControl.control.gameLevel;
		this.levelEndTime = Time.time + this.levelTimeLength;
		this.levelStartTime = Time.time;
		this.upgradeCanvas = GameObject.Find ("UpgradeCanvas");
        GameObject ground = GameObject.Find("Ground");

        if (GameControl.control.autoplay)
        {
            Time.timeScale = 10;
        }
        

        // Reset the average damage.
        GameControl.control.averageDamage = 0;
        GameControl.control.damageCount = 0;
        GameControl.control.damageDealtThisLevel = 0;

        this.groundLevel = ground.transform.position.y + ground.GetComponent<BoxCollider2D>().size.y;
        //Choose which enemies spawn
        this.numberOfUniquePacksToSpwan = Mathf.Min(GameControl.control.gameLevel / 4, 3)+1;
        this.timeBetweenUniquePacks = this.levelTimeLength / this.numberOfUniquePacksToSpwan + 1;
        this.lastUniqueSpawnTime = Time.time;
        Debug.Log("There are going to be this many eliete packs this level: " + this.numberOfUniquePacksToSpwan);
		//Spawn rates are how many seconds between each spawn there should be. 
		this.baseSpawnRatesForEachEnemy = GetSpawnRates(this.level);
		timeRemainingUntilNextSpawnForEachEnemy = new List<float> ();


		this.isMovingUI = true;
		this.isMovingUIUp = false;

		for (int i = 0; i < enemies.Length; i++){
			float spawnTime = this.baseSpawnRatesForEachEnemy [i];

			if (spawnTime != 0f) {
				timeRemainingUntilNextSpawnForEachEnemy.Add (this.enemies[i].GetComponent<Enemy>().maxSpawnRate);
			} 
		}

        // One pack spawns at level 3 and the next at level 8
        this.packOneSpawnTime = GameControl.control.gameLevel >= 3 ? UnityEngine.Random.Range(Time.time, this.levelEndTime + Time.time) : 1000;
        this.packTwoSpawnTime = GameControl.control.gameLevel >= 8 ? UnityEngine.Random.Range(Time.time, this.levelEndTime + Time.time) : 1000;

        // if more archers are needed spawn them
        SpawnArchers();

        // if more mages are needed, spawn them
        SpawnMages();

        // if more ballistas are needed, spawn them
        SpawnBallistas();


        GameObject.Find ("LevelLabel").gameObject.GetComponent<Text> ().text = ("Level " + GameControl.control.gameLevel);

        if (GameControl.control.gameLevel % this.howManyLevelsBetweenWhenNewEnemiesStartAppearing == 0)
        {
            Instantiate(this.newEnemyLabel, GameObject.Find("UI").transform);
        }

        /* Calls start on the objects that are found by these calls. 
         * This is to help with me putting code in start methods and 
         * then deciding to have a continuous level.
         */
		SetupGameObjectsWithTag ("Ability");
		SetupGameObjectsWithTag ("Castle");
		SetupGameObjectsWithTag ("Ally");

        lightSourceAnimator = GameObject.Find("Sun Light Source").GetComponent<Animator>();
        lightSourceAnimator.speed = 90f / levelTimeLength;
	}

    private void SpawnArchers()
    {
        Vector3 archerSpawnLocation = GameObject.Find("ArcherSpawnLocation").transform.position;
        Archer[] archers = GameObject.FindObjectsOfType<Archer>();
        foreach (Archer archer in archers)
        {
            Destroy(archer.gameObject);
        }
        float archerDistance = 2f / ((float)GameControl.control.archerCount);
        for (int i = 0; i < GameControl.control.archerCount; i++)
        {
            GameObject newArcher = Instantiate(this.archer);
            Vector3 oldPos = archerSpawnLocation;
            oldPos.x -= 1f;
            oldPos.x += archerDistance * i;
            newArcher.transform.position = oldPos;
            newArcher.name = "Archer";
        }
    }

    private void SpawnMages()
    {
        Vector3 mageSpawnLocation = GameObject.Find("MageStartLocation").transform.position;
        Mage[] mages = GameObject.FindObjectsOfType<Mage>();
        foreach (Mage mage in mages)
        {
            Destroy(mage.gameObject);
        }
        float distanceBetweenMages = 2f / (float)GameControl.control.mageCount;
        for (int i = 0; i < GameControl.control.mageCount; i++)
        {
            GameObject newMage = Instantiate(this.allyMage);
            Vector3 oldPos = mageSpawnLocation;
            oldPos.x -= 1f;
            oldPos.x += distanceBetweenMages * i;
            newMage.transform.position = oldPos;
            newMage.name = "Mage";
        }
    }

    private void SpawnBallistas()
    {

        Transform castle = GameObject.Find("Castle").transform;
        float ballista1ZPos = GameObject.Find("Ballista1").transform.position.z;
        int currentBallistaCount = GameObject.FindObjectsOfType<Ballista>().Length;
        for (int i = currentBallistaCount; i <= GameControl.control.ballistaCount; i++)
        {
            GameObject newBallista = Instantiate(this.ballista, castle);
            Vector3 pos = GameObject.Find("BallistaPos" + i.ToString()).transform.position;
            pos.z = ballista1ZPos - .1f * i;
            newBallista.transform.position = pos;
            newBallista.name = "Ballista" + i.ToString();
        }
    }


	void SetupGameObjectsWithTag(String tag){

		GameObject[] abilities = GameObject.FindGameObjectsWithTag (tag);
		foreach (GameObject ability in abilities) {
			ability.SendMessage ("Start");
		}
	}

    private void SetLevelGoingForBallistas(bool value)
    {
        Ballista[] ballistas = GameObject.FindObjectsOfType<Ballista>();
        foreach (Ballista ballista in ballistas)
        {
            ballista.SetIsLevelGoing(value);
        }
    }



	void OnApplicationStop() {
		GameControl.control.save ();
	}

	void Go(){
		this.isLevelGoing = true;
		GameControl.control.load ();
		this.level = GameControl.control.gameLevel;
		LevelSetup ();
        SetLevelGoingForBallistas(true);
        lightSourceAnimator.SetBool("isLevelGoing", true);

    }
		

	// Update is called once per frame
	void Update () {
		
        if (isDead)
        {
            if ((Input.GetMouseButton(0) || Input.touchCount > 0) && Time.time > deathTime + 5f){
                this.isDead = false;
                GameControl.control.save();
                levelOver = true;
                this.isLevelGoing = false;
                SetLevelGoingForBallistas(false);
                Destroy(GameObject.Find("YouDied"));
                this.isMovingUI = true;
                this.isMovingUIUp = true;
                SetupUpgradeMenu();
            }
            return;
        }

		if (isMovingUI) {
			if (isMovingUIUp) {
				upgradeCanvas.GetComponent<Animator> ().SetTrigger ("GoUp");
				isMovingUI = false;
				this.isMovingUIUp = false;
			} else {
				upgradeCanvas.GetComponent<Animator> ().SetTrigger ("GoDown");
				isMovingUI = false;
			}
		} else { 
			if (isLevelGoing) {
				if (Time.time < this.levelEndTime) {
					if (Time.time > lastEnemyUpdateTime + 1.0f){

						float timeDif = Time.time - lastEnemyUpdateTime;
						for (int i = 0; i < timeRemainingUntilNextSpawnForEachEnemy.Count; i++) {
                            timeRemainingUntilNextSpawnForEachEnemy[i] -= timeDif;
							if (timeRemainingUntilNextSpawnForEachEnemy [i] <= 0) {
                                

                                // Choose the number of enemies to spawn.
                                int enemyRand = UnityEngine.Random.Range(0, 20);
                                
                                //Spawn packs of enemies if it's time to do so.
                                if (Time.time > this.packOneSpawnTime)
                                {
                                    SpwanPack();
                                    this.packOneSpawnTime = Time.time + 100000;
                                }
                                if (Time.time > this.packTwoSpawnTime)
                                {
                                    SpwanPack();
                                    this.packTwoSpawnTime = Time.time + 100000;
                                }
                                

                                
                                int enemyCount = 1;

                                // Decide whether this is a unique pack
                                bool isUniquePack = false;
                                if (this.numberOfUniquePacksSpawned < this.numberOfUniquePacksToSpwan && (Time.time> this.lastUniqueSpawnTime + this.timeBetweenUniquePacks))
                                {

                                    isUniquePack = true;
                                    this.numberOfUniquePacksSpawned += 1;
                                    this.lastUniqueSpawnTime = Time.time;
                                    // Uniques spawn in groups of 3. 
                                    enemyCount = 3;
                                }
                                for (int j = 0; j < enemyCount; j++){

                                    // spawn the enemy in a random locaiton if it's spawning alone.
                                    int locationModifier = j * 15;
                                    if (enemyCount == 1)
                                    {
                                        locationModifier = UnityEngine.Random.Range(0, 40);
                                    }

                                    GameObject newEnemy = Instantiate (enemies [i]);
                                    Enemy enemyScript = newEnemy.GetComponent<Enemy>();
                                    // If it's a unique pack make the enemy unique. 
                                    if (isUniquePack)
                                    {
                                        newEnemy.GetComponent<Enemy>().SetIsGiant();
                                        SpriteRenderer[] sprites = newEnemy.GetComponentsInChildren<SpriteRenderer>();

                                        foreach (SpriteRenderer sprite in sprites)
                                        {
                                            sprite.color = new Color(0, 1f, 1f);
                                            newEnemy.GetComponent<Enemy>().startingColor = new Color(0, 1f, 1f);
                                        }
                                    }
									newEnemy.GetComponent<Rigidbody2D> ().gravityScale = 0;
                                    float yLoc = groundLevel - 1f; 
                                    if (newEnemy.GetComponent<Enemy>().isFlyer)
                                    {
                                        yLoc = UnityEngine.Random.Range(groundLevel + 4f, 4f);
                                    }
									newEnemy.transform.position = new Vector3 (19f - .05f * locationModifier, yLoc + .03f * locationModifier, locationModifier * .1f);
								}
								timeRemainingUntilNextSpawnForEachEnemy [i] = UnityEngine.Random.Range(this.baseSpawnRatesForEachEnemy[i] / 1.25f, this.baseSpawnRatesForEachEnemy [i] * .5f);
							}
						}

						this.lastEnemyUpdateTime = Time.time;
					}
				} else {
					if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag ("Money").Length == 0) {
						Debug.Log ("Level OVER! With " + (levelEndTime - Time.time).ToString() + " seconds left");

                        Debug.Log("Wrote to stats file");
                        System.IO.File.AppendAllText("C:\\development\\stats.csv", GameControl.control.gameLevel + "," + GameControl.control.totalDamageDone + "," + GameControl.control.gold + "," + GameControl.control.totalEnemiesKilled + "\n");


                        // Restore castle to full health. 
                        GameObject.Find("Castle").SendMessage("Start");

                        GameControl.control.gameLevel += 1;
						GameControl.control.save ();
						levelOver = true;
						this.isLevelGoing = false;
                        SetLevelGoingForBallistas(false);
                        this.isMovingUI = true;
						this.isMovingUIUp = true;
						SetupUpgradeMenu ();

                        /*
                        if (GameControl.control.autoplay)
                        {
                            Go();
                        }
                        */

					}
				}
			} 
		}
		
	}

    private void SpwanPack()
    {
        Debug.Log("Spawn pack baby!");
        List<GameObject> possibleEnemies = new List<GameObject>();
        
        for( int i = 0; i < timeRemainingUntilNextSpawnForEachEnemy.Count; i++)
        {
            GameObject enemy = enemies[i];
            if (!enemy.GetComponent<Enemy>().isFlyer)
            {
                possibleEnemies.Add(enemy);
            }
        }
        int whichGroup = UnityEngine.Random.Range(0, 2);
        if (whichGroup == 1)
        {
            SpawnSquare(possibleEnemies[UnityEngine.Random.Range(0, possibleEnemies.Count)]);
        } else if (whichGroup == 2)
        {
            SpawnLine(possibleEnemies[UnityEngine.Random.Range(0, possibleEnemies.Count)]);
        } else
        {
            SpawnSpear(possibleEnemies[UnityEngine.Random.Range(0, possibleEnemies.Count)]);
        }
        
    }

    private void SpawnSquare(GameObject enemy)
    {
        Vector3 startLocation = new Vector3(19f, this.groundLevel, 0f);
        GameObject inst = Instantiate(enemy);
        inst.transform.position = startLocation;
        startLocation.x -= .5f;
        startLocation.y += .5f;
        startLocation.z -= 1;
        inst = Instantiate(enemy);
        inst.transform.position = startLocation;
        startLocation.x += 1f;
        inst = Instantiate(enemy);
        inst.transform.position = startLocation;
        startLocation.x += .5f;
        startLocation.y -= .5f;
        startLocation.z += 1;
        inst = Instantiate(enemy);
        inst.transform.position = startLocation;
    }

    private void SpawnLine(GameObject enemy)
    {
        Vector3 startLocation = new Vector3(19f, this.groundLevel, 0f);

        for (int i = 0; i < 5; i++)
        {
            GameObject inst = Instantiate(enemy);
            inst.transform.position = startLocation;
            startLocation.x += 1f;
            startLocation.y += .8f;
            startLocation.z -= .5f;
        }
    }

    private void SpawnSpear(GameObject enemy)
    {
        Vector3 startLocation = new Vector3(19f, this.groundLevel, 0f);
        GameObject inst = Instantiate(enemy);
        inst.transform.position = startLocation;
        startLocation.x += .5f;
        startLocation.y += .3f;
        startLocation.z += 1f;
        inst = Instantiate(enemy);
        inst.transform.position = startLocation;
        startLocation.y -= .6f;
        startLocation.z -= 2;
        inst = Instantiate(enemy);
        inst.transform.position = startLocation;
        startLocation.x += .5f;
        startLocation.y -= .3f;
        startLocation.z -= 1;
        inst = Instantiate(enemy);
        inst.transform.position = startLocation;
        startLocation.y += 1.2f;
        startLocation.z += 3;
        inst = Instantiate(enemy);
        inst.transform.position = startLocation;

    }

    public void EndLevel()
    {
        // break out of method if death logic has already happened.
        if (isDead)
        {
            return;
        }

        // Destroy the remaining enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Collect the money on the ground
        GameObject[] money = GameObject.FindGameObjectsWithTag("Money");
        foreach (GameObject coin in money)
        {
            coin.SendMessage("Reward");
        }

        lightSourceAnimator.SetBool("isLevelGoing", false);

        // Remove the barrier if it's present.
        Destroy(GameObject.Find("Barrier"));

        // Collect the money on the ground
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        foreach (GameObject enemyProj in enemyProjectiles)
        {
            Destroy(enemyProj);
        }
        GameControl.control.gameLevel -= 1;

        if (GameObject.Find("YouDied") == null)
        {
            GameObject deathLabel = Instantiate(this.youDied, GameObject.Find("UI").transform);
            deathLabel.name = "YouDied";
            Debug.Log("Making a death thingy");
        }

        // Restore castle to full health. 
        GameObject.Find("Castle").SendMessage("Start");
        SetLevelGoingForBallistas(false);
        this.levelEndTime = Time.time+4f;
        this.isDead = true;
        this.deathTime = Time.time;

        if (GameControl.control.autoplay)
        {
            GameObject.Find("Purchase").SendMessage("Nextlevel");
        }
    }



	void SetupUpgradeMenu(){

		GameObject.Find ("Purchase").SendMessage ("Start");
		this.isMovingUI = true;
		this.isMovingUIUp = true;


	}

	private float[] GetSpawnRates(int currentLevel){
        float[] getSpawnRates = new float[enemies.Length];
		int howManyEnemies = currentLevel / this.howManyLevelsBetweenWhenNewEnemiesStartAppearing;

        // Wrap around the spawn rates if the game has run out of new enemies to spawn. 
        while (currentLevel >= enemies.Length * this.howManyLevelsBetweenWhenNewEnemiesStartAppearing)
        {
            currentLevel -= enemies.Length * this.howManyLevelsBetweenWhenNewEnemiesStartAppearing;
        }

        // Loop through each enemy slot setting the base spawn rate.
        // The pattern of spawn rates is 0 ...0,10,9,8,7,6,5,10,11,12,13,14 ...15
        // And this loops every time the game runs out of new enemies to spawn. 
        // The enemies get their health tripled and get a recolor
		for (int i = 0; i < enemies.Length; i++) {
			int startAppearingLevel = i * this.howManyLevelsBetweenWhenNewEnemiesStartAppearing;
			int deltaLevel = Mathf.Abs (currentLevel - i * this.howManyLevelsBetweenWhenNewEnemiesStartAppearing);
			if (currentLevel >= startAppearingLevel) {
				if (currentLevel < startAppearingLevel + 5)
                {
                    getSpawnRates[i] = (10f - ((float)(currentLevel - startAppearingLevel)));
                } else
                {
                    getSpawnRates[i] = Mathf.Min(100, (currentLevel - startAppearingLevel)*4);
                }
				
			} else {
				getSpawnRates [i] = 0f;
			}
		}
		return getSpawnRates;
	}
		
		
}

