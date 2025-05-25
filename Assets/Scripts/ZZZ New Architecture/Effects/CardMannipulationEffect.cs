using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMannipulationEffect : MonoBehaviour
{
        public void DiscardNextCard()
        {
                CardManagerNew.me.MoveCard_HandLastToGraveFirst();
        }
}
