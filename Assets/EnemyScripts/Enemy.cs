using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour {

	//health variables
	public int health;
	public int maxhealth;
    public float gameDifficulty = .7f;

	//attack variables
	public float attackRange;
	private float lastAttack;
	public int attackDamage;
	public GameObject goldDrop;
	public GameObject target;
	private float lastTargetCheckTime;
	public GameObject rangedAttack;
	protected bool isRangedAttacker;
    public GameObject damageLabel;
    protected Vector3 rangedProjStartLocation = Vector3.zero;

	//fire variables
	private bool isOnFire;
	private int fireDPS;
	private float fireDuration;
	private float lastBurnTime;

	//poison variables
	private float lastPoisonHit;
	private float firstPoisonTime;
	private float poisonDuration;
	private int poisonDPS;
	public bool isPoisoned;

	//paralyze variables
	private float firstParalyzeTime;
	private float paralyzeDuration;
	public bool isParalyzed;

	//movement variables
	public float movementSpeed = -0.5f;
	public Vector2 startPos = new Vector2 ((float)10, (float)-4.456);
	public float slow;
	public Vector3 scale;
	private Animator animator;
	private Rigidbody2D rb;
	private bool stopped;
	private float baseMovementSpeed;
    public bool isFlyer = false;

    //death
    public GameObject dyingAnimation;
	float birthTime;
	public float goldMultiplier;
	public float soulGemDropChance;
	public GameObject soulGem;

	//enemy variables
	public GameObject castle;
    public string lastAttacker;

	public bool isGiant = false;
	public bool isDead = false;
	public float maxSpawnRate;
    private GameObject uiCanvas;

    public int prestigeCount;
    public Color startingColor;
    public float rightEdgeOfScreenPos;
    private int[] bleedDamageChunks;
    private float lastBleedDamageTime;

    // Ability Drops
    public GameObject arrowBarrageAbilityDrop;
    public GameObject arrowMinigunAbilityDrop;
    public GameObject caltropsAbilityDrop;
    public GameObject barrierAbilityDrop;
    public GameObject fireWaveAbilityDrop;
    public GameObject blizzardAbilityDrop;
    public GameObject fanOfBoltsAbilityDrop;
    public GameObject explosiveShotAbilityDrop;

    private void Awake()
    {
        this.animator = this.transform.Find("Global_CTRL").GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {

		rb = GetComponent<Rigidbody2D> ();
		//rb.position = startPos;
		GameObject manager = GameObject.Find ("Manager");
		maxhealth = GetHealth();
        maxhealth = (int)(maxhealth + GameControl.control.gameLevel * gameDifficulty);
		this.transform.localScale = this.scale;
        this.bleedDamageChunks = new int[6];
        for (int i = 0; i < bleedDamageChunks.Length; i++)
        {
            bleedDamageChunks[i] = 0;
        }

        // Determine how many prestiges have happened for enemies.
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        this.prestigeCount = GameControl.control.gameLevel / (levelManager.howManyLevelsBetweenWhenNewEnemiesStartAppearing * levelManager.enemies.Length);

        // Load the ability drops from resources
        this.fireWaveAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/FireWaveAbilityCollectable");
        this.blizzardAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/BlizzardAbilityCollectable");
        this.arrowBarrageAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/ArrowBarrageCollectable");
        this.arrowMinigunAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/MinigunCollectable");
        this.barrierAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/BarrierCollectable");
        this.caltropsAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/CaltropsAbilityCollectable");
        this.fanOfBoltsAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/FanOfBoltsCollectable");
        this.explosiveShotAbilityDrop = (GameObject)Resources.Load("Enemies/EnemyDrops/AbilityDrops/ExplosiveShotCollectable");


        // Find the right edge of the screen. This value is used to make it so that
        // Enemies don't take damage when they're off screen.
        this.rightEdgeOfScreenPos = Camera.main.ScreenToWorldPoint(
                                new Vector2(Camera.main.pixelWidth, 0)).x;

        List<int> attackValues = GetAttack();
        this.attackDamage = attackValues[0];
        SetValues();
        this.startingColor = new Color(1, 1, 1);

        // Apply prestige stuff
        if (prestigeCount > 0)
        {
            int color = prestigeCount * 51;
            float r = 1, g = 1, b = 1;
            if (prestigeCount == 1)
            {
                b = .1f;
            } else if(prestigeCount == 2) {
                g = .1f;
            } else {
                r = .1f;
            }

            SetColor(new Color(r, g, b));
            this.startingColor = new Color(r, g, b);

            this.maxhealth *= (4 * prestigeCount);
            this.attackDamage = (int)((float)this.attackDamage * (3 * prestigeCount));
        }

		if (isGiant) {
			this.soulGemDropChance *= 5;
            this.goldMultiplier *= 5;
			this.scale *= 1.5f;
			this.transform.localScale = this.scale;		
			maxhealth *= 1;
			health = maxhealth;
			attackDamage = (int)(attackDamage * 1.5f);
		}
        this.uiCanvas = GameObject.Find("UI");
		this.baseMovementSpeed = this.movementSpeed;
		castle = GameObject.Find ("Castle");
		lastAttack = Time.time;
		this.birthTime = Time.time;
		this.animator = this.transform.Find("Global_CTRL").GetComponent<Animator> ();
        health = maxhealth;
        

	}

	protected virtual void SetValues(){

	}

	public void SetIsGiant(){
		this.isGiant = true;
	}

    private float lastBoundsCheckTime;
    private void CheckBounds()
    {
        if (Time.time > lastBoundsCheckTime + 1f)
        {
            if (this.transform.position.x > 100 || this.transform.position.x < -100)
            {
                Destroy(this.gameObject);
            }
            if (this.transform.position.y > 100 || this.transform.position.y < -100)
            {
                Destroy(this.gameObject);
            }
            lastBoundsCheckTime = Time.time;
        }
    }

	protected void TakeDamage(int damage){
        if (damage == 0)
        {
            return;
        }

        // Enemies don't take damage when they're off screen.
        if (this.transform.position.x > rightEdgeOfScreenPos)
        {
            return;
        }

		health -= damage;

        GameControl.control.damageCount += 1;
        GameControl.control.damageDealtThisLevel += damage;
        GameControl.control.averageDamage = GameControl.control.damageDealtThisLevel / GameControl.control.damageCount;
        
		if (health < 0) {
			this.health = 0;
		}

		if (health <= 0 && !isDead) {
			isDead = true;
            this.stopped = true;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			float soulGemDrop = Random.Range (0f, 100f);
			if (soulGemDrop <= this.soulGemDropChance) {
				GameObject soulGem = Instantiate (this.soulGem);
				soulGem.transform.position = this.transform.position;
			}
            GameControl.control.totalDamageDone += this.maxhealth;
            GameControl.control.totalEnemiesKilled += 1;
            SpawnAbilities();
            updateHealthBar();
            SpawnItem();
            this.animator.SetBool ("isDead", true);
            // Set the animation speed back to the normal speed in case this enemy is frozen.
            this.transform.Find("Global_CTRL").GetComponent<Animator>().speed = 1f;
            this.stopped = true;
			deathAnimation ();
			RewardGold ();
            this.tag = "DeadEnemy";
            this.GetComponent<MonoBehaviour>().enabled = false;
            Destroy(this.GetComponent<Collider2D>());
        }
		updateHealthBar ();
        if (damageLabel != null && uiCanvas != null)
        {
            GameObject dLabel = Instantiate(this.damageLabel, uiCanvas.transform);
            dLabel.GetComponent<Text>().text = damage.ToString();
            float size = Mathf.Min(4, (float)damage / (float)GameControl.control.averageDamage);
            dLabel.transform.localScale = new Vector3(size, size, 1);
            dLabel.transform.position = this.transform.position;
        }
    }

    private void SpawnAbilities()
    {
        int spawnAbility = Random.Range(0, 1000);
        Vector3 instantationPosition = this.transform.position;
        instantationPosition.z = -15;
        // Spawn a common ability
        if (spawnAbility <= 10)
        {
            if (this.lastAttacker == "Archer")
            {
                GameObject ability = Instantiate(this.arrowBarrageAbilityDrop);
                ability.transform.position = instantationPosition;
            } else if (this.lastAttacker == "Mage")
            {
                GameObject ability = Instantiate(this.fireWaveAbilityDrop);
                ability.transform.position = instantationPosition;
            } else if (this.lastAttacker == "Ballista")
            {
                GameObject ability = Instantiate(this.explosiveShotAbilityDrop);
                ability.transform.position = instantationPosition;
            } else
            {
                Debug.LogError("No Last Attacker");
            }
        }

        if (spawnAbility == 20)
        {
            if (this.lastAttacker == "Archer")
            {
                GameObject ability = Instantiate(this.arrowMinigunAbilityDrop);
                ability.transform.position = instantationPosition;
            }
            else if (this.lastAttacker == "Mage")
            {
                GameObject ability = Instantiate(this.blizzardAbilityDrop);
                ability.transform.position = instantationPosition;
            }
            else if (this.lastAttacker == "Ballista")
            {
                GameObject ability = Instantiate(this.fanOfBoltsAbilityDrop);
                ability.transform.position = instantationPosition;
            }
            else
            {
                Debug.LogError("No Last Attacker");
            }
        }

        int castleAbility = Random.Range(0, 3000);
        if (castleAbility <= 3)
        {
            Instantiate(this.barrierAbilityDrop).transform.position = this.transform.position;
        }
        if (castleAbility == 10)
        {
            Instantiate(this.caltropsAbilityDrop).transform.position = this.transform.position;
        }


    }

	public void AddFireDuration(float length){
		this.fireDuration += length;
	}

	public void SetFireDPS(int dps){
		this.fireDPS = dps;
	}

	public void SetOnFire(){
		this.isOnFire = true;
	}

    /*
     * Sets the last attacker string which is used on death to determine which ability to drop.
     * 
     */
    public void SetLastAttacker(string attackerName)
    {
        this.lastAttacker = attackerName;
    }

	public void Burn(){
		if (Time.time > lastBurnTime + 1.0f && isOnFire) {
			TakeDamage (fireDPS);
			Debug.Log ("Burned for " + fireDPS);
			this.fireDuration -= 1f;
			if (fireDuration <= 0f) {
				fireDuration = 0;
				fireDPS = 0;
				isOnFire = false;
			}
			lastBurnTime = Time.time;
		}
	}


	public void AddSlow(float slow){

        // Enemies can't be effected when they're off screen.
        if (this.transform.position.x > rightEdgeOfScreenPos)
        {
            return;
        }
		this.slow += slow / (this.health / 20);
        SetColor(new Color((1 - this.slow), (1-this.slow), 1));
	}

    private void SetColor(Color color)
    {
        SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }

	private float lastSlowClearTime;
	private void ClearSlow(){
		if (Time.time > lastSlowClearTime + 10f){
			this.slow = 0;
            SetColor(this.startingColor);
			lastSlowClearTime = Time.time;
		}
	}
	public abstract List<int> GetAttack();
	public abstract int GetHealth ();


	void RewardGold(){
		GameObject gold = Instantiate (goldDrop);
		gold.transform.position = this.transform.position;
        float fullDropChance = (float)this.goldMultiplier * (this.prestigeCount + 1);
        if (Random.Range(0,3) == 2)
        {
            fullDropChance = 0;

        }
        if (Random.Range(0,100) == 1)
        {
            fullDropChance *= 10;
        }
        if (Random.Range(0,10000) == 1)
        {
            fullDropChance *= 100;
        }
        fullDropChance = Random.Range(fullDropChance * .7f, fullDropChance * 1.3f) * (1f + GameControl.control.extraGoldPercentage / 100f);
        int garunteedGold = (int)fullDropChance;

        float chanceForExtraGold = fullDropChance % 1;
        if (Random.Range(0f, 1f) < chanceForExtraGold)
        {

            garunteedGold += 1;
        }
		gold.SendMessage ("SetValue", garunteedGold);

	}

	void updateHealthBar(){
		Transform healthbar = this.gameObject.transform.Find ("Healthbar");
		Transform greenbar = healthbar.Find ("Greenbar");
		Image green = greenbar.GetComponent<Image> ();
		Vector3 newScale = green.rectTransform.localScale;
		if (maxhealth != 0) {
			newScale.x = (float)((float)health / (float)maxhealth);
			green.rectTransform.localScale = newScale;
		}
	}

	void deathAnimation(){

	}

	void move(){
        if (isDead)
        {
            // Set the animation speed back to the normal speed in case this enemy is frozen.
            this.animator.speed = 1f;
        }else if (isParalyzed || stopped)
        {
            this.rb.velocity = Vector2.zero;

            // Set the animation speed to the slow amount
            this.animator.speed = (1f - this.slow);

            
        } else
        {
            this.rb.velocity = new Vector2(Mathf.Min(movementSpeed * (1f - this.slow), 0), 0);
            // Set the animation speed to the slow amount
            this.animator.speed = (1f - this.slow);
        }
	}


	protected void TargetCheck(){
        if (this.transform.position.x > rightEdgeOfScreenPos)
            return;
        if (target == null || Time.time > lastTargetCheckTime + 1f) {

			GameObject[] targets = GameObject.FindGameObjectsWithTag ("Ally");
			if (targets.Length > 0) {
				float closestDistance = 1000000f;
				GameObject closestTarget = targets [0];
				foreach (GameObject target in targets) {
                    
					float distance = this.transform.position.x - target.transform.position.x;
					if (distance >= 0 && distance < closestDistance) {
						closestTarget = target;
						closestDistance = distance;
					}
				}
				target = closestTarget;
				lastTargetCheckTime = Time.time;
			}
		}
	}

	protected virtual void attack (){
		if (target == null) {
			return;
		}
        float distance = target.GetComponent<Collider2D>().Distance(this.GetComponent<Collider2D>()).distance;
        if (distance <= attackRange) {
			animator.SetBool ("isAttacking", true);
			this.stopped = true;
			this.movementSpeed = 0f;

		} else {
			animator.SetBool ("isAttacking", false);
			this.stopped = false;
			this.movementSpeed = this.baseMovementSpeed;

		}
	}

	public void Attack(){
        if (target == null)
            return;
		if (isRangedAttacker) {
			GameObject rangedProj = Instantiate (this.rangedAttack);
			Vector2 travelDirection = target.transform.position - this.transform.position;
            Debug.Log("The problem vector: " + travelDirection);
            if (travelDirection.magnitude > .1f)
            {
                Vector2 unitVectorTravelDirection = travelDirection / travelDirection.magnitude;
                rangedProj.GetComponent<Rigidbody2D>().velocity = unitVectorTravelDirection * 10;
                rangedProj.SendMessage("SetDirection", unitVectorTravelDirection * 20);
            }
            rangedProj.transform.position = rangedProjStartLocation + this.transform.position;
			rangedProj.SendMessage ("SetTarget", this.target);
			rangedProj.SendMessage ("SetDamage", this.attackDamage);
            Rigidbody2D rangedRb = rangedProj.GetComponent<Rigidbody2D>();


            Quaternion newRotation = rangedProj.transform.rotation;
            if (travelDirection.x > 0)
            {
                newRotation.z = Mathf.Rad2Deg * Mathf.Atan(travelDirection.y / travelDirection.x);
            } 
            rangedProj.transform.eulerAngles = new Vector3(newRotation.x, newRotation.y, newRotation.z);



            lastAttack = Time.time;
		} else {
			target.SendMessage ("TakeDamage", attackDamage);
			lastAttack = Time.time;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
		}
	}

    private void SetBleed(int damage)
    {
        
        int sixth = Mathf.Max(damage / 6, 1);
        Debug.Log("Dealing bleed damage: " + sixth);
        for (int i = 0; i < 6; i++)
        {
            bleedDamageChunks[i] += sixth;
        }
    }

    private void DealBleedDamageIfTimeIsRight()
    {
        if (Time.time > lastBleedDamageTime + 1f && bleedDamageChunks[0] > 0)
        {
            Debug.Log("dealing bleed: " + bleedDamageChunks[0]);
            TakeDamage(bleedDamageChunks[0]);
            lastBleedDamageTime = Time.time;

            // Shift the bleed damage chunks.
            for (int i = 0; i < bleedDamageChunks.Length - 1; i++)
            {
                bleedDamageChunks[i] = bleedDamageChunks[i + 1];
            }
            bleedDamageChunks[bleedDamageChunks.Length - 1] = 0;
        }
    }

    private void SpawnItem()
    {
        GameObject.Find("ItemManager").GetComponent<RollItem>().SpawnItem(this.transform.position);
    }

	// Update is called once per frame
	void Update () {
		move ();
		TargetCheck ();
		attack ();
		ClearSlow ();
		Burn ();
        CheckBounds();
        DealBleedDamageIfTimeIsRight();


        if (Time.time > birthTime + 320.0f) {
			Destroy (this.gameObject);
		}

	}
}
