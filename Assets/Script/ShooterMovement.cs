using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : MonoBehaviour
{
    public GameObject bullet;
    public float moveSpeed = 0.05f;
    public float rotateSpeed = 0.2f;
    public float shootSpeed = 0.5f;
    public float lastShootTime = 0.0f;
    public float moveEdgeMargin = 0.5f;

    private UltimateJoystick shootJoystick;
    private UltimateJoystick movementJoystick;
    private float width;
    private float height;
    private float targetRotation;
    private float z;

	private void Start()
	{
        width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x - moveEdgeMargin;
        height = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y - moveEdgeMargin;
        targetRotation = 0.0f;
        z = transform.position.z;
        movementJoystick = UltimateJoystick.GetUltimateJoystick("Movement");
        shootJoystick = UltimateJoystick.GetUltimateJoystick("Shoot");
    }

	void Update()
    {
        Movement(movementJoystick);
        Shoot(shootJoystick);
    }

    private void Movement(UltimateJoystick joystick) 
    {
        Vector3 moveDirection = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);
        transform.position += moveDirection * moveSpeed;
        if (transform.position.x > width)
            transform.position = new Vector3(width, transform.position.y, z);
        if (transform.position.x < -width)
            transform.position = new Vector3(-width, transform.position.y, z);
        if (transform.position.y > height)
            transform.position = new Vector3(transform.position.x, height, z);
        if (transform.position.y < -height)
            transform.position = new Vector3(transform.position.x, -height, z);
    }

    private void Shoot(UltimateJoystick joystick)
    {
        float x = joystick.GetHorizontalAxis();
        float y = joystick.GetVerticalAxis();
        if (x != 0.0f || y != 0.0f)
        { 
            float angle = angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), moveSpeed * Time.time);

            lastShootTime += Time.deltaTime;
            if (lastShootTime > shootSpeed)
            {
                lastShootTime = 0;
                float tan = Mathf.Tan((transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180f);
                if (transform.rotation.eulerAngles.z < 180)
                    bullet.GetComponent<BulletMovement>().angle = new Vector3(-1, -tan, 0).normalized;
                else
                    bullet.GetComponent<BulletMovement>().angle = new Vector3(1, tan, 0).normalized;
                bullet.transform.position = transform.Find("ShootPoint").transform.position;
                Instantiate(bullet);
            }
        }
    }
}
