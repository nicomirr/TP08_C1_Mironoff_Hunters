using System;
using Game.Data;
using Game.Persistence;
using UnityEngine;

namespace Game.Player
{
    public class PlayerCurrencyController
    {        
        private int _currentCoins;
        public int CurrentCoins => _currentCoins;

        public event Action<int> OnCoinsValueChanged;              

        public PlayerCurrencyController(PlayerCurrencyControllerDataSo data)
        {
            _currentCoins = data.InitialCoins;
        }

        public PlayerCurrencyController(PlayerSessionData data)
        {
            _currentCoins = data.Coins;
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
