using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUIManagerScript : MonoBehaviour
{
        public GameObject DirIndicator;
        private InputManagerScript inputMan;

        private void Start()
        {
                inputMan = InputManagerScript.me;
        }
        private void Update()
        {
                if (inputMan.mouseDown && GameManager.me.currentGameState.gameState == EnumStorage.GameState.game)
                {
                        DirIndicator.transform.localScale = new Vector2(1, 1);
                        // change indicator direction, opposite of drag dir
                        DirIndicator.transform.rotation = Quaternion.Euler(0, 0,
                                -UtilityFuncManagerScript.me.ConvertV2ToAngle(inputMan.mouseDragDir));
                }
                else
                {
                        DirIndicator.transform.localScale = new Vector2(1, 0);
                }
        }
}