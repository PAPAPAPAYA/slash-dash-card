using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumStorage : MonoBehaviour
{
	public enum Tag
	{
		none,
		curse,
		sacrificed,
		ammo,
		madness,
		corpse,
		killing
	}
	public enum DmgType
	{
		playerSlash,
		explosion,
		bullet,
		poison
	};

	public enum GameState
	{
		game,
		upgrade
	};

	public enum BuffCategory
	{
		slashDmg,
		slashWidth,
		becomeMadness
	}
}
