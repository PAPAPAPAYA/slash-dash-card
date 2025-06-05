using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
	public string cardName;
	public int dmg;
	public bool tempCard;
	private int _ogDmg;
	private void OnEnable()
	{
		_ogDmg = dmg;
	}
	public void ResetDmg()
	{
		dmg = _ogDmg;
	}

	public void SetCostPayed()
	{
		CardManagerNew.me.costPayed = true;
	}
}
