using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : Blinkable
{
    public GameObject bullet;
    public GameObject healthSprite;
    public float initialHealth = 3.0f;
    public float moveSpeed = 0.06f;
    public float rotateSpeed = 0.5f;
    public float shootSpeed = 0.5f;
    public float lastShootTime = 0.0f;
    public float moveEdgeMargin = 0.5f;

	private Rigidbody2D rb;
    private UltimateJoystick shootJoystick;
    private UltimateJoystick movementJoystick;
    private float width;
    private float height;
    private float z;
    private ArrayList healthObjects;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void Start()
	{
		blinkTime = 2;

        width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x - moveEdgeMargin;
        height = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y - moveEdgeMargin;
        z = transform.position.z;
        movementJoystick = UltimateJoystick.GetUltimateJoystick("Movement");
        shootJoystick = UltimateJoystick.GetUltimateJoystick("Shoot");
        healthObjects = new ArrayList();
        for (int i = 0; i < initialHealth; i++)
        {
            GameObject obj = Instantiate(healthSprite);
            healthObjects.Add(obj);
            obj.transform.position = new Vector3(obj.transform.position.x + i * 0.8f, obj.transform.position.y, -1);
        }
    }

	public override void Update()
    {
        Movement(movementJoystick);
        Shoot(shootJoystick);
		base.Update();
    }

    private void Movement(UltimateJoystick joystick) 
    {
		Vector2 movePosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis()) * moveSpeed;
        if (movePosition.x > width)
			movePosition.x = width;
		if (movePosition.x < -width)
			movePosition.x = -width;
		if (movePosition.y > height)
			movePosition.y = height;
		if (movePosition.y < -height)
			movePosition.y = -height;
		rb.MovePosition(movePosition);
    }

    private void Shoot(UltimateJoystick joystick)
    {
        float x = joystick.GetHorizontalAxis();
        float y = joystick.GetVerticalAxis();
        if (x != 0.0f || y != 0.0f)
        { 
            float angle = angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90;
			rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), moveSpeed * Time.time));

            lastShootTime += Time.deltaTime;
            if (lastShootTime > shootSpeed)
            {
                lastShootTime = 0;
                float tan = Mathf.Tan((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180f);
                if (transform.rotation.eulerAngles.z < 180)
                    bullet.GetComponent<BulletMovement>().angle = new Vector2(-1, -tan).normalized;
                else
                    bullet.GetComponent<BulletMovement>().angle = new Vector2(1, tan).normalized;
                bullet.transform.position = transform.Find("ShootPoint").transform.position;
                Instantiate(bullet);
            }
        }
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			startBlinking = true;
			GameObject obj = (GameObject)healthObjects[healthObjects.Count - 1];
			Destroy(obj);
			healthObjects.Remove(obj);
			if (healthObjects.Count == 0) {
				Destroy(gameObject);
				print("GameOver");
			}
		}
	}
}
