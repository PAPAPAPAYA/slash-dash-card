using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumStorage : MonoBehaviour
{
	public enum PosType
	{
		playerPos
	};

	public enum DmgType
	{
		playerSlash,
		explosion,
		bullet,
		poison
	};
}
