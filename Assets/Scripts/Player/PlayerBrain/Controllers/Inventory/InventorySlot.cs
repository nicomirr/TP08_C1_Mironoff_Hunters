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

        //Quizas puede retornar el item? 
        public void UseItem()
        {
            _quantity--;
        }

    }

}
