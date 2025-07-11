using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardEventTrigger : MonoBehaviour
{
	public UnityEvent<bool> TryPayCostEvent;
	public void InvokeTryPayCostEvent()
	{
		TryPayCostEvent.Invoke(true);
	}
	
	public UnityEvent<GameObject> EnemyHitEvent;
	public void InvokeEnemyHitEvent(GameObject enemy)
	{
		EnemyHitEvent.Invoke(enemy);
	}
	
	public UnityEvent CardActivateEvent;
	public void InvokeActivateEvent()
	{
		if (TryPayCostEvent.GetPersistentEventCount() > 0 && CardManagerNew.me.costPayed)
		{
			CardActivateEvent.Invoke();
		}
	}
	
	public UnityEvent onToHandEvent;
	public void InvokeOnToHandEvent()
	{
		onToHandEvent.Invoke();
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

	public UnityEvent<GameObject> OnEnemyKilled;
	public void InvokeOnEnemyKilled(GameObject enemyKilled)
	{
		OnEnemyKilled.Invoke(enemyKilled);
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
