using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffEffect : MonoBehaviour
{
        public int indexToBoost; // + self index
        public int amountToBoost;
        public EnumStorage.BuffCategory thingToBoost;
        public MadnessEffect madnessEffect;
        public CardScript cardScript;
        private GameObject _cardToBoost;
        private CardManagerNew _cmn;
        private int _myIndex;

        private void OnEnable()
        {
                _cmn = CardManagerNew.me;
                _myIndex = GetComponent<CardScript>().myHandIndex;
                _cardToBoost = _cmn.hand[_myIndex + indexToBoost];
        }
        public void BoostHandIndex()
        {
                if (!_cmn.hand[_myIndex + indexToBoost])
                {
                        return;
                }
                switch (thingToBoost)
                {
                        case EnumStorage.BuffCategory.slashDmg:
                                _cardToBoost.GetComponent<CardScript>().dmg += amountToBoost;
                                _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(_cardToBoost.GetComponent<CardScript>().ResetDmg);
                                break;
                        case EnumStorage.BuffCategory.slashWidth:
                                break;
                        case EnumStorage.BuffCategory.becomeMadness:
                                _cardToBoost.GetComponent<CardEventTrigger>().CardActivateEvent.AddListener(madnessEffect.LoadResetMadness);
                                _cardToBoost.GetComponent<CardEventTrigger>().CardActivateEvent.AddListener(madnessEffect.ApplyMadnessToDmg);
                                _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(madnessEffect.AddMadness);
                                _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(cardScript.ResetDmg);
                                _cardToBoost.GetComponent<CardScript>().cardName+=" [Madness]";
                                break;
                        default:
                                break;
                }
        }
}
