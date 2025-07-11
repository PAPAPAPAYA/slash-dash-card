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
	private void OnEnable()
	{
		onReloaded.AddListener(ClearAllEvents);
	}
	private void ClearAllEvents()
	{
		onLastHand.RemoveAllListeners();
		onCardDrawn.RemoveAllListeners();
		onReloaded.RemoveAllListeners();
	}
}
