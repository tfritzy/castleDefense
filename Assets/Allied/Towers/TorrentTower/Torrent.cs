using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torrent : MonoBehaviour {

    public GameObject target;
    public int damage;

    private float startTime;
    private float upVelocity = 3;
    private float downVelocity = 18;
    private Vector2 startPos;
    private bool downVelocitySet = false;
    private Vector2 endLoc;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        startPos = this.transform.position;
        endLoc = new Vector2(Random.Range(-2f, 2f), 3f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > startTime + 10f)
            Destroy(this.gameObject);
        if (target == null)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(
                Random.Range(-7f, 7f),
                -1 * downVelocity * .66f);
            downVelocitySet = true;
        }
       
        if (downVelocitySet)
            return;
		if (Time.time < startTime + 1f)
        {
            Vector2 newPos = Vector2.MoveTowards
                (this.transform.position, startPos + endLoc
                , upVelocity * Time.deltaTime);
            this.transform.position = newPos;
            upVelocity *= 1 - Time.deltaTime;
        } else
        {
            Vector2 velocity = target.transform.position - this.transform.position;
            velocity.x += target.GetComponent<Rigidbody2D>().velocity.x / 3;
            velocity = velocity / velocity.magnitude;
            velocity *= downVelocity;
            this.GetComponent<Rigidbody2D>().velocity = velocity;
            downVelocitySet = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, 1.5f);
            foreach (Collider2D hit in hits)
            {
                if (hit.tag == "Enemy")
                {
                    hit.SendMessage("TakeDamage", damage);
                }
            }
            Destroy(this.gameObject);
        }
        
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }


}
