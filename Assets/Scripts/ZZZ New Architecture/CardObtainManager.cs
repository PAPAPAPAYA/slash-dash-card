using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                // make card holders
                for (var i = 0; i < 3; i++)
                {
                        var option = Instantiate(cardHolderPrefab); // instantiate
                        option.transform.position = optionPos[i].position; // set pos
                        option.transform.localScale = optionPos[i].localScale; // set scale
                        option.transform.SetParent(gameObject.transform); // set parent
                        option.GetComponent<CardHolderScript>().myMagnet = optionPos[i].gameObject; // assign magnet to card holder
                        var card = Instantiate(RollCardOption()); // roll a card
                        card.transform.SetParent(CardManagerNew.me.transform); // set card's parent
                        newCards.Add(card); // add card to list (so that we can destroy it later)
                        option.GetComponent<CardHolderScript>().myCard = card; // assign card to card holder
                        optionPos[i].GetComponent<CardMagnetScript>().myCardHolder = option; // assign card holder to magnet
                        option.SetActive(true); // enable card holder
                        newCardHolders.Add(option); // add card holder to list
                        // if all card holders are made, stop time
                        if (i == 2)
                        {
                                Time.timeScale = 0;
                        }
                }
        }
        public void ShowCardOptions_specifyCard(GameObject cardToGive)
        {
                ActivateCardMagnets();
                // make card holders
                for (var i = 0; i < 3; i++)
                {
                        var option = Instantiate(cardHolderPrefab); // instantiate
                        option.transform.position = optionPos[i].position; // set pos
                        option.transform.localScale = optionPos[i].localScale; // set scale
                        option.transform.SetParent(gameObject.transform); // set parent
                        option.GetComponent<CardHolderScript>().myMagnet = optionPos[i].gameObject; // assign magnet to card holder
                        var card = Instantiate(cardToGive); // roll a card
                        card.transform.SetParent(CardManagerNew.me.transform); // set card's parent
                        newCards.Add(card); // add card to list (so that we can destroy it later)
                        option.GetComponent<CardHolderScript>().myCard = card; // assign card to card holder
                        optionPos[i].GetComponent<CardMagnetScript>().myCardHolder = option; // assign card holder to magnet
                        option.SetActive(true); // enable card holder
                        newCardHolders.Add(option); // add card holder to list
                        // if all card holders are made, stop time
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
                // destroy all new card holders
                foreach (var cardOption in newCardHolders)
                {
                        Destroy(cardOption);
                }
                // clear new card holders list
                newCardHolders.Clear();
                
                // disable option magnets
                foreach (var option in optionPos)
                {
                        option.gameObject.SetActive(false);
                }
                CardManagerNew.me.UpdateHandCountOG();
                HideConfirmButton();
                CardUIManager.me.UpdateCardManagerHand();
                CardUIManager.me.UpdateHandUI();
                CardUIManager.me.UpdateHandMagnets();
                CardManagerNew.me.UpdateHandOrderList();
                GameManager.me.currentGameState.gameState = EnumStorage.GameState.game;
                Time.timeScale = 1;
                // record new card
                var newCard = new GameObject();
                // work backwards, clean up item not in card manager new's hand list
                for (int i = newCards.Count - 1; i >= 0; i--)
                {
                        if (!CardManagerNew.me.hand.Contains(newCards[i])) // if card not in hand, destory and remove
                        {
                                Destroy(newCards[i]);
                                newCards.RemoveAt(i);
                        }
                        else // if the card is newly added to hand, enable it and invoke WhenSelected event
                        {
                                newCards[i].SetActive(true);
                                newCard = newCards[i];
                                newCard.GetComponent<CardEventTrigger>().InvokeWhenSelected(); //! TIMEPOINT: when card is put into hand when leveling up
                        }
                }
                newCards.Clear();
        }
}
