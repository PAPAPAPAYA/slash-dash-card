using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoEffect : MonoBehaviour
{
        public int ammoAddAmount;
        [Header("REFs")]
        public IntVaribaleSO ammoCounter;
        public GameObjectReferenceSO bulletRef;
        private GameObject _bulletPrefab;
        public IntVaribaleSO bulletHp;

        private void Start()
        {
                _bulletPrefab = bulletRef.Value();
        }
        public void AddAmmo()
        {
                ammoCounter.value+=ammoAddAmount;
        }
        public void LoadDrawBullet()
        {
                LingerEffectManager.me.onCardDrawn.RemoveListener(SpawnBullet);
                LingerEffectManager.me.onCardDrawn.AddListener(SpawnBullet);
        }
        public void LoadResetAmmoCounter()
        {
                LingerEffectManager.me.onReloaded.RemoveListener(ResetAmmoCounter);
                LingerEffectManager.me.onReloaded.AddListener(ResetAmmoCounter);
        }
        private void ResetAmmoCounter()
        {
                ammoCounter.value = 0;
        }
        
        public void SpawnBullet()
        {
                for (int i = 0; i < ammoCounter.value; i++)
                {
                        SpawnBullet_atPlayerPos();
                }
                //ammoCounter.value--;
        }
        private void SpawnBullet_atPlayerPos()
        {
                var bullet = GameObjectPoolScript.me.BulletPool.Get();
                bullet.GetComponent<KnifeScript>().hp = bulletHp.value;
                bullet.transform.SetPositionAndRotation(PlayerControlScript.me.transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        }
}
