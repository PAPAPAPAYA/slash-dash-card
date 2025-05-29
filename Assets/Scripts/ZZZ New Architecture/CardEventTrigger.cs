using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardEventTrigger : MonoBehaviour
{
	public UnityEvent TryPayCostEvent;
	public void InvokeTryPayCostEvent()
	{
		TryPayCostEvent.Invoke();
	}
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

	public UnityEvent OnEnemyKilled;

	public void InvokeOnEnemyKilled()
	{
		OnEnemyKilled.Invoke();
	}
	public UnityEvent OnSlashFinished;
	public void InvokeOnSlashFinished()
	{
		OnSlashFinished.Invoke();
	}
	public UnityEvent OnAnyCardActivated;
	public void InvokeOnAnyCardActivated()
	{
		OnAnyCardActivated.Invoke();
	}
}
