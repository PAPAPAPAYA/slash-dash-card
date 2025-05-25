using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVaribaleSO : ScriptableObject
{
	public int value;
	public bool resetOnStart;
	private void OnEnable()
	{
		if (resetOnStart)
		{
			value = 0;
		}
	}
}
