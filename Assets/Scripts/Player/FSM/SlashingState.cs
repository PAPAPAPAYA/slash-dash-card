using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SlashingState : State
{
	public override void OnEnter(StateController stateController)
	{
		base.OnEnter(stateController);
		sc.ams.CardSystem_AdjustAbility(sc.cm.hand[^1].GetComponent<AbilityContainerScript>().myAbility, true); // load ability
		sc.cm.activatedCard = sc.cm.hand[^1].GetComponent<AbilityContainerScript>(); // record activated card
		sc.cm.MoveCard_HandLastToGraveLast();
		AbilityManagerScript.onPlayerSlash?.Invoke();
	}
	public override void OnUpdate()
	{
		base.OnUpdate();
		if (!sc.pcs.moving)
		{
			sc.ChangeState(sc.idleState);
		}
	}
	public override void OnExit()
	{
		base.OnExit();
		sc.ams.CardSystem_AdjustAbility(sc.cm.graveyard[^1].GetComponent<AbilityContainerScript>().myAbility, false); // unload ability
		if (sc.cm.graveyard[^1].GetComponent<AbilityContainerScript>().tempCard)
		{
			sc.cm.graveyard.RemoveAt(sc.cm.graveyard.Count - 1);
		}
	}
}
