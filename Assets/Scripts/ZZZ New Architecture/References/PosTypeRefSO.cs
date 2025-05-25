using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PosTypeRefSO : ScriptableObject
{
	public EnumStorage.PosType posType;
	public EnumStorage.PosType Value()
	{
		return posType;
	}
}
