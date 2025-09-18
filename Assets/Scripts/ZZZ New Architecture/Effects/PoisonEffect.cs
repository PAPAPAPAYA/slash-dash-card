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
                        if (es.myEnemyType != EnemyScript.EnemyType.score) // check if it's a score (can't apply poison to dead body)
                        {
                                es.poisonStack += stackToApply;
                        }
                }
        }
        public void LoadResolvePoison()
        {
                LingerEffectManager.me.onLastHand.RemoveListener(ResolvePoison);
                LingerEffectManager.me.onLastHand.AddListener(ResolvePoison);
                //todo better to also add upgrade timepoint, or else when player upgrades, poison dmg is skipped
        }
        private void ResolvePoison()
        {
                for (int i = EnemySpawnerScript.me.enemies.Count - 1; i >= 0; i--)
                {
                        var es = EnemySpawnerScript.me.enemies[i].GetComponent<EnemyScript>();
                        if (es.myEnemyType == EnemyScript.EnemyType.score)
                        {
                                continue;
                        }
                        es.GetHit(es.poisonStack, EnumStorage.DmgType.poison);
                }
        }
}
