using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LingeringEffect : MonoBehaviour
{
        public void OnKillBecomesOnHit()
        {
                LingerEffectManager.me.onKillBecomesOnHit = true;
        }
}
