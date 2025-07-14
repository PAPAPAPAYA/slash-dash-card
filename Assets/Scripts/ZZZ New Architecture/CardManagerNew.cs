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
		UpdateIndex();
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
				hand[0].GetComponent<CardEventTrigger>().InvokeOntoGraveEvent(); //! when sent to grave
				grave.Add(hand[0]);
				hand[0].GetComponent<CardScript>().ResetDmg();
			}
			hand.RemoveAt(0);
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
			UpdateIndex();
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
				hand[^1].GetComponent<CardEventTrigger>().InvokeOntoGraveEvent(); //! when sent to grave
				grave.Insert(0, hand[^1]);
			}
			hand.RemoveAt(hand.Count - 1);
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
			UpdateIndex();
		}
		if (hand.Count == 0)
		{
			reloaded = false;
		}
	}
	public void MoveCardSystem_HandIndexToGraveLast(int index)
	{
		if (hand.Count > 0)
		{
			if (!hand[index].GetComponent<CardScript>().tempCard)
			{
				hand[index].GetComponent<CardEventTrigger>().InvokeOnDiscardedEvent(); //! when discarded to grave
				hand[index].GetComponent<CardEventTrigger>().InvokeOntoGraveEvent(); //! when sent to grave
				grave.Add(hand[index]);
				hand[index].GetComponent<CardScript>().ResetDmg();
			}
			hand.RemoveAt(index);
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
			UpdateIndex();
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
				grave[0].GetComponent<CardEventTrigger>().InvokeOnToHandEvent(); //! when drawn to hand
				
				hand.Add(grave[0]);
				grave.RemoveAt(0);
			}
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
			LingerEffectManager.me.InvokeOnAnyCardDrawnEvent();
			UpdateIndex();
		}
	}
	public void MoveCard_GraveLastToHandLast()
	{
		if (grave.Count > 0)
		{
			if (!grave[^1].GetComponent<CardScript>().tempCard)
			{
				grave[^1].GetComponent<CardEventTrigger>().InvokeOnToHandEvent(); //! when drawn to hand
				
				hand.Add(grave[^1]);
				grave.RemoveAt(hand.Count - 1);
			}
			_cardUIManager.UpdateHandUI();
			_cardUIManager.UpdateGraveUI();
			_cardUIManager.UpdateHandMagnets();
			_cardUIManager.UpdateGraveMagnets();
			LingerEffectManager.me.InvokeOnAnyCardDrawnEvent();
			UpdateIndex();
		}
	}
	private void UpdateIndex()
	{
		foreach (var card in hand)
		{
			card.GetComponent<CardScript>().myHandIndex = hand.IndexOf(card);
		}
		foreach (var card in grave)
		{
			card.GetComponent<CardScript>().myGraveIndex = hand.IndexOf(card);
		}
	}
}
