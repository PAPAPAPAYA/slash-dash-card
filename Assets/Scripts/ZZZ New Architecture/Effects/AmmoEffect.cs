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
                LingerEffectManager.me.OnCardDrawn.RemoveListener(DrawBullet);
                LingerEffectManager.me.OnCardDrawn.AddListener(DrawBullet);
        }
        private void DrawBullet()
        {
                if (ammoCounter.value > 0)
                {
                        ammoCounter.value--;
                        NewSpawnKnife_atPlayerPos();
                        NewSpawnKnife_atPlayerPos();
                        NewSpawnKnife_atPlayerPos();
                }
        }
        private void NewSpawnKnife_atPlayerPos()
        {
                var knife = Instantiate(_bulletPrefab);
                knife.GetComponent<KnifeScript>().hp = bulletHp.value;
                knife.transform.SetPositionAndRotation(PlayerControlScript.me.transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        }
}
