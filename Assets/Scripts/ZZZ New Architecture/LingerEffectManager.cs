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
	public UnityEvent OnLastHand;
	public void InvokeOnLastHandEvent()
	{
		OnLastHand.Invoke();
	}
	public UnityEvent OnCardDrawn;
	public void InvokeOnCardDrawnEvent()
	{
		OnCardDrawn.Invoke();
	}
}
