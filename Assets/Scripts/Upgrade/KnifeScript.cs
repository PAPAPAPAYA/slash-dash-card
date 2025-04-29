using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
	public float speed;
	public float hp;
	public float rotateSpeed;
	public float homingAngleRange;
	private GameObject target;
	public int dmg;
	public float summonSickness;
	private bool summonSick = true;
	public float lifeSpan;
	private Rigidbody2D rb;
	public Color color_summonSick;
	private Color color_og;

	private void Start()
	{
		color_og = GetComponent<SpriteRenderer>().color;
		DecideTarget();
		rb = GetComponent<Rigidbody2D>();
		rotateSpeed = AbilityManagerScript.me.bullet_rotateSpd;
	}
	void Update()
	{
		//transform.position += speed * Time.deltaTime * transform.up;

		SummonSickness();

		if (lifeSpan > 0)
		{
			lifeSpan -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
		}
		
		if (hp<=0)
		{
			Destroy(gameObject);
		}
	}
	private void FixedUpdate()
	{
		if (AbilityManagerScript.me.lvl_homingBullet > 0)
		{
			Homing();
		}
		rb.velocity = transform.up * speed;
	}
	#region HOMING
	private void Homing()
	{
		if (target != null)
		{
			if (Vector2.Angle(transform.up, target.transform.position - transform.position) < homingAngleRange)
			{
				Vector2 dir = (Vector2)(target.transform.position - transform.position).normalized;
				float rotateAmount = Vector3.Cross(dir, transform.up).z;
				rb.angularVelocity = -rotateAmount * rotateSpeed;
			}
			else
			{
				DecideTarget();
			}
		}
		else
		{
			DecideTarget();
		}

	}
	private void DecideTarget()
	{
		float dist = float.MaxValue;
		foreach (GameObject enemy in EnemySpawnerScript.me.enemies)
		{
			Vector3 enemyPos = enemy.transform.position;
			if (Vector2.Distance((Vector2)enemyPos, (Vector2)transform.position) < dist &&
			      Vector2.Angle(transform.up, enemyPos - transform.position) < homingAngleRange)
			{
				dist = Vector2.Distance((Vector2)enemyPos, (Vector2)transform.position);
				target = enemy;
			}
		}
	}
	#endregion
	private void SummonSickness()
	{
		if (summonSickness > 0)
		{
			summonSick = true;
			summonSickness -= Time.deltaTime;
			GetComponent<SpriteRenderer>().color = color_summonSick;
		}
		else
		{
			summonSick = false;
			GetComponent<SpriteRenderer>().color = color_og;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			if (!summonSick)
			{
				if (AbilityManagerScript.me.lvl_onBulletHit_explosion > 0)
				{
					AbilityManagerScript.onKnifeHit?.Invoke(transform.position);
				}
				else
				{
					collision.GetComponent<EnemyScript>().GetHit(dmg);
				}
				hp--;
			}
		}
		else if (collision.CompareTag("Wall"))
		{
			hp--;
		}
	}
}
