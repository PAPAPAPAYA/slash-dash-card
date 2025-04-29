using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
	public List<GameObject> enemyPrefabs;
	public float spawnRadius;
	
	public List<GameObject> enemies;
	[Header("SPAWN AMOUNTs")]
	public int spawnAmount;
	public float spawnAmount_increaseInterval;
	private float spawnAmount_increaseTimer;
	[Header("SPAWN SPEEDs")]
	public float spawnInterval_start;
	public float spawnInterval_decreaseRate;
	private float spawnTimer;
	[Header("SPAWN HPs")]
	public int spawnHP;
	public float spawnHP_increaseInterval;
	private float spawnHP_increaseTimer;
	#region SINGLETON
	public static EnemySpawnerScript me;
	private void Awake()
	{
		me = this;
	}
	#endregion
	void Start()
	{
		spawnAmount_increaseTimer = spawnAmount_increaseInterval;
		spawnHP_increaseTimer = spawnHP_increaseInterval;
	}
	
	private void Update()
	{
		if (spawnTimer > 0) // spawn cd
		{
			spawnTimer -= Time.deltaTime;
		}
		else
		{
			spawnTimer = spawnInterval_start;
			for (int i = 0; i < spawnAmount; i++) // when CDed, spawn one enemy
			{
				SpawnOne();
			}
		}
		DecreaseSpawnInterval();
		IncreaseSpawnAmount();
		IncreaseSpawnHP();
	}
	private void SpawnOne()
	{
		GameObject enemySpawned = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
		Vector3 spawnPos = RandomPointOnUnitCircle(spawnRadius);
		spawnPos += PlayerControlScript.me.transform.position;
		enemySpawned.transform.position = spawnPos;
		enemySpawned.GetComponent<EnemyScript>().hp = spawnHP;
		enemies.Add(enemySpawned);
	}
	public static Vector3 RandomPointOnUnitCircle(float radius)
	{
		float angle = Random.Range(0f, Mathf.PI * 2);
		float x = Mathf.Sin(angle) * radius;
		float y = Mathf.Cos(angle) * radius;

		return new Vector3(x, y, 0);
	}
	private void DecreaseSpawnInterval()
	{
		if (spawnInterval_start > 1)
		{
			spawnInterval_start -= spawnInterval_decreaseRate * Time.deltaTime;
		}
	}
	private void IncreaseSpawnAmount()
	{
		if (spawnAmount_increaseTimer > 0)
		{
			spawnAmount_increaseTimer -= Time.deltaTime;
		}
		else
		{
			spawnAmount_increaseTimer = spawnAmount_increaseInterval;
			spawnAmount++;
		}
	}
	private void IncreaseSpawnHP()
	{
		if(spawnHP_increaseTimer>0)
		{
			spawnHP_increaseTimer -= Time.deltaTime;
		}
		else
		{
			spawnHP++;
			spawnHP_increaseTimer = spawnHP_increaseInterval;
		}

	}
}
