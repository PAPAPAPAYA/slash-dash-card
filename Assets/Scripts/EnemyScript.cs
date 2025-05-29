using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using TMPro;

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
	private bool hurt_stunned;
	public float spawn_iFrame;
	public EnemyType myEnemyType;
	public bool shielded;
	public float rotSpd;
	public int dmg;
	private float poison_timer; // timer used to time the dot
	private float poison_interval;
	private float poison_duration; // how long does the dot is active
	private int poison_dmg; // how much damage the dot does
	[Header("SLIMERs")]
	public List<GameObject> slimees;
	public float slimee_spawnForce;
	[Header("SHOOTERs")]
	public float shoot_interval;
	private float shoot_timer;
	public float shooter_stopDis;
	public GameObject bulletPrefab;
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
	private void Start()
	{
		mySR = myImg.GetComponent<SpriteRenderer>();
		ogMat = mySR.material;
		shoot_timer = Random.Range(2, shoot_interval);
	}
	private void Update()
	{
		MoveLogic();
		if (spawn_iFrame > 0) // i-frames when spawned so that it won't get killed the moment it is spanwed
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
		// not in use
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
		if (!hurt_stunned &&
			  myEnemyType != EnemyType.score)
		{
			transform.position = Vector3.MoveTowards(transform.position, PlayerControlScript.me.transform.position, moveSpd * Time.deltaTime);
		}
	}
	public void GetHit(int amount, EnumStorage.DmgType dmgType)
	{
		if (spawn_iFrame <= 0 &&
			hp > 0)
		{
			if (!undying)
			{
				hp -= amount;
				if (hp <= 0)
				{
					Die(dmgType);
				}
			}
			All_Hit_VFXs();
		}
	}
	private void UpdateDot()
	{
		if (poison_duration>0)
		{
			poison_duration -= Time.deltaTime;
			if (poison_timer>0)
			{
				poison_timer-=Time.deltaTime;
			}
			else
			{
				poison_timer = poison_interval;
				GetHit(poison_dmg, EnumStorage.DmgType.poison);
			}
		}
		else if (mySR.material.name.Contains(poisonedMat.name))
		{
			mySR.material = ogMat;
		}
	}
	public void StartPoison() // call this to activate poison
	{
		poison_duration = AbilityManagerScript.me.poison_duration;
		poison_dmg = AbilityManagerScript.me.poison_dmg;
		poison_interval = AbilityManagerScript.me.poison_interval;
		poison_timer = poison_interval; // initialize poison timer
		if (!mySR.material.name.Contains(poisonedMat.name))
		{
			mySR.material = poisonedMat;
		}
	}
	private IEnumerator HurtStun()
	{
		hurt_stunned = true;
		yield return new WaitForSecondsRealtime(hurt_stunDuration);
		hurt_stunned = false;
	}
	private void Die(EnumStorage.DmgType dmgType)
	{
		switch (myEnemyType)
		{
			case EnemyType.brawler:
				ShootOutCorpse(GameManager.me.scorePrefab, GameManager.me.score_spawnForce);
				AbilityManagerScript.onEnemyKilled?.Invoke(transform.position);
				if (poison_duration > 0)
				{
					AbilityManagerScript.onPoisonedEnemyKilled?.Invoke(transform.position);
				}
				break;
			case EnemyType.shooter:
				ShootOutCorpse(GameManager.me.scorePrefab, GameManager.me.score_spawnForce);
				AbilityManagerScript.onEnemyKilled?.Invoke(transform.position);
				if (poison_duration > 0)
				{
					AbilityManagerScript.onPoisonedEnemyKilled?.Invoke(transform.position);
				}
				break;
			case EnemyType.score:
				GameManager.me.score++;
				AbilityManagerScript.onScoreKilled?.Invoke(transform.position);
				break;
			default:
				break;
		}
		All_Hit_VFXs();
		if (dmgType == EnumStorage.DmgType.playerSlash)
		{
			CardManagerNew.me.activatedCard?.GetComponent<CardEventTrigger>().InvokeOnEnemyKilled();
		}
		EnemySpawnerScript.me.enemies.Remove(gameObject);
		Destroy(gameObject);
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
	private void ShootOutCorpse(GameObject obj2Shoot, float spawnForce)
	{
		GameObject obj = Instantiate(obj2Shoot);
		float randX = Random.Range(-1f, 1f);
		float randY = Random.Range(-1f, 1f);
		Vector3 randDir = new(randX, randY, 0);
		randDir = randDir.normalized;
		obj.transform.position = new Vector3(transform.position.x, transform.position.y, obj.transform.position.z);
		obj.GetComponent<Rigidbody2D>().AddForce(randDir * spawnForce, ForceMode2D.Impulse);
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
		GameObject ps = Instantiate(PS_blood);
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