using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAtPosEffect : MonoBehaviour
{
	public int explosion_dmg = 1;
	//[Header("REFs")]
	//public GameObjectReferenceSO explosionAreaRef;
	//public GameObject explosionAreaPrefab;
	//public EnumStorage.PosType posType;

	public void MakeExplosion_atPos(GameObject posObj)
	{
		//explosionAreaPrefab = explosionAreaRef.Value();
		//GameObject explosion = Instantiate(Prefab_explosionArea);
		var explosion = GameObjectPoolScript.me.ExplosionPool.Get();
		explosion.GetComponent<ExplosionAreaScript>().dmg = explosion_dmg;
		// switch (posType)
		// {
		// 	case EnumStorage.PosType.playerPos:
		// 		explosion.transform.position = PlayerControlScript.me.transform.position;
		// 		break;
		// 	default:
		// 		break;
		// }
		explosion.transform.position = posObj.transform.position;
	}
}
