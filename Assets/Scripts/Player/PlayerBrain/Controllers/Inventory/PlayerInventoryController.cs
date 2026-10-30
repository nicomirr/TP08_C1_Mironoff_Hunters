using UnityEngine;
using System;
using System.Collections.Generic;
using Game.Data;
using Game.Persistence;

namespace Game.Player
{
    public class PlayerInventoryController
    {
        private readonly List<InventorySlot> _inventorySlots = new();
        public List<InventorySlot> InventorySlots => _inventorySlots;

        public event Action<Sprite, int> OnSelectedSlotChanged;

        public event Action OnInventoryEmptied;

        private int _currentSlot;                  
        public int CurrentSlot => _currentSlot;

        public PlayerInventoryController() { }

        public PlayerInventoryController(PlayerSessionData data)
        {
            _inventorySlots = data.InventorySlots;
            _currentSlot = data.CurrentSlot;
        }

        public bool TryAddInventoryItem(InventoryItemDataSo item)
        {          
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                if (_inventorySlots[i].InventoryItem == item)
                {
                    if (_inventorySlots[i].Quantity == item.MaxQuantity)
                        return false;

                    _inventorySlots[i].StackItem();

                    _currentSlot = i;

                    OnSelectedSlotChanged?.Invoke(item.ItemImage, _inventorySlots[i].Quantity);
                    return true;
                }
            }

            _inventorySlots.Add(new InventorySlot(item));
            _currentSlot = _inventorySlots.Count - 1;

            OnSelectedSlotChanged?.Invoke(item.ItemImage, 1);

            return true;
        }

        public void SelectNextInventorySlot()
        {
            if (_inventorySlots.Count <= 1) return;

            _currentSlot++;

            if(_currentSlot >= _inventorySlots.Count)
                _currentSlot = 0;

            OnSelectedSlotChanged?.Invoke(_inventorySlots[_currentSlot].InventoryItem.ItemImage, 
                _inventorySlots[_currentSlot].Quantity);
        }

        public void SelectPreviousInventorySlot()
        {
            if (_inventorySlots.Count <= 1) return;

            _currentSlot--;

            if (_currentSlot < 0)
                _currentSlot = _inventorySlots.Count - 1;

            OnSelectedSlotChanged?.Invoke(_inventorySlots[_currentSlot].InventoryItem.ItemImage,
                _inventorySlots[_currentSlot].Quantity);
        }

        public void UseCurrentSlotItem()
        {
            if (_inventorySlots.Count == 0) return;

            InventorySlot currentSlot = _inventorySlots[_currentSlot];

            currentSlot.UseItem();

            if(_inventorySlots[_currentSlot].Quantity == 0)
            {
                _inventorySlots.Remove(currentSlot);

                _currentSlot = 0;

                if(_inventorySlots.Count == 0)
                {
                    OnInventoryEmptied?.Invoke();
                    return;
                }

                OnSelectedSlotChanged?.Invoke(_inventorySlots[_currentSlot].InventoryItem.ItemImage, _inventorySlots[_currentSlot].Quantity);
                return;
            }

            OnSelectedSlotChanged?.Invoke(currentSlot.InventoryItem.ItemImage, currentSlot.Quantity);
        }

        public void RaiseSelectedSlotChanged()
        {
            if (_inventorySlots.Count == 0) return;

            InventorySlot currentSlot = _inventorySlots[_currentSlot];
            OnSelectedSlotChanged?.Invoke(currentSlot.InventoryItem.ItemImage, currentSlot.Quantity);
        }
    }
}
