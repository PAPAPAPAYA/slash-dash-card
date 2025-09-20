using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScoreAtEnemyPosEffect : MonoBehaviour
{
	public int amountToSpawn;
	public float chanceToScore;
	public float chanceToSpawn;

	public void SpawnDummyScore(GameObject enemy)
	{
		if (!enemy.GetComponent<EnemyScript>()) return;
		for (var i = 0; i < amountToSpawn; i++)
		{
			if (Random.value < chanceToSpawn)
			{
				enemy.GetComponent<EnemyScript>().ShootOutCorpse(chanceToScore);
			}
		}
	}
	public void LoadSpawnDummyScoreToPoisonKill()
	{
		LingerEffectManager.me.onPoisonKill.RemoveListener(SpawnDummyScore);
		LingerEffectManager.me.onPoisonKill.AddListener(SpawnDummyScore);
	}
}
