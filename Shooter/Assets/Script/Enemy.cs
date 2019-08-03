using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Blinkable
{
	public Rigidbody2D rb;
	public GameObject target;
	public float health = 1.0f;
	public float speed = 0.03f;

	void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

    public override void Update()
    {
        if (health <= 0 && startBlinking == false)
            Destroy(gameObject);
		base.Update();
    }
}
