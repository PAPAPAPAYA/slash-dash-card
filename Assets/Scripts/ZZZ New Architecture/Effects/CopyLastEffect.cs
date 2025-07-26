using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLastEffect : MonoBehaviour
{
        private CardEventTrigger _lastTrigger;
        private CardScript _lastCardScript;
        public CardEventTrigger _myTrigger;
        private CardScript _myCardScript;

        private void OnEnable()
        {
                _myTrigger = GetComponent<CardEventTrigger>();
                _myCardScript = GetComponent<CardScript>();
        }
        public void GetRefs()
        {
                _lastTrigger = CardManagerNew.me.activatedCard.GetComponent<CardEventTrigger>();
                _lastCardScript = CardManagerNew.me.activatedCard.GetComponent<CardScript>();
        }
        public void CopyCardScript()
        {
                _myCardScript.cardName =  _lastCardScript.cardName;
                _myCardScript.dmg = _lastCardScript.dmg;
        }
        public void CopyEvents()
        {
                if (!_myTrigger)
                {
                      print("my trigger null: ");  
                }
                if (!_lastTrigger)
                {
                        print("last trigger null");
                }
                _myTrigger.TryPayCostEvent = _lastTrigger.TryPayCostEvent;
                _myTrigger.EnemyHitEvent = _lastTrigger.EnemyHitEvent;
                _myTrigger.CardActivateEvent =  _lastTrigger.CardActivateEvent;
                _myTrigger.onToHandEvent = _lastTrigger.onToHandEvent;
                _myTrigger.OnToGraveEvent = _lastTrigger.OnToGraveEvent;
                _myTrigger.OnDiscardedEvent = _lastTrigger.OnDiscardedEvent;
                _myTrigger.OnDmgCalculation =  _lastTrigger.OnDmgCalculation;
                _myTrigger.OnEnemyKilled = _lastTrigger.OnEnemyKilled;
                _myTrigger.OnSlashFinished =  _lastTrigger.OnSlashFinished;
        }
}
