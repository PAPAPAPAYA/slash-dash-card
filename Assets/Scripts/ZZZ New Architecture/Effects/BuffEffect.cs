using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffEffect : MonoBehaviour
{
        public int indexToBoost = 1; // + self index
        public int amountToBoost;
        public EnumStorage.BuffCategory thingToBoost;
        public IntVaribaleSO madnessEffectCounter;
        public GameObjectReferenceSO explosionAreaRef;
        private GameObject _cardToBoost;
        private CardManagerNew _cmn;
        private int _myIndex;

        private void OnEnable()
        {
                _cmn = CardManagerNew.me;
                
        }
        public void BoostHandIndex()
        {
                _myIndex = GetComponent<CardScript>().myHandIndex;
                _cardToBoost = _cmn.hand[_myIndex + indexToBoost - 1];
                if (!_cardToBoost)
                {
                        return;
                }
                switch (thingToBoost)
                {
                        case EnumStorage.BuffCategory.slashDmg:
                                //print(_cardToBoost.GetComponent<CardScript>().cardName+"'s dmg: "+_cardToBoost.GetComponent<CardScript>().dmg + " + " + amountToBoost);
                                _cardToBoost.GetComponent<CardScript>().dmg += amountToBoost;
                                _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(_cardToBoost.GetComponent<CardScript>().ResetDmg);
                                break;
                        case EnumStorage.BuffCategory.slashWidth:
                                break;
                        case EnumStorage.BuffCategory.becomeMadness:
                                if (!_cardToBoost.TryGetComponent<MadnessEffect>(out MadnessEffect meComponent))
                                {
                                    _cardToBoost.AddComponent<MadnessEffect>();
                                    _cardToBoost.GetComponent<MadnessEffect>().madnessCounterRef = madnessEffectCounter;
                                    _cardToBoost.GetComponent<CardEventTrigger>().CardActivateEvent.AddListener(_cardToBoost.GetComponent<MadnessEffect>().LoadResetMadness);
                                    _cardToBoost.GetComponent<CardEventTrigger>().CardActivateEvent.AddListener(_cardToBoost.GetComponent<MadnessEffect>().ApplyMadnessToDmg);
                                    _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(_cardToBoost.GetComponent<MadnessEffect>().AddMadness);
                                    _cardToBoost.GetComponent<CardEventTrigger>().OnSlashFinished.AddListener(_cardToBoost.GetComponent<CardScript>().ResetDmg);
                                    _cardToBoost.GetComponent<CardScript>().cardName = "[Madness] " + _cardToBoost.GetComponent<CardScript>().cardName;
                                    CardUIManager.me.UpdateHandUI();
                                }
                                break;
                        case EnumStorage.BuffCategory.becomeHitExplosion:
                                if (!_cardToBoost.TryGetComponent<ExplosionAtPosEffect>(
                                            out ExplosionAtPosEffect explosionEffect))
                                {
                                        _cardToBoost.AddComponent<ExplosionAtPosEffect>();
                                }
                                _cardToBoost.GetComponent<CardEventTrigger>().EnemyHitEvent.AddListener(_cardToBoost.GetComponent<ExplosionAtPosEffect>().MakeExplosion_atPos);
                                _cardToBoost.GetComponent<CardScript>().cardName = "[Explosive] " + _cardToBoost.GetComponent<CardScript>().cardName;
                                CardUIManager.me.UpdateHandUI();
                                break;
                        default:
                                break;
                }
        }
}
