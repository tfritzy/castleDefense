using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBolt : MonoBehaviour {

	float birthTime;
	public int damage;
	public GameObject target;
	private Rigidbody2D rb;
	public float velocity;
    public GameObject explosion;

	void Start(){
		rb = GetComponent<Rigidbody2D> ();
		birthTime = Time.time;
		GameObject[] allies = GameObject.FindGameObjectsWithTag ("Ally");
		for (int i = 0; i < allies.Length; i++) {
			Physics2D.IgnoreCollision (allies[i].GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());

        }
		
		if (target != null) {
			Vector2 difference = target.transform.position - this.transform.position;
			difference = difference / difference.magnitude;
			if (rb != null) {
				this.rb.velocity = difference * velocity;
			} 
		} 
	}

	// Update is called once per frame
	void Update () {

        if (rb.velocity.magnitude > .5f)
        {
            Quaternion newRotation = transform.rotation;
            newRotation.z = Mathf.Rad2Deg * Mathf.Atan(rb.velocity.y / rb.velocity.x);
            this.transform.eulerAngles = new Vector3(newRotation.x, newRotation.y, newRotation.z);
        }


        if (Time.time > (birthTime + 8.0f)) {
			Destroy (this.gameObject);
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Projectile")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Ally")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Enemy" && damage > 0)
        {
            collision.gameObject.SendMessage("SetLastAttacker", "Mage");
            collision.gameObject.SendMessage("TakeDamage", damage);
            collision.gameObject.SendMessage("AddSlow", GameControl.control.mageSlow / 100f);
            

            float explosionRadius = GameControl.control.mageSplashRadius * .2f;
            GameObject expInst = Instantiate(this.explosion);
            expInst.transform.localScale *= explosionRadius;
            Vector3 instPos = this.transform.position;
            instPos.z = 5;
            expInst.transform.position = instPos;

            
            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.tag == "Enemy" && !hit.Equals(collision))
                {
                    hit.gameObject.SendMessage("SetLastAttacker", "Mage");
                    hit.gameObject.SendMessage("TakeDamage", damage / 2);
                    hit.gameObject.SendMessage("AddSlow", GameControl.control.mageSlow / 100f / 5f);

                }
            }
            this.damage = 0;
            AudioManager.manager.Play("mageBasicAttackHit");
            Destroy(this.gameObject);
        }

        if (collision.gameObject.name == "Ground")
        {
            Destroy(this.gameObject);
            SetDamage(0);
        }
    }

	void SetDamage(int damage){
		this.damage = damage;
	}

}
