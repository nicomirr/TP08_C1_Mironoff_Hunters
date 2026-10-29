using Game.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Game.Player
{
    public class PlayerInventoryController
    {
        private readonly List<InventorySlot> _inventorySlots = new();

        public event Action<Sprite, int> OnSelectedSlotChanged;

        private int _currentSlot;   
                
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
    }
}
