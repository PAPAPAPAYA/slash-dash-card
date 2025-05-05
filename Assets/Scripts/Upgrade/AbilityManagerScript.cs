using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class AbilityManagerScript : MonoBehaviour
{
	#region SINGLETON
	public static AbilityManagerScript me;
	private void Awake()
	{
		me = this;
	}
	#endregion
	[Header("BASICS")]
	private GameObject player;
	private PlayerControlScript playerScript;
	public enum Abilities
	{
		bullet,
		explosiveBullet,
		homingBullet,
		piercingBullet,
		deathExplosion,
		poisonCloud_scoreGet,
		poisonCloud_poisonedEnemyKilled,
		MoreSlash,
		spikeSlash,
		copyFromGrave_Slash,
		madnessCut,
		copyLast
	};
	[Header("MADNESS")]
	public int madnessCount;
	[Header("SPIKE")]
	public GameObject Prefab_spike;
	public int spike_dmg;
	public float spike_dist;
	[Header("POISON")]
	public GameObject Prefab_poisonArea;
	public float poison_duration;
	public int poison_dmg;
	public float poison_interval;
	[Header("BULLET")]
	public int bulletHP;
	public int onEnemyHit_BulletAmount;
	public float bullet_rotateSpd;
	public GameObject prefab_bulletExplosionArea;
	public int bulletExplosion_dmg;
	public GameObject knifePrefab;
	[Header("KNOCKBACK")]
	public GameObject knockBackArea;
	[Header("EXPLOSION")]
	public GameObject Prefab_explosionArea;
	public int explosion_dmg;
	[Header("WALL BOUNCE")]
	public PhysicsMaterial2D phyMat_noBounce;
	public PhysicsMaterial2D phyMat_Bounce;
	[Header("SLASH AMOUNT")]
	public float colliderInterval;
	public GameObject actionPrefab;

	#region DELEGATES
	public delegate void OnEnemyHit(ActionColliderPrefabScript acps);
	public static OnEnemyHit onEnemyHit;
	public delegate void OnPlayerHit();
	public static OnPlayerHit onPlayerHit;
	public delegate void OnPlayerSlash();
	public static OnPlayerSlash onPlayerSlash;
	public delegate void OnEnemyKilled(Vector2 pos);
	public static OnEnemyKilled onEnemyKilled;
	public delegate void OnKnifeHit(Vector2 pos);
	public static OnKnifeHit onKnifeHit;
	public delegate void OnScoreKilled(Vector2 pos);
	public static OnScoreKilled onScoreKilled;
	public delegate void OnPoisonedEnemyKilled(Vector2 pos);
	public static OnPoisonedEnemyKilled onPoisonedEnemyKilled;
	public delegate void WhenSlashing(Vector2 pos);
	public static WhenSlashing whenSlashing;
	#endregion

	[Header("ACTIVATED UPGRADES")]
	public bool onPlayerHit_KnockBack = false;
	public bool wallBounce = false;
	public bool speedUp = false;
	public int lvl_onEnemyHit_bullet;
	public int lvl_homingBullet;
	public int lvl_piercingBullet;
	public int lvl_slashAmount_extra; // 1 for each side
	public int lvl_onEnemyKilled_explosion;
	public int lvl_onBulletHit_explosion;
	public int lvl_onScoreKilled_poison;
	public int lvl_onPoisonedEnemyKilled_poison;
	public int lvl_spikeSlash;
	public Abilities abilityeToGrant;
	private void Start()
	{
		player = PlayerControlScript.me.gameObject;
		playerScript = PlayerControlScript.me;

		onPlayerSlash += MakeSlashCollider;
		onPlayerSlash += MakeExtraSlashColliders;

		if (onPlayerHit_KnockBack)
		{
			onPlayerHit += ActivateKnockBackArea;
		}

		// if (speedUp) // not in use, when slashes an enemy, give a speed boost
		// {
		// 	onEnemyHit += SpeedUp;
		// }
	}
	void Update()
	{
		// for testing
		if (Input.GetKeyDown(KeyCode.T))
		{
			GainUpgrade(abilityeToGrant);
		}
	}
	public void GainUpgrade(Abilities ability)
	{
		switch (ability)
		{
			case Abilities.spikeSlash:
				lvl_spikeSlash++;
				if (lvl_spikeSlash == 1)
				{
					whenSlashing += SpawnSpike_atPos;
				}
				else
				{
					UpgradeSpike();
				}
				//print("spike slash");
				break;
			case Abilities.bullet:
				lvl_onEnemyHit_bullet++;
				if (lvl_onEnemyHit_bullet == 1)
				{
					onEnemyHit += SpawnKnife_atPos;
				}
				else
				{
					// upgrade
					UpgradeBullet();
				}
				//print("Bullet on enemy hit");
				break;
			case Abilities.homingBullet:
				lvl_homingBullet++;
				if (lvl_homingBullet > 1)
				{
					// upgrade
					UpgradeHomingBullet();
				}
				//print("homing bullets");
				break;
			case Abilities.explosiveBullet:
				lvl_onBulletHit_explosion++;
				if (lvl_onBulletHit_explosion == 1)
				{
					onKnifeHit += MakeBulletExplosion_atPos;
				}
				else
				{
					// upgarde
					UpgradeExplosiveBullet();
				}
				//print("explosive bullet");
				break;
			case Abilities.piercingBullet:
				lvl_piercingBullet++;
				// upgrade
				UpgradePiercingBullet();
				//print("piercing bullet");
				break;
			case Abilities.poisonCloud_scoreGet:
				lvl_onScoreKilled_poison++;
				if (lvl_onScoreKilled_poison == 1)
				{
					onScoreKilled += MakePoisonExplosion_atPos;
				}
				else
				{
					// upgrade
					UpgradePoisonCloud();
				}
				//print("poison on exp");
				break;
			case Abilities.deathExplosion:
				lvl_onEnemyKilled_explosion++;
				if (lvl_onEnemyKilled_explosion == 1)
				{
					onEnemyKilled += MakeExplosion_atPos;
				}
				else
				{
					// upgrade
					UpgradeDeathExplosion();
				}
				//print("death explosion");
				break;
			case Abilities.poisonCloud_poisonedEnemyKilled:
				lvl_onPoisonedEnemyKilled_poison++;
				if (lvl_onPoisonedEnemyKilled_poison == 1)
				{
					onPoisonedEnemyKilled += MakePoisonExplosion_atPos;
				}
				else
				{
					// upgrade
					UpgradeContagiousPoison();
				}
				//print("contagious poison");
				break;
			case Abilities.MoreSlash:
				lvl_slashAmount_extra++;
				break;
			default:
				break;
		}
	}
	public void CardSystem_AdjustAbility(Abilities ability, bool loadAbility)
	{
		switch (ability)
		{
			case Abilities.copyLast:
				if (loadAbility)
				{
					if (CardManager.me.graveyard.Count > 0)
					{
						AbilityContainerScript lastUsedCard = CardManager.me.graveyard[^1].GetComponent<AbilityContainerScript>();
						CardSystem_AdjustAbility(lastUsedCard.myAbility, true); // load ability
					}
				}
				else
				{
					if (CardManager.me.graveyard.Count > 1)
					{
						AbilityContainerScript secondLastUsedCard = CardManager.me.graveyard[^2].GetComponent<AbilityContainerScript>();
						CardSystem_AdjustAbility(secondLastUsedCard.myAbility, false); // unload ability
					}
				}
				break;
			case Abilities.madnessCut:
				if (loadAbility)
				{
					onEnemyHit += ApplyMadnessDmg;
					onPlayerSlash += AddMadness;
				}
				else
				{
					onEnemyHit -= ApplyMadnessDmg;
					onPlayerSlash -= AddMadness;
				}
				break;
			case Abilities.copyFromGrave_Slash:
				if (loadAbility)
				{
					onPlayerSlash += CardManager.me.CopyCard_randomGraveToHandFirst;
				}
				else
				{
					onPlayerSlash -= CardManager.me.CopyCard_randomGraveToHandFirst;
				}
				break;
			case Abilities.spikeSlash:
				if (loadAbility)
				{
					whenSlashing += SpawnSpike_atPos;
				}
				else
				{
					whenSlashing -= SpawnSpike_atPos;
				}
				//print("spike slash");
				break;
			case Abilities.bullet:
				if (loadAbility)
				{
					onEnemyHit += SpawnKnife_atPos;
				}
				else
				{
					onEnemyHit -= SpawnKnife_atPos;
				}
				//print("Bullet on enemy hit");
				break;
			default:
				break;
		}
	}
	#region ABILITY FUNCs
	private void ResumeOG(AbilityContainerScript acs)
	{
		acs.abilityName = acs.og_abilityName;
		acs.myAbility = acs.og_ability;
		acs.tempCard = acs.og_tempCard;
		acs.dmg = acs.og_dmg;
	}
	private void CopyGraveLast(AbilityContainerScript acs)
	{
		AbilityContainerScript lastUsedCard = CardManager.me.graveyard[^1].GetComponent<AbilityContainerScript>();
		acs.abilityName = lastUsedCard.abilityName;
		acs.myAbility = lastUsedCard.myAbility;
		acs.tempCard = lastUsedCard.tempCard;
		acs.dmg = lastUsedCard.dmg;
	}
	private void AddMadness()
	{
		madnessCount++;
	}
	private void ApplyMadnessDmg(ActionColliderPrefabScript acps)
	{
		acps.dmg = acps.dmg_og + madnessCount;
	}
	private void SpawnSpike_atPos(Vector2 pos)
	{
		GameObject spike = Instantiate(Prefab_spike);
		ExplosionAreaScript eas = spike.GetComponent<ExplosionAreaScript>();
		eas.dmg = spike_dmg;
		spike.transform.position = pos;
	}
	private void MakePoisonExplosion_atPos(Vector2 pos)
	{
		GameObject collider = Instantiate(Prefab_poisonArea);
		ExplosionAreaScript eas = collider.GetComponent<ExplosionAreaScript>();
		eas.dmg = poison_dmg;
		eas.dot = true;
		collider.transform.position = pos;
	}
	private void MakeExplosion_atPos(Vector2 pos)
	{
		GameObject explosion = Instantiate(Prefab_explosionArea);
		explosion.GetComponent<ExplosionAreaScript>().dmg = explosion_dmg;
		explosion.transform.position = pos;
	}
	private void MakeBulletExplosion_atPos(Vector2 pos)
	{
		GameObject explosion = Instantiate(prefab_bulletExplosionArea);
		explosion.GetComponent<ExplosionAreaScript>().dmg = bulletExplosion_dmg;
		explosion.transform.position = pos;
	}
	private void MakeSlashCollider()
	{
		ActionPrefabScript aps = actionPrefab.GetComponent<ActionPrefabScript>();
		playerScript.actionColliders.Add(Instantiate(aps.colliderPrefab, player.transform.position, player.transform.rotation, player.transform));
	}
	private void MakeExtraSlashColliders()
	{
		ActionPrefabScript aps = actionPrefab.GetComponent<ActionPrefabScript>();
		// make extras on the left
		for (int i = 0; i < lvl_slashAmount_extra; i++)
		{
			playerScript.actionColliders.Add
			(
			  Instantiate(aps.colliderPrefab, player.transform.TransformPoint(new Vector3(colliderInterval * (i + 1), 0, 0)), player.transform.rotation, player.transform)
			);
		}
		// make extras on the right
		for (int i = 0; i < lvl_slashAmount_extra; i++)
		{
			playerScript.actionColliders.Add
			(
			  Instantiate(aps.colliderPrefab, player.transform.TransformPoint(new Vector3(-colliderInterval * (i + 1), 0, 0)), player.transform.rotation, player.transform)
			);
		}
	}
	private void SpawnKnife_atPos(ActionColliderPrefabScript acps)
	{
		for (int i = 0; i < onEnemyHit_BulletAmount; i++)
		{
			GameObject knife = Instantiate(knifePrefab);
			knife.GetComponent<KnifeScript>().hp = bulletHP;
			knife.transform.position = player.transform.position;
			knife.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
		}
	}
	private void ActivateKnockBackArea()
	{
		knockBackArea.transform.position = player.transform.position;
		knockBackArea.SetActive(true);
	}
	private void SpeedUp()
	{
		Vector2 playerVel = player.GetComponent<Rigidbody2D>().velocity;
		Vector2 playerDir = playerVel.normalized;
		Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
		playerRB.AddForce(playerDir * 10f, ForceMode2D.Impulse);
		playerRB.drag *= 2f;
	}
	#endregion
	#region ABILITY UPGRADEs
	private void UpgradeSpike()
	{
		spike_dmg++;
	}
	private void UpgradeBullet()
	{
		onEnemyHit_BulletAmount++;
	}
	private void UpgradeHomingBullet()
	{
		bullet_rotateSpd += 50f;
	}
	private void UpgradeExplosiveBullet()
	{
		bulletExplosion_dmg++;
	}
	private void UpgradePiercingBullet()
	{
		bulletHP++;
	}
	private void UpgradePoisonCloud()
	{
		Prefab_poisonArea.transform.localScale *= 1.1f;
	}
	private void UpgradeDeathExplosion()
	{
		explosion_dmg++;
	}
	private void UpgradeContagiousPoison() // currently upgrading this and poison cloud do the same thing
	{
		Prefab_poisonArea.transform.localScale *= 1.1f;
	}
	#endregion
}
