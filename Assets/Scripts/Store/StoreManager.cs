using Game.Core;
using Game.Data;
using Game.Player.Brain;
using System.Collections.Generic;
using UnityEngine;

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
            bool itemBought = false; 

            switch (playerType)
            {
                case PlayerType.Player1:

                    itemBought = _playerOne.CurrencyController.TryChangeCurrency(value);

                    if(itemBought)
                        _playerOne.InventoryController.AddInventoryItem(item);

                    break;

                case PlayerType.Player2:

                    itemBought = _playerTwo.CurrencyController.TryChangeCurrency(value);

                    if (itemBought)
                        _playerTwo.InventoryController.AddInventoryItem(item);

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
