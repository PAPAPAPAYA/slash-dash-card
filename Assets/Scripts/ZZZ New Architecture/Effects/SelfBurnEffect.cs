using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBurnEffect : MonoBehaviour
{
	public int selfBurnAmount;
	public void SelfBurn(bool cost)
	{
		if (PlayerControlScript.me.hp > selfBurnAmount)
		{
			PlayerControlScript.me.GetHit(selfBurnAmount);
			if (cost)
			{
				CardManagerNew.me.costPayed = true;
			}
		}
		else
		{
			if (cost)
			{
				CardManagerNew.me.costPayed = false;
			}
		}
	}
}
