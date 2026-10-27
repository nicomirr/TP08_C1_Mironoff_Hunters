using UnityEngine;
using System.Collections.Generic;
using Game.Core;

namespace Game.Store
{
    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private List<StoreItem> _storeItems;
        [SerializeField] private UIStore _storeUI;

        private void OnEnable()
        {
            foreach (StoreItem item in _storeItems)
            {
                item.OnPlayerNear += DisplayItemText;
                item.OnPlayerLeft += HideItemText;
            }
        }

        private void OnDisable()
        {
            foreach (StoreItem item in _storeItems)
            {
                item.OnPlayerNear -= DisplayItemText;
                item.OnPlayerLeft -= HideItemText;
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
