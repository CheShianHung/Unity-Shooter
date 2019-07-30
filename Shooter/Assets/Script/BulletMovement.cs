using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    public Vector3 angle;
    private float width;
    private float height;
    private float moveEdgeMargin = 1f;

    private void Start()
    {
        width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x + moveEdgeMargin;
        height = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y + moveEdgeMargin;
    }

    public void Update()
    {
        transform.position += angle * speed;
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
