using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrowerProj : MonoBehaviour {

    public int damage;
    private HashSet<Collider2D> hits;

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
    }
}
