using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEffect : MonoBehaviour
{
        public int targetCount;
        public int counter;
        public void CountUp(GameObject enemy)
        {
                counter++;
                if (counter >= targetCount)
                {
                        CardManagerNew.me.activatedCard?.GetComponent<CardEventTrigger>().InvokeWhenCounterReached(enemy); //! TIMEPOINT: when counter reached
                        counter = 0;
                }
        }
}
