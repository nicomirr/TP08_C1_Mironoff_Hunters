using UnityEngine;
using System;
using System.Collections.Generic;
using Game.Data;

namespace Game.Player
{
    public class PlayerInventoryController
    {
        private readonly List<InventorySlot> _inventorySlots = new();

        public event Action<Sprite> OnSelectedSlotChanged;

        private int _currentSlot;   

        public void AddInventoryItem(InventoryItemDataSo item)
        {          
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                if (_inventorySlots[i].InventoryItem == item)
                {
                    _inventorySlots[i].StackItem();

                    _currentSlot = i;

                    OnSelectedSlotChanged?.Invoke(item.ItemImage);
                    return;
                }
            }

            _inventorySlots.Add(new InventorySlot(item));
            _currentSlot = _inventorySlots.Count - 1;

            OnSelectedSlotChanged?.Invoke(item.ItemImage);          
        }

        public void SelectNextInventoryItem()
        {
            
            


        }

        public void SelectPreviousInventoryItem()
        {
            

        }


    }
}
