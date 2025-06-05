using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEffect : MonoBehaviour
{
        public int healAmount;
        public int selfBurnAmount;
        public void Heal()
        {
                PlayerControlScript.me.hp += healAmount;
        }
        public void SelfBurn(bool cost)
        {
                if (PlayerControlScript.me.hp > selfBurnAmount)
                {
                        PlayerControlScript.me.GetHit(selfBurnAmount);
                        if (cost)
                        {
                                CardManagerNew.me.costPayed = true;
                        }
                }
                else
                {
                        if (cost)
                        {
                                CardManagerNew.me.costPayed = false;
                        }
                }
        }
}
