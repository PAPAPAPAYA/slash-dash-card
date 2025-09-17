using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAtPosEffect : MonoBehaviour
{
	public int explosionDmg = 1;

	public void MakeExplosion_atPos(GameObject posObj)
	{
		var explosion = GameObjectPoolScript.me.ExplosionPool.Get();
		explosion.GetComponent<ExplosionAreaScript>().dmg = explosionDmg;
		explosion.transform.position = posObj.transform.position;
	}
}
