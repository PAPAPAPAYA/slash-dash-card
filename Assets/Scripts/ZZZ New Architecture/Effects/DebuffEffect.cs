using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExplosionAtPosEffect))]
public class DebuffEffect : MonoBehaviour
{
        #region REFS
        private ExplosionAtPosEffect _explosionScript;
        #endregion
        private void OnEnable()
        {
                _explosionScript = GetComponent<ExplosionAtPosEffect>();
        }
        public void ApplyDeathRattleExplosion(GameObject enemy)
        {
                if (!enemy.CompareTag("Enemy")) return; // check if enemy is passed in
                // record debuff to enemy debuff recorder
                var es = enemy.GetComponent<EnemyScript>();
                if (es.myEnemyType != EnemyScript.EnemyType.score) // check if it's a score (can't apply debuff to dead body)
                {
                        enemy.GetComponent<DebuffRecorder>().deathRattleExplosion++;
                }
        }
        public void ResolveDeathRattle(GameObject enemy)
        {
                var es = enemy.GetComponent<EnemyScript>();
                var de = enemy.GetComponent<DebuffRecorder>();
                if (es.myEnemyType == EnemyScript.EnemyType.score)
                {
                        return;
                }
                // explosion
                if (de.deathRattleExplosion > 0)
                {
                        _explosionScript.MakeExplosion_atPos(es.gameObject);
                }
        }
}
