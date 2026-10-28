using System;
using Game.Data;
using UnityEngine;

namespace Game.Player
{
    public class PlayerCurrencyController
    {        
        private int _currentCoins;

        public event Action<int> OnCoinsValueChanged;

        public PlayerCurrencyController(PlayerCurrencyControllerDataSo data)
        {
            _currentCoins = data.InitialCoins;
        }

        public bool TryChangeCurrency(int value)
        {
            if(_currentCoins >= value)
            {
                _currentCoins -= value;
                OnCoinsValueChanged?.Invoke(_currentCoins);

                return true;
            }

            return false;
        }

        public void RaiseCoinsValueChanged()
        {
            OnCoinsValueChanged?.Invoke(_currentCoins);
        }
    }
}
