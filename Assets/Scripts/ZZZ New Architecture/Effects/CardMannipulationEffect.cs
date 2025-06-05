using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMannipulationEffect : MonoBehaviour
{
        public GameObject cardToAddToGrave;
        public GameObject cardToAddToHand;

        public void AddCurseToGrave() // curse card deal 1 dmg to self, when used, burn
        {
                CardManagerNew.me.grave.Add(Instantiate(cardToAddToGrave));
        }

        public void AddCurseToHand()
        {
                CardManagerNew.me.hand.Insert(0, Instantiate(cardToAddToHand));
                CardUIManager.me.UpdateHandUI();
        }
        public void DiscardNextCard(bool cost)
        {
                if (CardManagerNew.me.hand.Count <= 1)
                {
                        if (cost)
                        {
                                CardManagerNew.me.costPayed = false;
                        }
                }
                else
                {
                        CardManagerNew.me.MoveCard_HandLastToGraveFirst();
                        if (cost)
                        {
                                CardManagerNew.me.costPayed = true;
                        }
                }
        }
}
