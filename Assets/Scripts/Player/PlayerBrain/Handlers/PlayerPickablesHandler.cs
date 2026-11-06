using Game.Core;
using UnityEngine;

namespace Game.Player
{
    public class PlayerPickablesHandler
    {
        private readonly PlayerCurrencyController _currencyController;

        public PlayerPickablesHandler(PlayerCurrencyController currencyController)
        {
            _currencyController = currencyController;
        }

        public void HandleCollectedPickable(PickableType pickableType, int amount)
        {
            switch (pickableType)
            {
                case PickableType.Coin:

                    _currencyController.AddCurrency(amount);
                    break;

            }

        }
    }
}


