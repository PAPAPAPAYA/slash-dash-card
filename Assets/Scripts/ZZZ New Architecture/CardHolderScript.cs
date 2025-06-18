using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Update()
        {
                // todo: need to check game state, only in upgrade screen that cards can be moved
                ChangePosition();
                // if being dragged, record it in cardUIManager
                if (_beingDragged)
                {
                        
                }
                else
                {
                        if (!_released) // mouse up frame
                        {
                                _released = true;
                                CardUIManager.me.ShiftCardHolders();
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
                                        _mousePosOffset = transform.position - _mouseWorldPos  ;
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
                        var newPos = new Vector3(_mouseWorldPos.x, _mouseWorldPos.y, 0);
                        transform.position = newPos +  _mousePosOffset;
                        transform.localScale = new Vector3(1, 1, 1);
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
