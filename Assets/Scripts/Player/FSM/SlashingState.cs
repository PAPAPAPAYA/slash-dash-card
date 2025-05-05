using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SlashingState : State
{
	public override void OnEnter(StateController stateController)
	{
		base.OnEnter(stateController);
		AbilityContainerScript cardBeingUsed = sc.cm.hand[^1].GetComponent<AbilityContainerScript>();
		sc.ams.CardSystem_AdjustAbility(cardBeingUsed.myAbility, true); // load ability
		sc.cm.activatedCard = cardBeingUsed; // record activated card
		sc.cm.MoveCard_HandLastToGraveLast(); // move last card in hand to be the last card in grave
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
		AbilityContainerScript cardSentToGrave = sc.cm.graveyard[^1].GetComponent<AbilityContainerScript>();
		sc.ams.CardSystem_AdjustAbility(cardSentToGrave.myAbility, false); // unload ability
		if (sc.cm.graveyard[^1].GetComponent<AbilityContainerScript>().tempCard)
		{
			sc.cm.graveyard.RemoveAt(sc.cm.graveyard.Count - 1);
		}
	}
}
