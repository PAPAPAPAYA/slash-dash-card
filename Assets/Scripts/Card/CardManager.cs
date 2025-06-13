using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CardManager : MonoBehaviour
{
	#region SINGLETON
	public static CardManager me;
	void Awake()
	{
		me = this;
	}
	#endregion
	public List<GameObject> cardsToLoad;
	public List<GameObject> hand; // keeps track of cards in hand
	public List<GameObject> graveyard; // keeps track of cards used or discarded
	public bool reloaded;
	public int hand_count_og;
	private CardUIManager cardUIMan;
	public AbilityContainerScript activatedCard;
	public AbilityContainerScript lastUsedCard;
	void Start()
	{
		LoadCardsToHand();
		cardUIMan = CardUIManager.me;
	}
	private void LoadCardsToHand()
	{
		foreach (GameObject card in cardsToLoad)
		{
			hand.Add(Instantiate(card));
		}
		hand_count_og = hand.Count;
	}
	public void MoveCard_HandLastToGraveFirst()
	{
		if (hand.Count > 0)
		{ 
			graveyard.Insert(0, hand[^1]);
			if (graveyard[0].GetComponent<AbilityContainerScript>().myAbilities.Contains(AbilityManagerScript.Abilities.graveExplosion_selfPos))
			{
				AbilityManagerScript.me.MakeExplosion_atPos(PlayerControlScript.me.transform.position);
			}
			hand.RemoveAt(hand.Count - 1);
			cardUIMan.UpdateHandUI();
			cardUIMan.UpdateGraveUI();
			//cardUIMan.UpdateHandMagnets();
			//cardUIMan.UpdateGraveMagnets();
		}
		if (hand.Count == 0)
		{
			reloaded = false;
		}
	}
	public void MoveCard_HandLastToGraveLast()
	{
		if (hand.Count > 0)
		{
			graveyard.Add(hand[^1]);
			hand.RemoveAt(hand.Count - 1);
			cardUIMan.UpdateHandUI();
			cardUIMan.UpdateGraveUI();
		}
		if (hand.Count == 0)
		{
			reloaded = false;
		}
	}
	public void  MoveCard_GraveFirstToHandLast()
	{
		if (graveyard.Count > 0)
		{
			hand.Add(graveyard[0]);
			graveyard.RemoveAt(0);
			cardUIMan.UpdateHandUI();
			cardUIMan.UpdateGraveUI();
			// check if ability activated
			AbilityManagerScript.me.BulletWhenCardDrawn();
		}
	}
	public void CopyCard_randomGraveToHandFirst()
	{
		if (graveyard.Count > 0)
		{
			var copiedCard = Instantiate(graveyard[Random.Range(0, graveyard.Count - 1)]);
			hand.Insert(0, copiedCard);
			hand[0].GetComponent<AbilityContainerScript>().tempCard = true;
			cardUIMan.UpdateHandUI();
			cardUIMan.UpdateGraveUI();
			cardUIMan.UpdateHandMagnets();
			cardUIMan.UpdateGraveMagnets();
			// check if ability activated
			AbilityManagerScript.me.BulletWhenCardDrawn();
		}
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

}
