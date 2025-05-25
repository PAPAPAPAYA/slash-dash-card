using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
	public string cardName;
	public int dmg;
	public int og_dmg;
	void OnEnable()
	{
		og_dmg = dmg;
	}
	public void ResetDmg()
	{
		dmg = og_dmg;
	}
}
