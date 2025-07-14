using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardEventTrigger : MonoBehaviour
{
	public UnityEvent<bool> TryPayCostEvent;
	public void InvokeTryPayCostEvent() // 尝试支付cost
	{
		TryPayCostEvent.Invoke(true);
	}
	
	public UnityEvent<GameObject> EnemyHitEvent; // 击中敌人
	public void InvokeEnemyHitEvent(GameObject enemy)
	{
		EnemyHitEvent.Invoke(enemy);
	}
	
	public UnityEvent CardActivateEvent;
	public void InvokeActivateEvent() // 发动
	{
		if (TryPayCostEvent.GetPersistentEventCount() > 0 && CardManagerNew.me.costPayed)
		{
			CardActivateEvent.Invoke();
		}
	}
	
	public UnityEvent onToHandEvent;
	public void InvokeOnToHandEvent() // 上手
	{
		onToHandEvent.Invoke();
	}
	
	public UnityEvent OnToGraveEvent; // 送墓
	public void InvokeOntoGraveEvent()
	{
		OnToGraveEvent.Invoke();
	}

	public UnityEvent OnDiscardedEvent; // 丢弃
	public void InvokeOnDiscardedEvent()
	{
		OnDiscardedEvent.Invoke();
	}
	
	public UnityEvent OnDmgCalculation; // 伤害计算时
	public void InvokeOnDmgCalculation()
	{
		OnDmgCalculation.Invoke();
	}

	public UnityEvent<GameObject> OnEnemyKilled; // 击杀敌人时
	public void InvokeOnEnemyKilled(GameObject enemyKilled)
	{
		OnEnemyKilled.Invoke(enemyKilled);
	}
	
	public UnityEvent OnSlashFinished; // 攻击完成时
	public void InvokeOnSlashFinished()
	{
		OnSlashFinished.Invoke();
	}
	
	public UnityEvent OnAnyCardActivated; // 任意卡发动时
	public void InvokeOnAnyCardActivated()
	{
		OnAnyCardActivated.Invoke();
	}
}
