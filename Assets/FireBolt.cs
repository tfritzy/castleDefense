using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : MonoBehaviour {

    private int damage;
    private float explosionRadius;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
            
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius);
            foreach(Collider2D hit in hits)
            {
                if (hit.tag == "Enemy")
                {
                    hit.SendMessage("TakeDamage", damage);
                }
            }
            GameObject inst = Instantiate(explosion, this.transform.position, new Quaternion(), null);
            inst.transform.localScale *= 3;
            Destroy(inst, .4f);
            Destroy(this.gameObject);
        }
    }

    public void SetRadius(float radius)
    {
        this.explosionRadius = radius;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
