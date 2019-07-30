using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Blinkable
{
    public float health = 1.0f;

    public override void Update()
    {
        if (health <= 0 && startBlinking == false)
            Destroy(gameObject);
		base.Update();
    }
}
