using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	public Rigidbody2D rb;
	public Vector2 angle;
    public float speed;

    private float width;
    private float height;
    private float moveEdgeMargin = 1f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
    private void Start()
    {
        width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x + moveEdgeMargin;
        height = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y + moveEdgeMargin;
    }

    public void Update()
    {
		rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + angle * speed);
        if (transform.position.x > width || transform.position.x < -width ||
            transform.position.y > height || transform.position.y < -height)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
            Destroy(gameObject);
    }
}
