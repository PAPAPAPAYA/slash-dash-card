using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPoolScript : MonoBehaviour
{
        #region SINGLETON
        public static GameObjectPoolScript me;
        private void Awake()
        {
                me = this;
        }
        #endregion
        public GameObject bulletPrefab;
        public ObjectPool<GameObject> BulletPool;
        public GameObject bulletParent;
        public GameObject explosionPrefab;
        public ObjectPool<GameObject> ExplosionPool;
        public GameObject explosionParent;
        public List<GameObject> enemyPrefabs;
        public ObjectPool<GameObject> EnemyPool;
        public GameObject enemyParent;
        public GameObject scorePrefab;
        public ObjectPool<GameObject> ScorePool;
        public GameObject scoreParent;
        private void Start()
        {
                ScorePool = new ObjectPool<GameObject>
                (
                        InstantiateScore,
                        gameObjectToGet => gameObjectToGet.SetActive(true),
                        gameObjectToGet => gameObjectToGet.SetActive(false),
                        gameObjectToGet => Destroy(gameObjectToGet),
                        true,
                        50,
                        500
                );
                EnemyPool = new ObjectPool<GameObject>
                (
                        InstantiateEnemy,
                        GetEnemy,
                        enemyToGet => enemyToGet.SetActive(false),
                        enemyToGet => Destroy(enemyToGet),
                        true,
                        50,
                        200
                );
                BulletPool = new ObjectPool<GameObject>
                (
                        InstantiateBullet,
                        gameObjectToGet => gameObjectToGet.SetActive(true),
                        gameObjectToGet => gameObjectToGet.SetActive(false),
                        gameObjectToGet => Destroy(gameObjectToGet),
                        true,
                        50,
                        500
                );
                ExplosionPool = new ObjectPool<GameObject>
                (
                        InstantiateExplosion,
                        gameObjectToGet => gameObjectToGet.SetActive(true),
                        gameObjectToGet => gameObjectToGet.SetActive(false),
                        gameObjectToGet => Destroy(gameObjectToGet),
                        true,
                        50,
                        500
                );
        }

        private GameObject InstantiateScore()
        {
                var scoreToMake = Instantiate(scorePrefab);
                scoreToMake.transform.SetParent(scoreParent.transform);
                return scoreToMake;
        }
        private GameObject InstantiateEnemy()
        {
                var enemyToMake = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
                enemyToMake.transform.SetParent(enemyParent.transform);
                return enemyToMake;
        }
        private void GetEnemy(GameObject enemyToGet)
        {
                enemyToGet.SetActive(true);
        }
        private GameObject InstantiateBullet()
        {
                var objectToMake = Instantiate(bulletPrefab);
                objectToMake.transform.SetParent(bulletParent.transform);
                return objectToMake;
        }
        private GameObject InstantiateExplosion()
        {
                var objectToMake = Instantiate(explosionPrefab);
                objectToMake.transform.SetParent(explosionParent.transform);
                return objectToMake;
        }
}
