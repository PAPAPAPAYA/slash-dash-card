using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObtainManager : MonoBehaviour
{
        public List<GameObject> cardPool;
        public List<Transform> optionPos;
        public GameObject cardHolderPrefab;
        public List<GameObject> cardOptions;
        #region SINGLETON
        public static CardObtainManager me;
        private void Awake()
        {
                me = this;
        }
        #endregion
        public void ShowCardOptions()
        {
                ActivateCardMagnets();
                for (var i = 0; i < 3; i++)
                {
                        var option = Instantiate(cardHolderPrefab) ;
                        
                        option.transform.position = optionPos[i].position;
                        option.transform.localScale = optionPos[i].localScale;
                        option.transform.SetParent(gameObject.transform);
                        option.GetComponent<CardHolderScript>().myMagnet = optionPos[i].gameObject;
                        option.GetComponent<CardHolderScript>().myCard = RollCardOption();
                        optionPos[i].GetComponent<CardMagnetScript>().myCardHolder = option;
                        option.SetActive(true);
                        cardOptions.Add(option);
                        if (i == 2)
                        {
                                Time.timeScale = 0;
                        }
                }
        }
        private void ActivateCardMagnets()
        {
                foreach (var option in optionPos)
                {
                        option.gameObject.SetActive(true);
                }
        }
        private GameObject RollCardOption()
        {
                // randomly spawn a card from pool
                return cardPool[Random.Range(0, cardPool.Count)];
        }
        public void ConfirmButtonFunc()
        {
                foreach (var cardOption in cardOptions)
                {
                        Destroy(cardOption);
                }
                cardOptions.Clear();
                foreach (var option in optionPos)
                {
                        option.gameObject.SetActive(false);
                }
                GameManager.me.currentGameState.gameState = EnumStorage.GameState.game;
                CardManagerNew.me.UpdateHandCountOG();
                Time.timeScale = 1;
        }
}
