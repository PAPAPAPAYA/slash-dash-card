using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ActionColliderPrefabScript : MonoBehaviour
{
	public int dmg;
	private PlayerControlScript pcs;
	public int dmg_og;
	
	private void Start()
	{
		pcs = PlayerControlScript.me;
		dmg_og = dmg;
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.gameObject.CompareTag("Enemy"))&&
			collision.gameObject.GetComponent<EnemyScript>())
		{
			if (collision.gameObject.GetComponent<EnemyScript>().spawn_iFrame <= 0)
			{
				AbilityManagerScript.onEnemyHit?.Invoke();
				CalculateDmg();
				collision.gameObject.GetComponent<EnemyScript>().GetHit(dmg, EnumStorage.DmgType.playerSlash);
			}
		}
	}
	private void CalculateDmg()
	{
		CardManagerNew.me.activatedCard.GetComponent<CardEventTrigger>().InvokeOnDmgCalculation();
		dmg = CardManagerNew.me.activatedCard.dmg;
	}
}
