using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerNew : MonoBehaviour
{
	#region SINGLETON
	public static CardManagerNew me;
	void Awake()
	{
		_cardUIManager = CardUIManager.me;
		me = this;
		foreach (var card in initCards)
		{
			var cardInst = Instantiate(card);
			cardInst.transform.parent = transform;
			hand.Add(cardInst);
			cardInst.SetActive(true);
		}
		_handCountOg = hand.Count;
	}
	#endregion
	public List<GameObject> initCards;
	public List<GameObject> hand;
	public List<GameObject> grave;
	public bool reloaded;
	private CardUIManager _cardUIManager;
	public int _handCountOg;
	public CardScript activatedCard;
	public CardScript lastUsedCard;
	public bool costPayed;
	
	private void Update()
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
	public void UpdateHandCountOG()
	{
		_handCountOg = hand.Count;
	}
	public void MoveAllGraveToHand() // move all cards in grave to hand
	{
		while (grave.Count > 0)
		{
			MoveCard_GraveFirstToHandLast();
		}
	}
	public void MoveCard_HandFirstToGraveLast()
	{
		if (hand.Count > 0)
		{
			if (!hand[0].GetComponent<CardScript>().tempCard)
			{
				hand[0].GetComponent<CardEventTrigger>().InvokeOntoGraveEvent(); //! when send to grave
				grave.Add(hand[0]);
			}
			hand.RemoveAt(0);
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
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
			if (!hand[^1].GetComponent<CardScript>().tempCard)
			{
				hand[^1].GetComponent<CardEventTrigger>().InvokeOntoGraveEvent(); //! when send to grave
				grave.Insert(0, hand[^1]);
			}
			hand.RemoveAt(hand.Count - 1);
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
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
			if (!grave[0].GetComponent<CardScript>().tempCard)
			{
				grave[0].GetComponent<CardEventTrigger>().InvokeOnToHandEvent(); //! when send to grave
				
				hand.Add(grave[0]);
				grave.RemoveAt(0);
			}
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
			LingerEffectManager.me.InvokeOnAnyCardDrawnEvent();
			
			// check if ability activated
			//AbilityManagerScript.me.BulletWhenCardDrawn();
		}
	}
}
