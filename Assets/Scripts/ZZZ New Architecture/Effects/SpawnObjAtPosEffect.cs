using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjAtPosEffect : MonoBehaviour
{
	public GameObject gameObjectToSpawn;
	public int amountToSpawn;
	public float chanceToScore;
	
	public void SpawnObject(GameObject enemy)
	{
		if (!enemy.GetComponent<EnemyScript>()) return;
		for (var i = 0; i < amountToSpawn; i++)
		{
			enemy.GetComponent<EnemyScript>().ShootOutCorpse(chanceToScore);
		}
	}
}
