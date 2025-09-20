using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LingerEffectManager : MonoBehaviour
{
	#region SINGLETON
	public static LingerEffectManager me;
	void Awake()
	{
		me = this;
	}
	#endregion
	#region EVENTS
	public UnityEvent onLastHand;
	public void InvokeOnLastHandEvent()
	{
		onLastHand.Invoke();
	}
	public UnityEvent onCardDrawn;
	public void InvokeOnAnyCardDrawnEvent()
	{
		onCardDrawn.Invoke();
	}
	public UnityEvent onReloaded;
	public void InvokeOnReloadedEvent()
	{
		onReloaded.Invoke();
	}
	public UnityEvent<GameObject> onPoisonKill;
	public void InvokeOnPoisonKillEvent(GameObject enemyKilled)
	{
		onPoisonKill.Invoke(enemyKilled);
	}
	#endregion
	#region METHODS
	public void ClearAllEvents()
	{
		onLastHand.RemoveAllListeners();
		onCardDrawn.RemoveAllListeners();
		onReloaded.RemoveAllListeners();
	}
	public void ClearAllFlags()
	{
		onKillBecomesOnHit = false;
	}
	#endregion
	#region FLAGS
	public bool onKillBecomesOnHit;
	#endregion
	private void OnEnable()
	{
		//onReloaded.AddListener(ClearAllEvents);
	}
}
