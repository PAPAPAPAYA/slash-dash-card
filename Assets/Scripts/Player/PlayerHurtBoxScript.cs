using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBoxScript : MonoBehaviour
{
	#region SINGLETON
	public static PlayerHurtBoxScript me;
	private void Awake()
	{
		me = this;
	}
	#endregion
	private PlayerControlScript pcs;
	
	private void Start()
	{
		pcs = PlayerControlScript.me;
	}
	private void Update()
	{
		transform.position = pcs.transform.position;
	}
	public void GetHit_byEnemy(int hitAmount)
	{
		if (!pcs.invincible)
		{
			pcs.hp -= hitAmount;
			GameObject ps = Instantiate(pcs.PS_blood);
			ps.transform.position = transform.position;
			AbilityManagerScript.onPlayerHitByEnemy?.Invoke();
		}
	}
}
