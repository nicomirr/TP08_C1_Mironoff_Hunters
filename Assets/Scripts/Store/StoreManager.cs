using UnityEngine;
using System.Collections.Generic;
using Game.Core;
using Game.Data;
using Game.Player.Brain;
using Game.UI;

namespace Game.Store
{
    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private PlayerBrain _playerOne;
        [SerializeField] private PlayerBrain _playerTwo;

        [SerializeField] private List<StoreItem> _storeItems;
        [SerializeField] private UIStore _storeUI;

        private void OnEnable()
        {
            foreach (StoreItem item in _storeItems)
            {
                item.OnPlayerNear += DisplayItemText;
                item.OnPlayerLeft += HideItemText;
                item.OnPurchaseRequested += HandlePurchaseRequest;
            }
        }

        private void OnDisable()
        {
            foreach (StoreItem item in _storeItems)
            {
                item.OnPlayerNear -= DisplayItemText;
                item.OnPlayerLeft -= HideItemText;
                item.OnPurchaseRequested -= HandlePurchaseRequest;
            }
        }

        private void HandlePurchaseRequest(PlayerType playerType, InventoryItemDataSo item, int value)
        {
            bool canBuy = false;           
            bool hasSpace = false; 

            switch (playerType)
            {
                case PlayerType.Player1:

                    canBuy = _playerOne.CurrencyController.HasEnoughFunds(value);

                    if (!canBuy) return;
                                        
                    hasSpace = _playerOne.InventoryController.TryAddInventoryItem(item);

                    if (!hasSpace) return;

                    _playerOne.CurrencyController.SubstractCurrency(value);

                    break;

                case PlayerType.Player2:

                    canBuy = _playerTwo.CurrencyController.HasEnoughFunds(value);

                    if (!canBuy) return;

                    hasSpace = _playerTwo.InventoryController.TryAddInventoryItem(item);

                    if (!hasSpace) return;

                    _playerTwo.CurrencyController.SubstractCurrency(value);

                    break;
            }
        }


        private void DisplayItemText(PlayerType playerType, int value, string description)
        {
            _storeUI.DisplayText(playerType, value, description);
        }

        private void HideItemText(PlayerType playerType)
        {
            _storeUI.HideText(playerType);
        }
    }

}
