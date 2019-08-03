using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Triangle : Enemy
{
	public float stopDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        health = 30;
		speed = 0.8f;
    }

    // Update is called once per frame
    public override void Update()
    {
		float distance = -1;
		if (target != null)
			distance = Vector3.Distance(transform.position, target.transform.position);
		if (distance != -1 && distance >= stopDistance) {
	        Vector3 diff = (target.transform.position - transform.position).normalized;
	        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			rb.MoveRotation(Quaternion.Euler(0f, 0f, rot_z - 90));
			rb.velocity = new Vector2(transform.up.x, transform.up.y) * speed;
		}
		else
			rb.velocity = Vector2.zero;
		base.Update();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "PlayerAttack" && startBlinking == false) {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			startBlinking = true;
            health -= 10;
		}
    }
}
