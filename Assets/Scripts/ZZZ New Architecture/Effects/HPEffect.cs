using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEffect : MonoBehaviour
{
        public int healAmount;
        public void Heal()
        {
                PlayerControlScript.me.hp += healAmount;
        }
}
