using Game.Data;

namespace Game.Player
{
    public class InventorySlot
    {
        private InventoryItemDataSo _inventoryItem;
        public InventoryItemDataSo InventoryItem => _inventoryItem;

        private int _quantity;
        public int Quantity => _quantity;

        public bool IsEmpty => _inventoryItem == null;

        public InventorySlot(InventoryItemDataSo inventoryItem)
        {
            _inventoryItem = inventoryItem;
            _quantity = 1;
        }
        
        public void StackItem()
        {            
            _quantity++;
        }               

        public bool TryRemoveItem(InventoryItemDataSo inventoryItem)
        {
            if (_inventoryItem != inventoryItem || _inventoryItem == null)
                return false;

            _quantity--;

            if (_quantity <= 0)
            {
                _quantity = 0;
                _inventoryItem = null;
            }


            return true;

        }

    }

}
