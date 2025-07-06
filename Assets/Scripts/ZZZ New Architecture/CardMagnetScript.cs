using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMagnetScript : MonoBehaviour
{
        public GameObject myCardHolder;
        public float attractDis;
        public bool enlarge;
        private GameObject _cardHolderBeingDragged;
        private CardUIManager _cardUIManager;
        public bool imOptionMagnet;
        public bool debugMe;
        private void Start()
        {
                _cardUIManager = CardUIManager.me.GetComponent<CardUIManager>();
        }
        private void Update()
        {
                if (debugMe)
                {
                        if (!myCardHolder)
                        {
                                print(gameObject.name + ": no card holder");
                        }
                        //print("my index: " + _cardUIManager.cardMagnets_hand.IndexOf(gameObject) + "; my card holder: " + myCardHolder);
                        //print("space in hand: "+HandHasSpace());
                }
                if (!_cardHolderBeingDragged)
                {
                        _cardHolderBeingDragged = CardUIManager.me.cardHolderBeingDragged;
                }
                
                if (_cardHolderBeingDragged) // if a card is being dragged
                {
                        if (!myCardHolder) // if this magnet isn't assigned a card
                        {
                                if (Vector3.Distance(_cardHolderBeingDragged.transform.position, transform.position) < attractDis) // if the dragged card in my range
                                {
                                        //if (_cardHolderBeingDragged.GetComponent<CardHolderScript>().myMagnet.GetComponent<CardMagnetScript>().imOptionMagnet &&
                                        //    HandHasSpace())
                                        {
                                                myCardHolder = _cardHolderBeingDragged.gameObject; // assign the dragged card to me
                                                if (myCardHolder.GetComponent<CardHolderScript>().myMagnet) // if the dragged card has an original magnet assigned
                                                {
                                                        myCardHolder.GetComponent<CardHolderScript>().myMagnet.GetComponent<CardMagnetScript>().myCardHolder = null; // clear the og magnet's assignment
                                                }
                                                _cardHolderBeingDragged.GetComponent<CardHolderScript>().myMagnet = gameObject; // and assign me to dragged card
                                                _cardHolderBeingDragged.GetComponent<CardHolderScript>().inHand = true; // set inHand, so that confirm button won't delete it
                                                
                                                // update card ui manager's cardholder hand
                                                if (imOptionMagnet) // if im an option magnet, remove card and card holder from lists
                                                {
                                                        if (_cardUIManager.cardHolders_hand.Contains(_cardHolderBeingDragged))
                                                        {
                                                                _cardUIManager.cardHolders_hand.Remove(_cardHolderBeingDragged);
                                                                CardManagerNew.me.hand.Remove(_cardHolderBeingDragged.GetComponent<CardHolderScript>().myCard);
                                                        }
                                                }
                                                else // if im a hand magnet, add card and card holder to lists
                                                {
                                                        if (!_cardUIManager.cardHolders_hand.Contains(_cardHolderBeingDragged))
                                                        {
                                                                _cardUIManager.cardHolders_hand.Add(_cardHolderBeingDragged);
                                                                //var myMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(gameObject);
                                                                CardManagerNew.me.hand.Add(_cardHolderBeingDragged.GetComponent<CardHolderScript>().myCard);
                                                        }
                                                }
                                        }
                                }
                        }
                        else // if this magnet is already assigned a card
                        {
                                if (Vector3.Distance(_cardHolderBeingDragged.transform.position,
                                            transform.position) < attractDis &&
                                    _cardHolderBeingDragged != myCardHolder &&
                                    !imOptionMagnet)
                                {
                                        if (_cardHolderBeingDragged.transform.position.x > transform.position.x)
                                        {
                                                // shift left
                                                {
                                                        ShiftLeft(gameObject);
                                                }
                                        }
                                        else
                                        {
                                                // shift right
                                                {
                                                        ShiftRight(gameObject);
                                                }
                                        }
                                }
                        }
                }
                if (myCardHolder && myCardHolder != _cardHolderBeingDragged && myCardHolder.GetComponent<CardHolderScript>().myMagnet == gameObject)
                {
                        myCardHolder.transform.position = transform.position;
                }
                if (enlarge &&
                    myCardHolder &&
                    Vector2.Distance(myCardHolder.transform.position, transform.position) < 1f)
                {
                        myCardHolder.transform.localScale = new Vector3(2f, 2f, 2f);
                }
                if (Input.GetMouseButtonUp(0) && _cardHolderBeingDragged)
                {
                        _cardHolderBeingDragged = null;
                        
                        if (!myCardHolder) // grab my card holder from card holder list
                        {
                                foreach (var cardHolder in _cardUIManager.cardHolders_hand)
                                {
                                        if (cardHolder.GetComponent<CardHolderScript>().myMagnet == gameObject)
                                        {
                                                myCardHolder = cardHolder;
                                        }
                                }
                        }
                        
                }
                Debug.DrawLine(transform.position, transform.position + Vector3.up * attractDis, Color.red);
        }
        private void ShiftLeft(GameObject cardMagnet)
        {
                var newInsertedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(cardMagnet);
                var startIndex = _cardUIManager.cardMagnets_hand.Count - 1;
                var cardBeingDraggedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(_cardHolderBeingDragged
                        .GetComponent<CardHolderScript>()
                        .myMagnet);
                if (!_cardUIManager.HandHasSpace() &&
                    cardBeingDraggedMagnetIndex < newInsertedMagnetIndex)
                {
                        return;
                }
                if (cardBeingDraggedMagnetIndex > newInsertedMagnetIndex)
                {
                        startIndex = cardBeingDraggedMagnetIndex;
                }
                for (var i = startIndex; i > newInsertedMagnetIndex; i--)
                {
                        var lastMagnet = _cardUIManager.cardMagnets_hand[i - 1].GetComponent<CardMagnetScript>();
                        var currentMagnet = _cardUIManager.cardMagnets_hand[i].GetComponent<CardMagnetScript>();
                        if (lastMagnet.myCardHolder)
                        {
                                currentMagnet.myCardHolder = lastMagnet.myCardHolder;
                                currentMagnet.myCardHolder.GetComponent<CardHolderScript>().myMagnet =
                                        currentMagnet.gameObject;
                                lastMagnet.myCardHolder = null;
                        }
                }
        }
        private void ShiftRight(GameObject cardMagnet)
        {
                var newInsertedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(cardMagnet);
                var startIndex = 0;
                var cardBeingDraggedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(_cardHolderBeingDragged
                        .GetComponent<CardHolderScript>()
                        .myMagnet);
                if (!_cardUIManager.HandHasSpace() &&
                    cardBeingDraggedMagnetIndex > newInsertedMagnetIndex)
                {
                        return;
                }
                if (cardBeingDraggedMagnetIndex < newInsertedMagnetIndex)
                {
                        startIndex = cardBeingDraggedMagnetIndex;
                }
                for (var i = startIndex; i < newInsertedMagnetIndex; i++)
                {
                        var nextMagnet = _cardUIManager.cardMagnets_hand[i + 1].GetComponent<CardMagnetScript>();
                        var currentMagnet = _cardUIManager.cardMagnets_hand[i].GetComponent<CardMagnetScript>();
                        if (nextMagnet.myCardHolder)
                        {
                                currentMagnet.myCardHolder = nextMagnet.myCardHolder;
                                currentMagnet.myCardHolder.GetComponent<CardHolderScript>().myMagnet =
                                        currentMagnet.gameObject;
                                nextMagnet.myCardHolder = null;
                        }
                }
        }
}