using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using TMPro;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
	public enum EnemyType
	{
		brawler,
		slimer,
		shooter,
		bullet,
		score,
		restart
	};
	[Header("BASICs")]
	public int hp;
	public float moveSpd;
	public float hurt_stunDuration;
	private bool _hurtStunned;
	public float spawn_iFrame;
	private float _spawnIFrameOg;
	public EnemyType myEnemyType;
	public bool shielded;
	public float rotSpd;
	public int dmg;
	public int poisonStack;
	#region DEPRECATED
	private float poison_timer; // timer used to time the dot
	private float poison_interval;
	private float poison_duration; // how long does the dot is active
	private int poison_dmg; // how much damage the dot does
	#endregion
	
	[Header("SLIMERs")]
	public List<GameObject> slimees;
	public float slimee_spawnForce;
	
	[Header("SHOOTERs")]
	public float shoot_interval;
	private float shoot_timer;
	public float shooter_stopDis;
	public GameObject bulletPrefab;
	
	[Header("SCOREs")] public float chanceToScore;
	
	[Header("SHADOWs")]
	public GameObject myShadow;
	public float shadow_xOffset;
	public float shadow_yOffset;
	
	[Header("HP INDICATORs")]
	public GameObject hpIndicator;
	
	[Header("VFXs")]
	private SpriteRenderer mySR;
	public GameObject myImg;
	private Material ogMat;
	public Material hurtMat;
	public Material poisonedMat;
	public float flashDuration;
	public GameObject PS_blood;
	public float hurt_bulletTime_scale;
	public float hurt_bulletTime_duration;
	
	[Header("FOR PLAYTESTs")]
	public bool undying;

	private void Awake()
	{
		mySR = myImg.GetComponent<SpriteRenderer>();
		ogMat = mySR.material;
		shoot_timer = Random.Range(2, shoot_interval);
		_spawnIFrameOg = spawn_iFrame;
	}
	private void OnEnable()
	{
		mySR.material = ogMat;
		spawn_iFrame = _spawnIFrameOg;
		_hurtStunned = false;
	}
	private void Update()
	{
		MoveLogic();
		if (spawn_iFrame > 0) // i-frames when spawned so that it won't get killed the moment it is spawned
		{
			spawn_iFrame -= Time.deltaTime;
		}
		Shoot();
		if (myEnemyType != EnemyType.score)
		{
			UpdateDot();
		}
		FacePlayer();
		UpdateHpIndicator();
		// not in use, need to implement again
		ControlShadow();
		FreezeHPIndicatorRotation();
	}
	private void Shoot()
	{
		if (myEnemyType == EnemyType.shooter)
		{
			if (shoot_timer > 0)
			{
				shoot_timer -= Time.deltaTime;
			}
			else
			{
				shoot_timer = shoot_interval;
				GameObject bullet = Instantiate(bulletPrefab);
				bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
			}
		}
	}
	private void MoveLogic()
	{
		if (!_hurtStunned &&
			  myEnemyType != EnemyType.score)
		{
			transform.position = Vector3.MoveTowards(transform.position, PlayerControlScript.me.transform.position, moveSpd * Time.deltaTime);
		}
	}
	public void GetHit(int amount, EnumStorage.DmgType dmgType)
	{
		if (spawn_iFrame <= 0 &&
			hp > 0 &&
			amount > 0)
		{
			All_Hit_VFXs();
			switch (dmgType)
			{
				case EnumStorage.DmgType.poison:
					poisonStack = 0;
					break;
				case EnumStorage.DmgType.bullet:
					TriggerCommonOnHitEvent();
					break;
				case EnumStorage.DmgType.explosion:
					TriggerCommonOnHitEvent();
					break;
				case EnumStorage.DmgType.playerSlash:
					TriggerCommonOnHitEvent();
					break;
			}
			if (!undying)
			{
				hp -= amount;
				if (hp <= 0)
				{
					Die(dmgType);
				}
			}
		}
	}
	private void TriggerCommonOnHitEvent()
	{
		CardManagerNew.me.activatedCard?.GetComponent<CardEventTrigger>().InvokeEnemyHitEvent(gameObject); //! TIMEPOINT: when enemy is hit
		if (LingerEffectManager.me.onKillBecomesOnHit)
		{
			CardManagerNew.me.activatedCard?.GetComponent<CardEventTrigger>().InvokeOnEnemyKilled(gameObject); //! TIMEPOINT: when enemy is killed
		}
	}
	private void UpdateDot()
	{
		// if (poison_duration>0)
		// {
		// 	poison_duration -= Time.deltaTime;
		// 	if (poison_timer>0)
		// 	{
		// 		poison_timer-=Time.deltaTime;
		// 	}
		// 	else
		// 	{
		// 		poison_timer = poison_interval;
		// 		GetHit(poison_dmg, EnumStorage.DmgType.poison);
		// 	}
		// }
		// else if (mySR.material.name.Contains(poisonedMat.name))
		// {
		// 	mySR.material = ogMat;
		// }
	}
	public void StartPoison_deprecated() // call this to activate poison
	{
		poison_duration = AbilityManagerScript.me.poison_duration;
		poison_dmg = AbilityManagerScript.me.poison_dmg;
		poison_interval = AbilityManagerScript.me.poison_interval;
		poison_timer = poison_interval; // initialize poison timer
		// change material to indicate poisoned state
		if (!mySR.material.name.Contains(poisonedMat.name))
		{
			mySR.material = poisonedMat;
		}
	}
	private IEnumerator HurtStun()
	{
		_hurtStunned = true;
		yield return new WaitForSecondsRealtime(hurt_stunDuration);
		_hurtStunned = false;
	}
	private void Die(EnumStorage.DmgType dmgType)
	{
		All_Hit_VFXs();
		switch (myEnemyType)
		{
			case EnemyType.brawler:
				ShootOutCorpse(1);
				AbilityManagerScript.onEnemyKilled?.Invoke(transform.position);
				if (poison_duration > 0)
				{
					AbilityManagerScript.onPoisonedEnemyKilled?.Invoke(transform.position);
				}
				GameObjectPoolScript.me.EnemyPool.Release(gameObject);
				break;
			case EnemyType.shooter:
				ShootOutCorpse(1);
				AbilityManagerScript.onEnemyKilled?.Invoke(transform.position);
				if (poison_duration > 0)
				{
					AbilityManagerScript.onPoisonedEnemyKilled?.Invoke(transform.position);
				}
				GameObjectPoolScript.me.EnemyPool.Release(gameObject);
				break;
			case EnemyType.score:
				if (chanceToScore > Random.value)
				{
					GameManager.me.score++;
				}
				AbilityManagerScript.onScoreKilled?.Invoke(transform.position);
				GameObjectPoolScript.me.ScorePool.Release(gameObject);
				break;
			default:
				break;
		}
		if (dmgType == EnumStorage.DmgType.playerSlash)
		{
			if (!LingerEffectManager.me.onKillBecomesOnHit)
			{
				CardManagerNew.me.activatedCard?.GetComponent<CardEventTrigger>().InvokeOnEnemyKilled(gameObject); //! TIMEPOINT: when enemy is killed by slash
			}
		}
		EnemySpawnerScript.me.enemies.Remove(gameObject);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") &&
			collision.GetComponent<PlayerHurtBoxScript>() &&
			myEnemyType != EnemyType.score)
		{
			collision.GetComponent<PlayerHurtBoxScript>().GetHit_byEnemy(dmg);
		}
	}
	public void ShootOutCorpse(float _chanceToScore)
	{
		//var obj = Instantiate(obj2Shoot);
		var obj = GameObjectPoolScript.me.ScorePool.Get();
		obj.GetComponent<EnemyScript>().hp = 1;
		EnemySpawnerScript.me.enemies.Add(obj);
		var randX = Random.Range(-1f, 1f);
		var randY = Random.Range(-1f, 1f);
		Vector3 randDir = new(randX, randY, 0);
		randDir = randDir.normalized;
		obj.transform.position = new Vector3(transform.position.x, transform.position.y, obj.transform.position.z);
		if (obj.GetComponent<EnemyScript>())
		{
			obj.GetComponent<EnemyScript>().chanceToScore = _chanceToScore;
			if (_chanceToScore <= 0)
			{
				obj.GetComponentInChildren<SpriteRenderer>().material = hurtMat;
			}
		}
		obj.GetComponent<Rigidbody2D>().AddForce(randDir * GameManager.me.score_spawnForce, ForceMode2D.Impulse);
	}
	public void RestartButton_Init()
	{
		hp = 1;
		transform.localPosition = new Vector3(1.7f, -1.7f, 1);
		myImg.transform.localPosition = new Vector3(0, 0, 0);
		myShadow.transform.localPosition = new Vector3(.2f, -.2f, 1);
	}
	#region VISUALs
	private void FacePlayer()
	{
		if (myEnemyType != EnemyType.bullet &&
			myEnemyType != EnemyType.score)
		{
			Vector3 dir = PlayerControlScript.me.transform.position - transform.position;
			dir = dir.normalized;
			var targetRot = Quaternion.LookRotation(Vector3.forward, dir);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpd * Time.deltaTime);
		}
	}
	private void UpdateHpIndicator()
	{
		if (hp > 1)
		{
			hpIndicator.GetComponent<TextMeshPro>().text = hp + "";
		}
		else
		{
			hpIndicator.GetComponent<TextMeshPro>().text = "";
		}
	}
	#endregion
	#region DEPRECATED
	// private void OnCollisionEnter2D(Collision2D other)
	// {
	// 	if (other.gameObject.CompareTag("Player") &&
	// 	myEnemyType != EnemyType.score)
	// 	{
	// 		if (hp > 0)
	// 		{
	// 			other.gameObject.GetComponent<Rigidbody2D>().velocity = other.gameObject.GetComponent<Rigidbody2D>().velocity * 0.2f;
	// 		}
	// 	}
	// }
	private void FreezeHPIndicatorRotation()
	{
		hpIndicator.transform.rotation = Quaternion.Euler(0, 0, 0);
	}
	private void ControlShadow()
	{
		// shadow keeps a constant relative position
		// shadow mimics rotation
		//myShadow.transform.SetPositionAndRotation(new Vector3(transform.position.x + shadow_xOffset, transform.position.y + shadow_yOffset, 1), transform.rotation);
	}
	#endregion
	#region HIT VFXs
	private void All_Hit_VFXs()
	{
		//CameraScript.me.CamShake_EnemyHit();
		var ps = Instantiate(PS_blood);
		ps.transform.position = transform.position;
		StartCoroutine(HurtStun());
		HitFlash();
		//if (PlayerControlScript.me.hitBulletTime)
		//{
		//    StartCoroutine(Hurt_BulletTime());
		//}
		//else
		//{
		//    Die();
		//}
	}
	private void HitFlash()
	{
		mySR.material = hurtMat;
		StartCoroutine(UndoHitFlashMat());
	}
	IEnumerator UndoHitFlashMat()
	{
		yield return new WaitForSecondsRealtime(flashDuration);
		if (poison_duration > 0)
		{
			mySR.material = poisonedMat;
		}
		else
		{
			mySR.material = ogMat;
		}
	}
	IEnumerator Hurt_BulletTime()
	{
		Time.timeScale = hurt_bulletTime_scale;
		yield return new WaitForSecondsRealtime(hurt_bulletTime_duration);
		//Die();
		Time.timeScale = 1;
	}
	#endregion
}