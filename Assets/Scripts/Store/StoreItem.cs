using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Game.Core;
using Game.Data;

namespace Game.Store
{
    public class StoreItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private StoreItemDataSo _data;
            
        public event Action<PlayerType, int, string> OnPlayerNear;
        public event Action<PlayerType> OnPlayerLeft;

        public event Action<PlayerType, InventoryItemDataSo, int> OnPurchaseRequested;

        private bool _canInteract;
        public bool CanInteract => _canInteract;
        public bool HasInteractorInRange { get; set; }

        private void Start()
        {
            _canInteract = true;
        }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            OnPurchaseRequested?.Invoke(playerType, _data.InventoryItemData, _data.Value);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlayerIdentity playerIdentity))
                OnPlayerNear?.Invoke(playerIdentity.PlayerType, _data.Value, _data.Description);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlayerIdentity playerIdentity))
                OnPlayerLeft?.Invoke(playerIdentity.PlayerType);         
        }
    }
}

