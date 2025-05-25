using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSlashEffect : MonoBehaviour
{
	public int extraSlashAmount;
	public void ExtraSlashCollider()
	{
		for (int i = 0; i < extraSlashAmount; i++)
		{
			ActionPrefabScript aps = PlayerControlScript.me.actionPrefab.GetComponent<ActionPrefabScript>();
			PlayerControlScript.me.actionColliders.Add(Instantiate(aps.colliderPrefab, 
																PlayerControlScript.me.transform.position, 
																PlayerControlScript.me.transform.rotation, 
																PlayerControlScript.me.transform));
		}
	}
}
