using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMannipulationEffect : MonoBehaviour
{
        [Header("DRAW")] public int drawAmount;
        [Header("ADD")] public GameObject cardToAddToGrave;
        public GameObject cardToAddToHand;

        public void DrawFromGrave()
        {
                for (int i = 0; i < drawAmount; i++)
                {
                        CardManagerNew.me.MoveCard_GraveLastToHandLast();
                }
        }
        public void AddCurseToGrave()
        {
                CardManagerNew.me.grave.Add(Instantiate(cardToAddToGrave));
        }
        public void AddCurseToHand()
        {
                CardManagerNew.me.hand.Insert(0, Instantiate(cardToAddToHand));
                CardUIManager.me.UpdateHandUI();
        }
        public void CheckIfTheresCardToDiscard()
        {
                if (CardManagerNew.me.hand.Count < 1)
                {
                        CardManagerNew.me.costPayed = false;
                }
                else
                {
                        CardManagerNew.me.costPayed = true;
                }
        }
        public void DiscardNextCard()
        {
                CardManagerNew.me.MoveCardSystem_HandFirstToGraveLast();
                //CardManagerNew.me.MoveCardSystem_HandIndexToGraveLast(myCardIndex + 1);
        }
        public void DrawAmmoCard()
        {
                foreach (var cardHolder in CardUIManager.me.cardHolders_grave)
                {
                        if (!cardHolder.GetComponent<CardHolderScript>().myCard.GetComponent<AmmoEffect>()) continue;
                        cardToAddToHand = cardHolder;
                        print("add "+cardToAddToHand.GetComponent<CardHolderScript>().myCard.GetComponent<CardScript>().cardName+" to hand");
                        return;
                }
        }
        //public void DiscardNextCard(bool cost)
        //    {
        //            if (CardManagerNew.me.hand.Count <= 1)
        //            {
        //                    if (cost)
        //                    {
        //                            CardManagerNew.me.costPayed = false;
        //                    }
        //            }
        //            else
        //            {
        //                    var myCardIndex = GetComponent<CardScript>().myHandIndex;
        //                    CardManagerNew.me.MoveCardSystem_HandIndexToGraveLast(myCardIndex + 1);
        //                    if (cost)
        //                    {
        //                            CardManagerNew.me.costPayed = true;
        //                    }
        //            }
        //    }
}