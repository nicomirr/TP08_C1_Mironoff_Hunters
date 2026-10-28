using System;
using Game.Data;

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

        public void RaiseCoinsValueChanged()
        {
            OnCoinsValueChanged?.Invoke(_currentCoins);
        }
    }
}
