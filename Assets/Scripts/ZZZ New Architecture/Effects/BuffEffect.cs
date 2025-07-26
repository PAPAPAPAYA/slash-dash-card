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
        public IntVaribaleSO madnessEffectCounter;
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
                                _cardToBoost.AddComponent<MadnessEffect>();
                                _cardToBoost.GetComponent<MadnessEffect>().madnessCounterRef = madnessEffectCounter;
                                _cardToBoost.GetComponent<CardEventTrigger>().CardActivateEvent.AddListener(_cardToBoost.GetComponent<MadnessEffect>().LoadResetMadness);
                                _cardToBoost.GetComponent<CardEventTrigger>().CardActivateEvent.AddListener(_cardToBoost.GetComponent<MadnessEffect>().ApplyMadnessToDmg);
                                _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(_cardToBoost.GetComponent<MadnessEffect>().AddMadness);
                                _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(_cardToBoost.GetComponent<CardScript>().ResetDmg);
                                _cardToBoost.GetComponent<CardScript>().cardName ="[Madness] "+ _cardToBoost.GetComponent<CardScript>().cardName;
                                break;
                        default:
                                break;
                }
        }
}
