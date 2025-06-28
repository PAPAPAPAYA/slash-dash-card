using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAreaScript : MonoBehaviour
{
        public float knockback_amount;
        public float knockBack_amount_weak;
        public List<GameObject> enemiesInArea;
        public float timer;
        #region SINGLETON
        public static KnockBackAreaScript me;
        private void Awake()
        {
                me = this;
        }
        #endregion
        private void Update()
        {
                if (timer > 0)
                {
                        timer -= Time.deltaTime;
                }
                else
                {
                        gameObject.SetActive(false);
                        timer = .1f;
                }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Enemy") ||
                    collision.CompareTag("Bullet"))
                {
                        KnockBackOneEnemy(collision.gameObject);
                        //enemiesInArea.Add(collision.gameObject);
                }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
                //   if (collision.CompareTag("Enemy") ||
                //collision.CompareTag("Bullet"))
                //   {
                //  if (enemiesInArea.Contains(collision.gameObject))
                //  {
                //	enemiesInArea.Remove(collision.gameObject);
                //  }
                //   }
        }
        private void KnockBackOneEnemy(GameObject enemy)
        {
                var knockBack_dir = (enemy.transform.position - transform.position).normalized;
                var knockBack_force = knockBack_dir * knockback_amount; //! if knockback_amount is too small, player may collide with the enemy again and stuck inside the enemy
                if (enemy.GetComponent<Rigidbody2D>() &&
                    enemy.GetComponent<EnemyScript>().myEnemyType != EnemyScript.EnemyType.score &&
                    enemy.GetComponent<EnemyScript>().myEnemyType != EnemyScript.EnemyType.restart)
                {
                        enemy.GetComponent<Rigidbody2D>().AddForce(knockBack_force, ForceMode2D.Impulse);
                }
                if (enemy.GetComponent<EnemyScript>().myEnemyType == EnemyScript.EnemyType.bullet)
                {
                        enemy.GetComponent<EnemyScript>().GetHit(1, EnumStorage.DmgType.explosion);
                }
        }
        public void KnockBackEnemies()
        {
                foreach (var enemy in enemiesInArea)
                {
                        Vector3 knockBack_dir = (enemy.transform.position - transform.position).normalized;
                        Vector3 knockBack_force = knockBack_dir * knockback_amount;
                        if (enemy.GetComponent<Rigidbody2D>() &&
                            enemy.GetComponent<EnemyScript>().myEnemyType != EnemyScript.EnemyType.score &&
                            enemy.GetComponent<EnemyScript>().myEnemyType != EnemyScript.EnemyType.restart)
                        {
                                enemy.GetComponent<Rigidbody2D>().AddForce(knockBack_force, ForceMode2D.Impulse);
                        }
                        if (enemy.GetComponent<EnemyScript>().myEnemyType == EnemyScript.EnemyType.bullet)
                        {
                                enemy.GetComponent<EnemyScript>().GetHit(1, EnumStorage.DmgType.explosion);
                        }
                }
        }
        public void KnockBackEnemies_Weak()
        {
                foreach (var enemy in enemiesInArea)
                {
                        Vector3 knockBack_dir = (enemy.transform.position - transform.position).normalized;
                        Vector3 knockBack_force = knockBack_dir * knockBack_amount_weak;
                        if (enemy.GetComponent<Rigidbody2D>() &&
                            enemy.GetComponent<EnemyScript>().myEnemyType != EnemyScript.EnemyType.score &&
                            enemy.GetComponent<EnemyScript>().myEnemyType != EnemyScript.EnemyType.restart)
                        {
                                enemy.GetComponent<Rigidbody2D>().AddForce(knockBack_force, ForceMode2D.Impulse);
                        }
                        if (enemy.GetComponent<EnemyScript>().myEnemyType == EnemyScript.EnemyType.bullet)
                        {
                                enemy.GetComponent<EnemyScript>().GetHit(1, EnumStorage.DmgType.explosion);
                        }
                }
        }
}