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
        
        private void OnEnable()
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
        public void CompulsiveCardAdd()
        {
                CardObtainManager.me.ShowCardOptions_specifyCard(cardToAddToHand);
                CardObtainManager.me.ShowConfirmButton();
                GameManager.me.currentGameState.gameState = EnumStorage.GameState.upgrade;
                //CardManagerNew.me.MoveAllHandToGrave();
                //CardManagerNew.me.MoveAllGraveToHand();
                CardUIManager.me.AssignMagnets();
                CardUIManager.me.ActivateNextMagnet();
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
                foreach (var card in _cmn.grave)
                {
                        if (!card.GetComponent<AmmoEffect>()) continue;
                        _cmn.DrawCardFromGraveToHandFirst(card);
                        return;
                }
        }
        // todo: need testing
        public void DiscardAllCurse()
        {
                for (var i = _cmn.hand.Count - 1; i >= 0; i--)
                {
                        if (_cmn.hand[i].GetComponent<CardScript>().myTags.Contains(EnumStorage.Tag.curse))
                        {
                                _cmn.MoveCardFromHandToGrave(_cmn.hand[i]);
                        }
                }
                // foreach (var card in _cmn.hand)
                // {
                //         if (!card.GetComponent<CardScript>().myTags.Contains(EnumStorage.Tag.curse)) continue;
                //         _cmn.MoveCardFromHandToGrave(card);
                // }
        }
}