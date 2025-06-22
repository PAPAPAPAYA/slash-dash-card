using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMagnetScript : MonoBehaviour
{
        public GameObject myCardHolder;
        public float attractDis;
        public bool enlarge;
        private GameObject _cardBeingDragged;
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
                        //print("my index: " + _cardUIManager.cardMagnets_hand.IndexOf(gameObject) + "; my card holder: " + myCardHolder);
                        //print("space in hand: "+HandHasSpace());
                }
                if (!_cardBeingDragged)
                {
                        _cardBeingDragged = CardUIManager.me.cardBeingDragged;
                }
                if (Input.GetMouseButtonUp(0))
                {
                        _cardBeingDragged = null;
                        if (!myCardHolder) // grab my card holder from card holder list
                        {
                                foreach (var cardHolder in _cardUIManager.cardHolders_hand)
                                {
                                        if (cardHolder.GetComponent<CardHolderScript>().myMagnet == gameObject)
                                        {
                                                myCardHolder = cardHolder;
                                        }
                                }
                                if (HandHasSpace())
                                {
                                        CardUIManager.me.AutoShiftRight();
                                }
                        }
                        
                }
                if (_cardBeingDragged) // if a card is being dragged
                {
                        if (!myCardHolder) // if this magnet isn't assigned a card
                        {
                                if (Vector3.Distance(_cardBeingDragged.transform.position, transform.position) < attractDis) // if the dragged card in my range
                                {
                                        myCardHolder = _cardBeingDragged.gameObject; // assign the dragged card to me
                                        if (myCardHolder.GetComponent<CardHolderScript>().myMagnet) // if the dragged card has an original magnet assigned
                                        {
                                                myCardHolder.GetComponent<CardHolderScript>().myMagnet.GetComponent<CardMagnetScript>().myCardHolder = null; // clear the og magnet's assignment
                                        }
                                        _cardBeingDragged.GetComponent<CardHolderScript>().myMagnet = gameObject; // and assign me to dragged card
                                        _cardBeingDragged.GetComponent<CardHolderScript>().inHand = true; // set inHand, so that confirm button won't delete it
                                        
                                        // update card ui manager's cardholder hand list
                                        if (imOptionMagnet)
                                        {
                                                if (_cardUIManager.cardHolders_hand.Contains(_cardBeingDragged))
                                                {
                                                        _cardUIManager.cardHolders_hand.Remove(_cardBeingDragged);
                                                        CardManagerNew.me.hand.Remove(_cardBeingDragged.GetComponent<CardHolderScript>().myCard);
                                                }
                                        }
                                        else
                                        {
                                                if (!_cardUIManager.cardHolders_hand.Contains(_cardBeingDragged))
                                                {
                                                        _cardUIManager.cardHolders_hand.Add(_cardBeingDragged);
                                                        CardManagerNew.me.hand.Add(_cardBeingDragged.GetComponent<CardHolderScript>().myCard);
                                                }
                                        }
                                }
                        }
                        else // if this magnet is already assigned a card
                        {
                                if (Vector3.Distance(_cardBeingDragged.transform.position,
                                            transform.position) < attractDis &&
                                    _cardBeingDragged != myCardHolder &&
                                    !imOptionMagnet)
                                {
                                        if (_cardBeingDragged.transform.position.x > transform.position.x)
                                        {
                                                // shift left
                                                //if (HandHasSpace())
                                                {
                                                        ShiftLeft(gameObject);
                                                }
                                        }
                                        else
                                        {
                                                //if (HandHasSpace())
                                                {
                                                        ShiftRight(gameObject);
                                                }
                                        }
                                }
                        }
                }
                if (myCardHolder && myCardHolder != _cardBeingDragged && myCardHolder.GetComponent<CardHolderScript>().myMagnet == gameObject)
                {
                        myCardHolder.transform.position = transform.position;
                }
                if (enlarge &&
                    myCardHolder &&
                    Vector2.Distance(myCardHolder.transform.position, transform.position) < 1f)
                {
                        myCardHolder.transform.localScale = new Vector3(2f, 2f, 2f);
                }
                Debug.DrawLine(transform.position, transform.position + Vector3.up * attractDis, Color.red);
        }
        private bool HandHasSpace()
        {
                var yesSpace = false;
                //foreach (var magnet in _cardUIManager.cardMagnets_hand)
                {
                        //foreach (var cardHolder in _cardUIManager.cardHolders_hand)
                        {
                                //var magnetScript = magnet.GetComponent<CardMagnetScript>();
                                //var cardHolderScript = cardHolder.GetComponent<CardHolderScript>();
                                if (_cardUIManager.cardHolders_hand.Count < _cardUIManager.cardMagnets_hand.Count)
                                {
                                        yesSpace = true;
                                        //break;
                                }
                        }
                }
                return yesSpace;
        }
        private void ShiftLeft(GameObject cardMagnet)
        {
                var newInsertedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(cardMagnet);
                var startIndex = _cardUIManager.cardMagnets_hand.Count - 1;
                var cardBeingDraggedIndex = _cardUIManager.cardMagnets_hand.IndexOf(_cardBeingDragged
                        .GetComponent<CardHolderScript>()
                        .myMagnet);
                if (cardBeingDraggedIndex > newInsertedMagnetIndex)
                {
                        startIndex = cardBeingDraggedIndex;
                }
                if (!HandHasSpace())
                {
                        if (cardBeingDraggedIndex > newInsertedMagnetIndex)
                        {
                                startIndex = cardBeingDraggedIndex;
                        }
                        else
                        {
                                return;
                        }
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
                var startIndex = newInsertedMagnetIndex;
                var cardBeingDraggedIndex = _cardUIManager.cardMagnets_hand.IndexOf(_cardBeingDragged
                        .GetComponent<CardHolderScript>()
                        .myMagnet);
                if (!HandHasSpace())
                {
                        if (cardBeingDraggedIndex < newInsertedMagnetIndex)
                        {
                                startIndex = cardBeingDraggedIndex;
                        }
                        else
                        {
                                return;
                        }
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