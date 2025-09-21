using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
        public int stackToApply;
        public void ApplyPoison(GameObject enemy)
        {
                if (enemy.CompareTag("Enemy")) // check if enemy is passed in
                {
                        // tell the enemy to apply dot to itself
                        var es = enemy.GetComponent<EnemyScript>();
                        var dr = enemy.GetComponent<DebuffRecorder>();
                        if (es.myEnemyType != EnemyScript.EnemyType.score) // check if it's a score (can't apply poison to dead body)
                        {
                                //es.poisonStack += stackToApply;
                                dr.poisonStack += stackToApply;
                        }
                }
        }
        public void LoadResolvePoison()
        {
                LingerEffectManager.me.onLastHand.RemoveListener(ResolvePoison);
                LingerEffectManager.me.onLastHand.AddListener(ResolvePoison);
        }
        private void ResolvePoison()
        {
                for (int i = EnemySpawnerScript.me.enemies.Count - 1; i >= 0; i--)
                {
                        var es = EnemySpawnerScript.me.enemies[i].GetComponent<EnemyScript>();
                        var dr = EnemySpawnerScript.me.enemies[i].GetComponent<DebuffRecorder>();
                        if (es.myEnemyType == EnemyScript.EnemyType.score)
                        {
                                continue;
                        }
                        es.GetHit(dr.poisonStack, EnumStorage.DmgType.poison);
                }
        }
}
