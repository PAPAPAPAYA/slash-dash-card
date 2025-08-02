using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityContainerScript : MonoBehaviour
{
	public string abilityName;
	public List<AbilityManagerScript.Abilities> myAbilities;
	public bool tempCard;
	public int dmg = 1;
	[Header("AMMO")]
	public int ammoAddAmount;
	[Header("EXTRA SLASHES")]
	public int extraSlashAmount; // slash amount -1
	[Header("SELF BURN")]
	public int selfBurnAmount;
	#region ORIGINAL CARD INFO
	[HideInInspector]
	public string og_abilityName;
	[HideInInspector]
	public List<AbilityManagerScript.Abilities> og_myAbilities;
	[HideInInspector]
	public bool og_tempCard;
	[HideInInspector]
	public int og_dmg;
	[HideInInspector]
	public int og_ammoAddAmount;
	public int og_extraSlashAmount;
	public int og_selfBurnAmount;
	#endregion
	void Start()
	{
		og_abilityName = abilityName;
		//UtilityFuncManagerScript.me.CopyList(myAbilities, og_myAbilities);
		og_tempCard = tempCard;
		og_dmg = dmg;
		og_ammoAddAmount = ammoAddAmount;
	}
	public void ResetVariables()
	{
		dmg = og_dmg;
		ammoAddAmount = og_ammoAddAmount;
		extraSlashAmount = og_extraSlashAmount;
		selfBurnAmount = og_selfBurnAmount;
	}
}
