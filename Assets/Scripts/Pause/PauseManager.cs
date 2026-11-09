using UnityEngine;
using System.Collections.Generic;
using Game.UI;
using Game.Core;
using System;

namespace Game.Pause
{
    public class PauseManager : MonoBehaviour, IPauseToggle
    {
        private bool _gamePaused;

        public static event Action OnGamePaused;
        public static event Action OnGameUnpaused;

        public void TogglePauseState()
        {           
            _gamePaused = !_gamePaused;

            if (_gamePaused)            
                OnGamePaused?.Invoke();            
            else            
                OnGameUnpaused?.Invoke();               
            
            //CursorEvents.RaiseChangeCursorVisibilityRequest(_gamePaused);

            Time.timeScale = _gamePaused ? 0f : 1f;            
        }
    }
}

