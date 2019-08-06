using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallWave_Triangle : SmallWave
{
	public GameObject enemy;
	public GameObject shooter;

	private int totalEnemy = 30;
	private int currentEnemyCount = 0;
	private float minTime = 0.5f;
	private float maxTime = 1.5f;
	private float intervalTime;
	private float timer = 0.0f;
	private bool allEnemySpawn = false;

    void Start()
    {
		intervalTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
		if (!allEnemySpawn) {
			timer += Time.deltaTime;
			if (timer >= intervalTime) {
				currentEnemyCount++;
				intervalTime = Random.Range(minTime, maxTime);
				timer = 0f;
				SpawnEnemy();
				if (currentEnemyCount >= totalEnemy)
					allEnemySpawn = true;
			}
				
		}
    }

	void SpawnEnemy()
	{
		float angle = Random.Range(0, 180);
		float rad = angle * Mathf.Deg2Rad;
		float tan = Mathf.Tan(rad);
		Vector2 direction = new Vector2(1 / tan, 1);

		RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector3(0, 0, 0), direction, 15);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider.gameObject.tag == "Respawn") {
				enemy.transform.position = new Vector3(hit.point.x, hit.point.y, -1);
				enemy.GetComponent<Enemy_Triangle>().target = shooter;
				Instantiate(enemy);
			}
		}
	}
}
