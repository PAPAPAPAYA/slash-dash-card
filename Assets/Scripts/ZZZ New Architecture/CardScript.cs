using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
	public string cardName;
	public int dmg;
	public bool tempCard = false;
	private int _ogDmg;
	public int myHandIndex;
	public int myGraveIndex;
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
