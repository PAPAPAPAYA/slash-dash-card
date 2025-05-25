using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
	public bool homing;
	public float speed;
	[HideInInspector]public float hp;
	public float rotateSpeed;
	public float homingAngleRange;
	private GameObject _target;
	public int dmg;
	public float summonSickness;
	private bool _summonSick = true;
	public float lifeSpan;
	private Rigidbody2D _rb;
	public Color color_summonSick;
	private Color color_og;

	private void Start()
	{
		color_og = GetComponent<SpriteRenderer>().color;
		DecideTarget();
		_rb = GetComponent<Rigidbody2D>();
		//rotateSpeed = AbilityManagerScript.me.bullet_rotateSpd;
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
		if (homing)
		{
			Homing();
		}
		_rb.velocity = transform.up * speed;
	}
	#region HOMING
	private void Homing()
	{
		if (_target != null)
		{
			if (Vector2.Angle(transform.up, _target.transform.position - transform.position) < homingAngleRange)
			{
				var dir = (Vector2)(_target.transform.position - transform.position).normalized;
				var rotateAmount = Vector3.Cross(dir, transform.up).z;
				_rb.angularVelocity = -rotateAmount * rotateSpeed;
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
		var dist = float.MaxValue;
		foreach (var enemy in EnemySpawnerScript.me.enemies)
		{
			var enemyPos = enemy.transform.position;
			if (Vector2.Distance((Vector2)enemyPos, (Vector2)transform.position) < dist &&
			      Vector2.Angle(transform.up, enemyPos - transform.position) < homingAngleRange)
			{
				dist = Vector2.Distance((Vector2)enemyPos, (Vector2)transform.position);
				_target = enemy;
			}
		}
	}
	#endregion
	private void SummonSickness()
	{
		if (summonSickness > 0)
		{
			_summonSick = true;
			summonSickness -= Time.deltaTime;
			GetComponent<SpriteRenderer>().color = color_summonSick;
		}
		else
		{
			_summonSick = false;
			GetComponent<SpriteRenderer>().color = color_og;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			if (!_summonSick)
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
