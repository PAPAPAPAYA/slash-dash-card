using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerNew : MonoBehaviour
{
	public List<GameObject> hand;
	public List<GameObject> grave;
	public bool reloaded;
	private CardUIManager cardUIMan;
	public static CardManagerNew me;
	private int hand_count_og;
	public CardScript activatedCard;
	void Awake()
	{
		cardUIMan = CardUIManager.me;
		me = this;
	}
	void Start()
	{
		hand_count_og = hand.Count;
	}
	
	void Update()
	{
		if (hand.Count >= hand_count_og)
		{
			reloaded = true;
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
			cardUIMan.UpdateHandUI();
			cardUIMan.UpdateGraveUI();
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
			cardUIMan.UpdateHandUI();
			cardUIMan.UpdateGraveUI();
			LingerEffectManager.me.InvokeOnCardDrawnEvent();
			// check if ability activated
			//AbilityManagerScript.me.BulletWhenCardDrawn();
		}
	}
}
