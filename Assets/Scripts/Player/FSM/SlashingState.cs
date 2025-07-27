using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;

public class SlashingState : State
{
	public override void OnEnter(StateController stateController)
	{
		base.OnEnter(stateController);
		
		CardScript cardBeingUsed = sc.cmn.hand[0].GetComponent<CardScript>();
        //AbilityContainerScript cardBeingUsed = sc.cm.hand[^1].GetComponent<AbilityContainerScript>();
        //sc.cm.lastUsedCard = sc.cm.activatedCard;
        sc.cmn.MoveCard_HandFirstToGraveLast();
        sc.cmn.activatedCard = cardBeingUsed; // record activated card
		cardBeingUsed.GetComponent<CardEventTrigger>().InvokeTryPayCostEvent(); // ! when try to pay cost
		cardBeingUsed.GetComponent<CardEventTrigger>().InvokeActivateEvent(); //! when card used
		foreach (var card in sc.cmn.hand)
		{
			card.GetComponent<CardEventTrigger>().InvokeOnAnyCardActivated();
		}
		//sc.cm.activatedCard = cardBeingUsed; // record activated card
		//foreach (var ability in cardBeingUsed.myAbilities)
		{
			//sc.ams.CardSystem_AdjustAbility(ability, true); // load or activate abilities 
		}
		//sc.cmn.MoveCard_HandLastToGraveFirst(); // move last card in hand to be the first card in grave
		
		AbilityManagerScript.onPlayerSlash?.Invoke(); //! time point
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
		sc.cmn.activatedCard.GetComponent<CardEventTrigger>().InvokeOnSlashFinished();
		AbilityContainerScript cardSentToGrave = sc.cm.activatedCard;
		AbilityManagerScript.beforeUnload?.Invoke(); //! time point
		//foreach (var ability in cardSentToGrave.myAbilities)
		{
			//sc.ams.CardSystem_AdjustAbility(ability, false); // unload ability
		}
		// if this is the last card in hand, load the last card ability list
		if (sc.cmn.hand.Count == 0)
		{
			LingerEffectManager.me.InvokeOnLastHandEvent(); //! last card in hand
			foreach (var ability in sc.ams.lastCardAbility)
			{
				//sc.ams.CardSystem_AdjustAbility(ability, true); // load ability
			}
		}
		foreach (var ability in sc.ams.lastCardAbility)
		{
			//sc.ams.CardSystem_AdjustAbility(ability, false); // unload the last card ability list
		}
		//if (sc.cm.graveyard[^1].GetComponent<AbilityContainerScript>().tempCard)
		{
			//sc.cm.graveyard.RemoveAt(sc.cm.graveyard.Count - 1);
		}
	}
}
