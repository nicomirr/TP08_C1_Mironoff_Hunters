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

        public bool HasEnoughFunds(int value)
        {
            return _currentCoins >= value;
        }

        public void ChangeCurrency(int value)
        {
            _currentCoins -= value;
            OnCoinsValueChanged?.Invoke(_currentCoins);            
        }

        public void RaiseCoinsValueChanged()
        {
            OnCoinsValueChanged?.Invoke(_currentCoins);
        }
    }
}
