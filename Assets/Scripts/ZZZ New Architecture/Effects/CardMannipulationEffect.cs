using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMannipulationEffect : MonoBehaviour
{
        private CardManagerNew _cmn;
        [Header("DRAW")] public int drawAmount;
        [Header("ADD")] public GameObject cardToAddToGrave;
        public GameObject cardToAddToHand;
        
        private void Start()
        {
                _cmn = CardManagerNew.me;
        }
        public void DrawFromGrave()
        {
                for (int i = 0; i < drawAmount; i++)
                {
                        _cmn.MoveCard_GraveLastToHandLast();
                }
        }
        public void AddCurseToGrave()
        {
                _cmn.grave.Add(Instantiate(cardToAddToGrave));
        }
        public void AddCurseToHand()
        {
                _cmn.hand.Insert(0, Instantiate(cardToAddToHand));
                CardUIManager.me.UpdateHandUI();
        }
        public void CheckIfTheresCardToDiscard()
        {
                if (_cmn.hand.Count < 1)
                {
                        _cmn.costPayed = false;
                }
                else
                {
                        _cmn.costPayed = true;
                }
        }
        public void DiscardNextCard()
        {
                _cmn.MoveCardSystem_HandFirstToGraveLast();
                //_cmn.MoveCardSystem_HandIndexToGraveLast(myCardIndex + 1);
        }
        public void DrawAmmoCard()
        {
                foreach (var cardHolder in CardUIManager.me.cardHolders_grave)
                {
                        if (!cardHolder.GetComponent<CardHolderScript>().myCard.GetComponent<AmmoEffect>()) continue;
                        cardToAddToHand = cardHolder;
                        cardToAddToHand.GetComponent<CardHolderScript>().myCard.GetComponent<CardScript>().tempCard = true;
                        _cmn.hand.Add(cardToAddToHand);
                        //_cmn.hand.Insert(0, cardToAddToHand);
                        print("add ["+cardToAddToHand.GetComponent<CardHolderScript>().myCard.GetComponent<CardScript>().cardName+"] to hand");
                        return;
                }
        }
        //public void DiscardNextCard(bool cost)
        //    {
        //            if (_cmn.hand.Count <= 1)
        //            {
        //                    if (cost)
        //                    {
        //                            _cmn.costPayed = false;
        //                    }
        //            }
        //            else
        //            {
        //                    var myCardIndex = GetComponent<CardScript>().myHandIndex;
        //                    _cmn.MoveCardSystem_HandIndexToGraveLast(myCardIndex + 1);
        //                    if (cost)
        //                    {
        //                            _cmn.costPayed = true;
        //                    }
        //            }
        //    }
}