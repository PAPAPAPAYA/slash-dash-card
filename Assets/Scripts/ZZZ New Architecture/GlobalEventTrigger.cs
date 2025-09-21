using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventTrigger : MonoBehaviour
{
        #region SINGLETON
        public static GlobalEventTrigger me;
        private void Awake()
        {
                me = this;
        }
        #endregion
        public UnityEvent<GameObject> onEnemyDeath;
        public void InvokeOnEnemyDeath(GameObject enemy)
        {
                onEnemyDeath.Invoke(enemy);
        }
}