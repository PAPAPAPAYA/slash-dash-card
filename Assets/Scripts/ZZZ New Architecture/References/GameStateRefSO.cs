using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStateRefSO : ScriptableObject
{
        public EnumStorage.GameState gameState;
        public bool resetOnStart;
        private void OnEnable()
        {
                gameState = EnumStorage.GameState.game;
        }
        public EnumStorage.GameState Value()
        {
                return gameState;
        }
}
