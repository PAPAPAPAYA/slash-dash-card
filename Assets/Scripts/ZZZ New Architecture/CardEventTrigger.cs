using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardEventTrigger : MonoBehaviour
{
	public UnityEvent EnemyHitEvent;
	public void InvokeEnemyHitEvent()
	{
		EnemyHitEvent.Invoke();
	}
	public UnityEvent CardActivateEvent;
	public void InvokeActivateEvent()
	{
		CardActivateEvent.Invoke();
	}
	public UnityEvent OnToGraveEvent;
	public void InvokeOntoGraveEvent()
	{
		OnToGraveEvent.Invoke();
	}
	public UnityEvent OnDmgCalculation;
	public void InvokeOnDmgCalculation()
	{
		OnDmgCalculation.Invoke();
	}
	public UnityEvent OnSlashFinished;
	public void InvokeOnSlashFinished()
	{
		OnSlashFinished.Invoke();
	}
}
