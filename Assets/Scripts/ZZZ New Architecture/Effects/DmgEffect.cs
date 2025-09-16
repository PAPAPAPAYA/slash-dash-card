using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgEffect : MonoBehaviour
{
        public void MercyHit(GameObject enemyHit)
        {
                CardManagerNew.me.activatedCard.dmg = Mathf.Min(enemyHit.GetComponent<EnemyScript>().hp - 1, CardManagerNew.me.activatedCard.ogDmg);
        }
}