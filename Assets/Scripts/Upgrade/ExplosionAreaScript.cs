using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ExplosionAreaScript : MonoBehaviour
{
	public int dmg;
	public bool dot;
	public float timer = .2f;
	private float timer_og;
	void Start()
	{
		timer_og = timer;
	}
	
	private void Update()
	{
		// timer: when time, destroy self
		if (timer > 0)
		{
			timer-=Time.deltaTime;
		}
		else
		{
			//Destroy(gameObject);
			timer = timer_og;
			CollisionMakerScript.me.explosionCollider_pool.Release(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!dot)
		{
			if (other.CompareTag("Enemy"))
			{
				other.GetComponent<EnemyScript>().GetHit(dmg);
			}
		}
		else if (dot)
		{
			if (other.CompareTag("Enemy"))
			{
				// tell the enemy to apply dot to itself
				EnemyScript es = other.GetComponent<EnemyScript>();
				if (es.myEnemyType != EnemyScript.EnemyType.score)
				{
					es.StartPoison();
				}
			}
		}
	}
}
