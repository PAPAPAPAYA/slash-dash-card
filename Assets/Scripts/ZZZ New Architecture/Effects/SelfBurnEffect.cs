using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBurnEffect : MonoBehaviour
{
	public int selfBurnAmount;
	public void SelfBurn()
	{
		PlayerControlScript.me.GetHit(selfBurnAmount);
	}
}
