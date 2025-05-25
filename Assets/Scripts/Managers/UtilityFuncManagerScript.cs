using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using UnityEngine;

public class UtilityFuncManagerScript : MonoBehaviour
{
	#region SINGLETON
	public static UtilityFuncManagerScript me;
	private void Awake()
	{
		me = this;
	}
	#endregion

	public float ConvertV2ToAngle(Vector2 dir)
	{
		return Mathf.Atan2(dir.x, dir.y) * (180 / Mathf.PI);
	}

	// used to shuffle given GameObject list
	public List<GameObject> ShuffleList(List<GameObject> list)
	{
		List<GameObject> shuffled = new();
		shuffled = list.OrderBy(x => Random.value).ToList();
		return shuffled;
	}
	// used to copy ability list
	public void CopyList(List<AbilityManagerScript.Abilities> from, List<AbilityManagerScript.Abilities> to)
	{
		to.Clear();
		foreach (AbilityManagerScript.Abilities fromItem in from)
		{
			to.Add(fromItem);
		}
	} 
}
