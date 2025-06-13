using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class CardUIManager : MonoBehaviour
{
	[HideInInspector]
	public GameObject cardBeingDragged;
	public GameObject prefabCardPos;
	public GameObject prefab_cardHolder;
	public Color secondToLastColor;
	public float greyScaleCadence;
	private Vector2 centerPos;
	public Vector2 graveStartPos;
	public Vector2 cardArrangeInterval;
	public int handMaxSize; // used to make card holders, need to be greater than hand size at all time (like an object pool)
	public int graveMaxSize;
	public List<GameObject> premade_cardHolders_hand; // stores all the pre-made card holders
	public List<GameObject> premade_cardHolders_grave;
	public List<GameObject> cardHolders_hand; // stores activated hand card holders
	public List<GameObject> cardHolders_grave; // stores activated grave card holders
	#region CARD MAGNETS
	public List<GameObject> premade_cardMagnets_hand;
	public List<GameObject> premade_cardMagnets_grave;
	public List<GameObject> cardMagnets_hand;
	public List<GameObject> cardMagnets_grave;
	#endregion
	#region Magnets
	public GameObject handParent;
	public GameObject graveParent;
	public GameObject magnetPrefab;
	#endregion
	#region SINGLETON
	public static CardUIManager me;
	void Awake()
	{
		me = this;
	}
	#endregion
	
	void Start()
	{
		centerPos = prefab_cardHolder.transform.position;
		MakeCardHolders();
		UpdateHandUI();
		UpdateGraveUI();
		MakeCardMagnets();
		UpdateHandMagnets();
		UpdateGraveMagnets();
	}
	void Update()
	{
		//MakeCurrentCardBigger();
	}
	#region CARD MAGNETS
	private void MakeCardMagnets() // premake card magnets
	{
		for (var i = 0; i < handMaxSize; i++) // make hand card magnets
		{
			var cardMag = Instantiate(magnetPrefab);
			premade_cardMagnets_hand.Add(cardMag);
		}
		for (var i = 0; i < graveMaxSize; i++) // make grave card magnets
		{
			var cardMag = Instantiate(magnetPrefab);
			premade_cardMagnets_grave.Add(cardMag);
		}
		ArrangeHandMagnets();
		ArrangeGraveMagnets();
	}
	public void UpdateHandMagnets()
	{
		cardMagnets_hand.Clear(); // clear hand ui list
		foreach (var cardHolder in premade_cardMagnets_hand) // deactivate all hand ui
		{
			cardHolder.transform.localScale = new Vector3(1,1,1);
			cardHolder.SetActive(false);
		}
		for (var i = 0; i < CardManagerNew.me.hand.Count; i++) // activate and assign cardholder
		{
			premade_cardMagnets_hand[i].SetActive(true);
			premade_cardMagnets_hand[i].GetComponent<CardMagnetScript>().myCard = premade_cardHolders_hand[i];
			cardMagnets_hand.Add(premade_cardMagnets_hand[i]);
		}
		ArrangeHandMagnets();
	}
	private void ArrangeHandMagnets()
	{
		for (var i = cardMagnets_hand.Count - 1; i >= 0; i--)
		{
			cardMagnets_hand[i].transform.position = centerPos - cardArrangeInterval * (cardMagnets_hand.Count - 1 - i);
		}
	}
	public void UpdateGraveMagnets()
	{
		cardMagnets_grave.Clear(); // clear hand ui list
		foreach (var cardHolder in premade_cardMagnets_grave) // deactivate all hand ui
		{
			cardHolder.transform.localScale = new Vector3(1,1,1);
			cardHolder.SetActive(false);
		}
		for (var i = 0; i < CardManagerNew.me.grave.Count; i++) // activate and assign cardholder
		{
			premade_cardMagnets_grave[i].SetActive(true);
			premade_cardMagnets_grave[i].GetComponent<CardMagnetScript>().myCard = premade_cardHolders_grave[i];
			cardMagnets_grave.Add(premade_cardMagnets_grave[i]);
		}
		ArrangeGraveMagnets();
	}
	private void ArrangeGraveMagnets()
	{
		for (var i = cardMagnets_grave.Count - 1; i >= 0; i--)
		{
			cardMagnets_grave[i].transform.position = graveStartPos + cardArrangeInterval * (i + 1);
		}
	}
	#endregion
	private void MakeCurrentCardBigger()
	{
		if (CardManagerNew.me.reloaded)
		{
			if (cardHolders_hand.Count > 0)
				cardHolders_hand[^1].transform.localScale = new Vector3(1.15f, 1.15f, 1);
		}
	}
	#region CARD HOLDERS
	private void MakeCardHolders() // pre-make card holders and only activate and deactivate based on the hand and graveyard
	{
		for (int i = 0; i < handMaxSize; i++) // make hand card holders
		{
			var cardHolder = Instantiate(prefab_cardHolder);
			premade_cardHolders_hand.Add(cardHolder);
		}
		for (int i = 0; i < graveMaxSize; i++) // make grave card holders
		{
			var cardHolder = Instantiate(prefab_cardHolder);
			premade_cardHolders_grave.Add(cardHolder);
		}
	}
	public void UpdateHandUI()
	{
		cardHolders_hand.Clear(); // clear hand ui list
		foreach (var cardHolder in premade_cardHolders_hand) // deactivate all hand ui
		{
			cardHolder.transform.localScale = new Vector3(1,1,1);
			cardHolder.SetActive(false);
		}
		for (int i = 0; i < CardManagerNew.me.hand.Count; i++) // activate and change text based on hand
		{
			premade_cardHolders_hand[i].SetActive(true);
			premade_cardHolders_hand[i].GetComponent<CardHolderScript>().inHand = true;
			cardHolders_hand.Add(premade_cardHolders_hand[i]);
			premade_cardHolders_hand[i].GetComponentInChildren<TextMeshPro>().text = CardManagerNew.me.hand[i].GetComponent<CardScript>().cardName;
		}
		ArrangeHandUI();
	}
	private void ArrangeHandUI()
	{
		for (var i = cardHolders_hand.Count - 1; i >= 0; i--)
		{
			cardHolders_hand[i].transform.position = centerPos - cardArrangeInterval * (cardHolders_hand.Count - 1 - i);
		}
	}
	public void UpdateGraveUI()
	{
		cardHolders_grave.Clear(); // clear grave ui list
		foreach (var cardHolder in premade_cardHolders_grave) // deactivate all grave ui
		{
			cardHolder.SetActive(false);
		}
		for (var i = 0; i < CardManagerNew.me.grave.Count; i++) // activate and change text based on grave
		{
			premade_cardHolders_grave[i].SetActive(true);
			cardHolders_grave.Add(premade_cardHolders_grave[i]);
			premade_cardHolders_grave[i].GetComponentInChildren<TextMeshPro>().text = CardManagerNew.me.grave[i].GetComponent<CardScript>().cardName;
		}
		ArrangeGraveUI();
	}
	private void ArrangeGraveUI()
	{
		for (var i = 0; i < cardHolders_grave.Count; i++)
		{
			cardHolders_grave[i].transform.position = graveStartPos + cardArrangeInterval * (i + 1);
		}
	}
	#endregion
}
