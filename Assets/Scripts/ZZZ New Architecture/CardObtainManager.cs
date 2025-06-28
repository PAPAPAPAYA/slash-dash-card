using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObtainManager : MonoBehaviour
{
        public List<GameObject> cardPool;
        public List<Transform> optionPos;
        public GameObject cardHolderPrefab;
        public List<GameObject> newCardHolders;
        public GameObject confirmButton;
        public List<GameObject> newCards;
        #region SINGLETON
        public static CardObtainManager me;
        private void Awake()
        {
                me = this;
        }
        #endregion
        #region BUTTON
        public void ShowConfirmButton()
        {
                confirmButton.SetActive(true);
        }
        public void HideConfirmButton()
        {
                confirmButton.SetActive(false);
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
                        var card = Instantiate(RollCardOption());
                        card.transform.SetParent(CardManagerNew.me.transform);
                        newCards.Add(card);
                        option.GetComponent<CardHolderScript>().myCard = card;
                        optionPos[i].GetComponent<CardMagnetScript>().myCardHolder = option;
                        option.SetActive(true);
                        newCardHolders.Add(option);
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
                // randomly return a card from pool
                return cardPool[Random.Range(0, cardPool.Count)];
        }
        public void ConfirmButtonFunc()
        {
                foreach (var cardOption in newCardHolders)
                {
                        Destroy(cardOption);
                }
                foreach (var card in newCards)
                {
                        if (!CardManagerNew.me.hand.Contains(card))
                        {
                                Destroy(card);
                        }
                }
                newCardHolders.Clear();
                foreach (var option in optionPos)
                {
                        option.gameObject.SetActive(false);
                }
                CardManagerNew.me.UpdateHandCountOG();
                HideConfirmButton();
                CardUIManager.me.UpdateHandUI();
                CardUIManager.me.UpdateHandMagnets();
                GameManager.me.currentGameState.gameState = EnumStorage.GameState.game;
                Time.timeScale = 1;
        }
}
