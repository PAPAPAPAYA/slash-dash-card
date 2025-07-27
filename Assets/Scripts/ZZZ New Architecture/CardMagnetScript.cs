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
                _cardUIManager = CardUIManager.me.GetComponent<CardUIManager>(); // get card ui manager ref
        }
        private void Update()
        {
                // tick in editor to print
                if (debugMe)
                {
                        if (!myCardHolder)
                        {
                                print(gameObject.name + ": no card holder");
                        }
                }
                // if doesn't have card holder being dragged's ref, get it from card ui manager
                if (!_cardHolderBeingDragged)
                {
                        _cardHolderBeingDragged = _cardUIManager.cardHolderBeingDragged;
                }
                
                if (_cardHolderBeingDragged) // if a card is being dragged
                {
                        if (!myCardHolder) // if this magnet isn't assigned a card
                        {
                                if (Vector3.Distance(_cardHolderBeingDragged.transform.position, transform.position) < attractDis) // if the dragged card is in my range
                                {
                                        myCardHolder = _cardHolderBeingDragged; // assign the dragged card to me
                                        if (myCardHolder.GetComponent<CardHolderScript>().myMagnet) // if the dragged card has an original magnet assigned
                                        {
                                                myCardHolder.GetComponent<CardHolderScript>().myMagnet.GetComponent<CardMagnetScript>().myCardHolder = null; // clear the og magnet's assignment
                                        }
                                        _cardHolderBeingDragged.GetComponent<CardHolderScript>().myMagnet = gameObject; // and assign me to dragged card
                                        _cardHolderBeingDragged.GetComponent<CardHolderScript>().inHand = true; // set inHand, so that confirm button won't delete it
                                                
                                        // update card ui manager's cardholder hand list
                                        if (imOptionMagnet) // if im an option magnet, remove card and its card holder from card ui manager's hand list and card manager new's hand list
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
                                                        CardManagerNew.me.hand.Add(_cardHolderBeingDragged.GetComponent<CardHolderScript>().myCard);
                                                }
                                        }
                                }
                        }
                        else // if this magnet is already assigned a card holder
                        {
                                if (Vector3.Distance(_cardHolderBeingDragged.transform.position,
                                            transform.position) < attractDis && // if card holder is in my range
                                    _cardHolderBeingDragged != myCardHolder && // if the card being dragged isn't assigned to me
                                    !imOptionMagnet) // if im not an option magnet
                                {
                                        if (_cardHolderBeingDragged.transform.position.x > transform.position.x)
                                        {
                                                // shift left
                                                {
                                                        ShiftLeft();
                                                }
                                        }
                                        else
                                        {
                                                // shift right
                                                {
                                                        ShiftRight();
                                                }
                                        }
                                }
                        }
                }
                // set my card holder's pos to my pos
                if (myCardHolder && myCardHolder != _cardHolderBeingDragged && myCardHolder.GetComponent<CardHolderScript>().myMagnet == gameObject)
                {
                        myCardHolder.transform.position = transform.position;
                }
                // enlarge my card holder
                if (enlarge &&
                    myCardHolder &&
                    Vector2.Distance(myCardHolder.transform.position, transform.position) < 1f)
                {
                        myCardHolder.transform.localScale = new Vector3(2f, 2f, 2f);
                }
                // if mouse left button is lifted, and there is a card being dragged
                if (Input.GetMouseButtonUp(0) && _cardHolderBeingDragged)
                {
                        _cardHolderBeingDragged = null; // clear card being dragged
                        
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
        private void ShiftLeft()
        {
                var newInsertedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(gameObject); // get the index of myself (the magnet triggering a shift)
                var startIndex = _cardUIManager.cardMagnets_hand.Count - 1; // set the start index as the last hand magnet index
                var cardBeingDraggedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(_cardHolderBeingDragged
                        .GetComponent<CardHolderScript>()
                        .myMagnet); // get the dragged card's current assigned magnet index
                // if hand doesn't have empty space, and dragged card's magnet is on the right of me, don't shift left
                if (!_cardUIManager.HandHasSpace() &&
                    cardBeingDraggedMagnetIndex < newInsertedMagnetIndex)
                {
                        return;
                }
                // if dragged card's magnet is on the left of me, set the start index as the dragged card magnet index (in this case, start shifting left from the dragged card magnet)
                if (cardBeingDraggedMagnetIndex > newInsertedMagnetIndex)
                {
                        startIndex = cardBeingDraggedMagnetIndex;
                }
                for (var i = startIndex; i > newInsertedMagnetIndex; i--) // start from start index, work from left to right
                {
                        // assign right magnet's card holder to left magnet, and clear right magnet's card holder
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
        private void ShiftRight()
        {
                var newInsertedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(gameObject);
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