using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class CardScript : MonoBehaviour
{
        public string cardName;
        private string _ogCardName;
        public int dmg;
        public bool tempCard = false;
        private int _ogDmg;
        private CardEventTrigger _myEventTrigger;
        public int myHandIndex;
        public int myGraveIndex;
        private Component[] myComponents;
        private void OnEnable()
        {
                _ogCardName = cardName;
                _ogDmg = dmg;
                if (GetComponent<CardEventTrigger>())
                {
                        _myEventTrigger = GetComponent<CardEventTrigger>();
                }
                else
                {
                        Debug.LogWarning("couldn't get card event trigger");
                }
                myComponents = GetComponents<Component>();
        }
        public void ResetDmg()
        {
                dmg = _ogDmg;
        }

        public void SetCostPayed()
        {
                CardManagerNew.me.costPayed = true;
        }

        public void Reset()
        {
                ResetCardName();
                ResetDmg();
                ResetEventTrigger();
                ResetComponents();
        }
        public void ResetEventTrigger() // reset event trigger (keep only persistent listeners)
        {
                _myEventTrigger.EnemyHitEvent.RemoveAllListeners();
                _myEventTrigger.CardActivateEvent.RemoveAllListeners();
                _myEventTrigger.onToHandEvent.RemoveAllListeners();
                _myEventTrigger.OnToGraveEvent.RemoveAllListeners();
                _myEventTrigger.OnDiscardedEvent.RemoveAllListeners();
                _myEventTrigger.OnDmgCalculation.RemoveAllListeners();
                _myEventTrigger.OnEnemyKilled.RemoveAllListeners();
                _myEventTrigger.OnSlashFinished.RemoveAllListeners();
        }
        public void ResetCardName()
        {
                cardName = _ogCardName;
        }
        public void ResetComponents() // remove components added in runtime
        {
                foreach (var component in GetComponents<Component>())
                {
                        if (!myComponents.Contains(component))
                        {
                                Destroy(component);
                        }
                }
        }
        public void Print() // for debugging and testing
        {
                print(cardName + "'s test func called");
        }
}