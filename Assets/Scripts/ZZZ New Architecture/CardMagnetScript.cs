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
        private void Start()
        {
                _cardUIManager = CardUIManager.me.GetComponent<CardUIManager>();
        }
        private void Update()
        {
                if (!_cardBeingDragged)
                {
                        _cardBeingDragged = CardUIManager.me.cardBeingDragged;
                }
                if (Input.GetMouseButtonUp(0))
                {
                        _cardBeingDragged = null;
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
                                        // check if newly assigned card not in card ui manager's lists, if not, add
                                        if (!_cardUIManager.cardHolders_hand.Contains(_cardBeingDragged))
                                        {
                                                _cardUIManager.cardHolders_hand.Add(_cardBeingDragged);
                                        }
                                }
                        }
                        else // if this magnet is already assigned a card
                        {
                                if (Vector3.Distance(_cardBeingDragged.transform.position,
                                            transform.position) < attractDis &&
                                    _cardBeingDragged != myCardHolder)
                                {
                                        if (_cardBeingDragged.transform.position.x > transform.position.x)
                                        {
                                                // shift left
                                                //ShiftLeftOne(_cardBeingDragged);
                                                ShiftLeft(gameObject);
                                        }
                                        else
                                        {
                                                ShiftRight(gameObject);
                                        }
                                }
                        }
                }
                if (myCardHolder && myCardHolder != _cardBeingDragged)
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
        private void ShiftLeft(GameObject cardMagnet)
        {
                var newInsertedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(cardMagnet);
                for (var i = _cardUIManager.cardMagnets_hand.Count - 1; i > newInsertedMagnetIndex; i--)
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
        private void ShiftLeftOne(GameObject cardHolder)
        {
                if (_cardUIManager.cardMagnets_hand.IndexOf(gameObject) < _cardUIManager.cardMagnets_hand.Count - 1)
                {
                        var leftMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(gameObject) + 1;
                        if (!_cardUIManager.cardMagnets_hand[leftMagnetIndex].GetComponent<CardMagnetScript>()
                                    .myCardHolder)
                        {
                                myCardHolder.GetComponent<CardHolderScript>().myMagnet =
                                        _cardUIManager.cardMagnets_hand[leftMagnetIndex];
                                _cardUIManager.cardMagnets_hand[leftMagnetIndex].GetComponent<CardMagnetScript>().myCardHolder = myCardHolder;
                                myCardHolder = cardHolder;
                                cardHolder.GetComponent<CardHolderScript>().myMagnet.GetComponent<CardMagnetScript>().myCardHolder =
                                        null;
                        }
                }
        }
        private void ShiftRight(GameObject cardMagnet)
        {
                var newInsertedMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(cardMagnet);
                for (var i = 0; i < newInsertedMagnetIndex; i++)
                {
                        var lastMagnet = _cardUIManager.cardMagnets_hand[i + 1].GetComponent<CardMagnetScript>();
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
        private void ShiftRightOne(GameObject cardHolder)
        {
                if (_cardUIManager.cardMagnets_hand.IndexOf(gameObject) > 0)
                {
                        var rightMagnetIndex = _cardUIManager.cardMagnets_hand.IndexOf(gameObject) - 1;
                        if (!_cardUIManager.cardMagnets_hand[rightMagnetIndex].GetComponent<CardMagnetScript>()
                                    .myCardHolder)
                        {
                                myCardHolder.GetComponent<CardHolderScript>().myMagnet =
                                        _cardUIManager.cardMagnets_hand[rightMagnetIndex];
                                _cardUIManager.cardMagnets_hand[rightMagnetIndex].GetComponent<CardMagnetScript>().myCardHolder = myCardHolder;
                                myCardHolder = cardHolder;
                                cardHolder.GetComponent<CardHolderScript>().myMagnet.GetComponent<CardMagnetScript>().myCardHolder =
                                        null;
                        }
                }
        }
}