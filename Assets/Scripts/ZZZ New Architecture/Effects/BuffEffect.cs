using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : MonoBehaviour
{
        public int indexToBoost; // + self index
        public int amountToBoost;
        public EnumStorage.BuffCategory thingToBoost;

        public void BoostHandIndex()
        {
                var cmn = CardManagerNew.me;
                var myIndex = GetComponent<CardScript>().myHandIndex;
                switch (thingToBoost)
                {
                        case EnumStorage.BuffCategory.slashDmg:
                                cmn.hand[myIndex + indexToBoost].GetComponent<CardScript>().dmg += amountToBoost;
                                break;
                        case EnumStorage.BuffCategory.slashWidth:
                                break;
                        default:
                                break;
                }
        }
}
