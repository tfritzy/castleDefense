using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrowerProj : MonoBehaviour {

    public int damage;
    private HashSet<Collider2D> hits;
    private float startTime;

    private void Update()
    {
        if (Time.time > startTime + .75f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !hits.Contains(collision))
        {
            collision.SendMessage("TakeDamage", damage);
            hits.Add(collision);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
        startTime = Time.time;
        hits = new HashSet<Collider2D>();
    }
}
