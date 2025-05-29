using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerNew : MonoBehaviour
{
	public List<GameObject> hand;
	public List<GameObject> grave;
	public bool reloaded;
	private CardUIManager _cardUIManager;
	public static CardManagerNew me;
	private int _handCountOg;
	public CardScript activatedCard;
	public CardScript lastUsedCard;
	void Awake()
	{
		_cardUIManager = CardUIManager.me;
		me = this;
	}
	void Start()
	{
		_handCountOg = hand.Count;
	}
	
	void Update()
	{
		if (hand.Count >= _handCountOg && !reloaded)
		{
			reloaded = true;
			LingerEffectManager.me.InvokeOnReloadedEvent();
		}
		if (hand.Count == 0)
		{
			reloaded = false;
		}
	}

	public void MoveCard_HandLastToGraveFirst()
	{
		if (hand.Count > 0)
		{
			hand[^1].GetComponent<CardEventTrigger>().InvokeOntoGraveEvent(); //! when send to grave
			grave.Insert(0, hand[^1]);
			hand.RemoveAt(hand.Count - 1);
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
		}
		if (hand.Count == 0)
		{
			reloaded = false;
		}
	}
	public void MoveCard_GraveFirstToHandLast()
	{
		if (grave.Count > 0)
		{
			hand.Add(grave[0]);
			grave.RemoveAt(0);
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			LingerEffectManager.me.InvokeOnCardDrawnEvent();
			// check if ability activated
			//AbilityManagerScript.me.BulletWhenCardDrawn();
		}
	}
}
