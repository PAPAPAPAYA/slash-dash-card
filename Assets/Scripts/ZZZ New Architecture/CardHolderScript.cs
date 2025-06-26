using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHolderScript : MonoBehaviour
{
        public bool _beingDragged = false;
        private bool _mouseOver = false;
        private bool _clicked = false;
        private bool _released = false;
        private Vector3 _mouseWorldPos;
        private Vector3 _mousePosOffset;
        public bool inHand;
        public GameObject myMagnet;
        public GameObject myCard;

        private void OnEnable()
        {
                GetComponentInChildren<TextMeshPro>().text = myCard.GetComponent<CardScript>().cardName;
        }
        private void Update()
        {
                // todo: need to check game state, only in upgrade screen that cards can be moved
                if (GameManager.me.currentGameState.gameState == EnumStorage.GameState.upgrade)
                {
                        ChangePosition(); 
                }
                // if being dragged, record it in cardUIManager
                if (_beingDragged)
                {
                        
                }
                else
                {
                        if (!_released) // mouse up frame
                        {
                                _released = true;
                                
                        }
                }
        }
        private void ChangePosition()
        {
                _mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (_mouseOver)
                {
                        if (Input.GetMouseButton(0))
                        {
                                _released = false;
                                _beingDragged = true;
                                if (!_clicked) // mouse down frame
                                {
                                        _mousePosOffset = (transform.position - _mouseWorldPos)/transform.localScale.x;
                                        _mousePosOffset = new Vector3(_mousePosOffset.x, _mousePosOffset.y, 0);
                                        _clicked = true;
                                        CardUIManager.me.cardBeingDragged = gameObject;
                                }
                        }
                        else
                        {
                                _clicked = false;
                                _beingDragged = false;
                                CardUIManager.me.cardBeingDragged = null;
                        }
                        //_beingDragged = Input.GetMouseButton(0);
                }
                else
                {
                        _beingDragged = false;
                        _clicked = false;
                }
                if (_beingDragged)
                {
                        transform.localScale = new Vector3(1, 1, 1);
                        var newPos = new Vector3(_mouseWorldPos.x, _mouseWorldPos.y, 0);
                        transform.position = newPos +  _mousePosOffset;
                        
                }
        }
        private void OnMouseEnter()
        {
                _mouseOver = true;
        }
        private void OnMouseExit()
        {
                _mouseOver = false;
        }
}
