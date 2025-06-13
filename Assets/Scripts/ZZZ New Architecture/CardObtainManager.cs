using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObtainManager : MonoBehaviour
{
        public List<GameObject> cardPool;
        public List<Transform> optionPos;
        public GameObject cardHolderPrefab;
        #region SINGLETON
        public static CardObtainManager me;
        private void Awake()
        {
                me = this;
        }
        #endregion
        public void ShowCardOptions()
        {
                for (var i = 0; i < 3; i++)
                {
                        print("?");
                        var option = Instantiate(cardHolderPrefab) ;
                        option.transform.position = optionPos[i].position;
                        option.transform.localScale = optionPos[i].localScale;
                        option.transform.SetParent(gameObject.transform);
                        option.GetComponent<CardHolderScript>().myMagnet = optionPos[i].gameObject;
                        optionPos[i].GetComponent<CardMagnetScript>().myCard = option;
                        if (i == 2)
                        {
                                Time.timeScale = 0;
                        }
                }
        }
        private GameObject RollCardOption()
        {
                // randomly spawn a card from pool
                return cardPool[Random.Range(0, cardPool.Count)];
        }
}
