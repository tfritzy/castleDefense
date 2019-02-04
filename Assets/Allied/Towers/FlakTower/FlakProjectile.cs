using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakProjectile : MonoBehaviour {

    public GameObject target;
    public int damage;
    public GameObject smokeCloud;

    private float instTime;
    private float projectileSpeed = 14;
    private float endY;

    // Use this for initialization
    void Start () {
        instTime = Time.time;
        
    }

    private void Update()
    {
        if (this.transform.position.y >= endY)
        {
            GameObject inst = Instantiate(smokeCloud, this.transform.position, new Quaternion());
            Destroy(inst, .3f);
            inst.transform.localScale *= 3;
            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 3);
            foreach(Collider2D hit in hits)
            {
                if (hit.tag != "Enemy")
                    continue;
                hit.SendMessage("TakeDamage", damage);
            }
            Destroy(this.gameObject);
        }
    }


    public void SetTarget(GameObject target)
    {
        this.target = target;
        Vector2 velocity = target.transform.position - this.transform.position;
        velocity += target.GetComponent<Rigidbody2D>().velocity;
        velocity = velocity / velocity.magnitude;
        velocity *= projectileSpeed;
        this.GetComponent<Rigidbody2D>().velocity = velocity;
        endY = target.transform.position.y;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

}
