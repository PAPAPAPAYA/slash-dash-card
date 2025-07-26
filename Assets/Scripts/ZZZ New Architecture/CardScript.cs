using System.Collections;
using System.Collections.Generic;
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
        
    }
	public void ResetDmg()
	{
		dmg = _ogDmg;
	}

	public void SetCostPayed()
	{
		CardManagerNew.me.costPayed = true;
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
}
