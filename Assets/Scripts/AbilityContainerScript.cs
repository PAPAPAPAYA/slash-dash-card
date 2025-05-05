using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityContainerScript : MonoBehaviour
{
	public string abilityName;
	public AbilityManagerScript.Abilities myAbility;
	public bool tempCard;
	public int dmg = 1;
	#region ORIGINAL CARD INFO
	public string og_abilityName;
	public AbilityManagerScript.Abilities og_ability;
	public bool og_tempCard;
	public int og_dmg;
	#endregion
	void Start()
	{
		og_abilityName = abilityName;
		og_ability = myAbility;
		og_tempCard = tempCard;
		og_dmg = dmg;
	}
	
}
