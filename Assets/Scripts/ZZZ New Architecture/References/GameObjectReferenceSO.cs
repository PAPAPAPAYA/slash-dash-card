using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameObjectReferenceSO : ScriptableObject
{
	public GameObject goRef;
	public GameObject Value()
	{
		return goRef;
	}
}
