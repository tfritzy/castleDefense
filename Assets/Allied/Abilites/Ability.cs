using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	protected float cooldown;
	protected float lastCastTime;
	protected bool isGoing;
	public GameObject projectile;
	protected List<GameObject> projectiles;
	public Vector2 projectileStartLocation;
	protected Vector2 projectileEndLocation;
	protected GameObject ownerBody;
	protected Companion owner;

	public abstract int GetDamage ();
	public abstract void GetInput();
	public abstract bool IsInputSatisfied ();
	private RectTransform reloadBar;


	// Use this for initialization
	void Start () {
		lastCastTime = Time.time;
		this.reloadBar = transform.Find ("ReloadBar").GetComponent<RectTransform> ();
		this.reloadBar.SendMessage ("SetReloadTime", cooldown);
		this.reloadBar.SendMessage ("ReloadByY");
		this.reloadBar.SendMessage ("StartRelaod");
		cooldown = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isGoing) {
			if (IsInputSatisfied ()) {
				Fire ();
			} else {
				GetInput ();
			}
		}
	}
		
	public void StartAbility(){
		Debug.Log ("Ability Atempted Started");
		if (Time.time > lastCastTime + cooldown) {
			this.isGoing = true;
			Debug.Log ("Ability Started");
		}
	}

	public void Fire(){
		GameObject clone = Instantiate (projectile);
		FormatProjectile (clone);
		isGoing = false;
		lastCastTime = Time.time;
	}

	public virtual void FormatProjectile(GameObject projectile){
		projectile.transform.position = projectileStartLocation;
		projectile.SendMessage ("SetDamage", GetDamage ());
	}


}
