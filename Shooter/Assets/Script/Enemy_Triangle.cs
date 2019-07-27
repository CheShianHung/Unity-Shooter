using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Triangle : Enemy
{
    public Transform target;
	public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        Vector3 diff = (target.position - transform.position).normalized;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

		base.Update();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.transform.tag == "PlayerAttack") {
			startBlinking = true;
		}
    }
}
