using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPoolScript : MonoBehaviour
{
        #region SINGLETON
        public static GameObjectPoolScript me;
        void Awake()
        {
                me = this;
        }
        #endregion
        public GameObject bulletPrefab;
        public ObjectPool<GameObject> BulletPool;
        public GameObject explosionPrefab;
        public ObjectPool<GameObject> ExplosionPool;
	
        private void Start()
        {
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
        private GameObject InstantiateBullet()
        {
                var objectToMake = Instantiate(bulletPrefab);
                objectToMake.transform.SetParent(gameObject.transform);
                return objectToMake;
        }

        private GameObject InstantiateExplosion()
        {
                var objectToMake = Instantiate(explosionPrefab);
                objectToMake.transform.SetParent(gameObject.transform);
                return objectToMake;
        }
}
