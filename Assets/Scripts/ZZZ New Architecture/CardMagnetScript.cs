using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMagnetScript : MonoBehaviour
{
        public GameObject myCard;
        public float attractDis;
        private void Start()
        {
                // if (transform.parent.GetComponent<CardHolderScript>())
                // {
                //         myCard = transform.parent.gameObject;
                // }
        }
        private void Update()
        {
                if (CardUIManager.me.cardBeingDragged)
                {
                        if (!myCard)
                        {
                                if (Vector3.Distance(CardUIManager.me.cardBeingDragged.transform.position, transform.position) < attractDis)
                                {
                                        myCard = CardUIManager.me.cardBeingDragged.gameObject;
                                        if (myCard.GetComponent<CardHolderScript>().myMagnet)
                                        {
                                                myCard.GetComponent<CardHolderScript>().myMagnet.GetComponent<CardMagnetScript>().myCard = null;
                                        }
                                        CardUIManager.me.cardBeingDragged.GetComponent<CardHolderScript>().myMagnet = gameObject;
                                }
                                else
                                {
                                        //print(CardUIManager.me.cardBeingDragged.transform.position);
                                }
                        }
                }
                if (myCard && myCard != CardUIManager.me.cardBeingDragged)
                {
                        myCard.transform.position = transform.position;
                }
                Debug.DrawLine(transform.position, transform.position + Vector3.up * attractDis, Color.red);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
                print(other.gameObject.name);
        }
}
