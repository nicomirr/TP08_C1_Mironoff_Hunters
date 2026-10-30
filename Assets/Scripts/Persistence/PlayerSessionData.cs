using System.Collections.Generic;
using Game.Player;

namespace Game.Persistence
{
    public class PlayerSessionData
    {
        private int _currentHealth;
        public int CurrentHealth => _currentHealth;

        private int _maxHealth;
        public int MaxHealth => _maxHealth; 

        private int _coins;
        public int Coins => _coins;

        private List<InventorySlot> _inventorySlots;
        public List<InventorySlot> InventorySlots => _inventorySlots;

        private int _currentSlot;
        public int CurrentSlot => _currentSlot; 

        public PlayerSessionData() { }

        public PlayerSessionData(int currentHealth, int maxHealth, int coins, List<InventorySlot> inventorySlots, int currentSlot)
        {
            _currentHealth = currentHealth;
            _maxHealth = maxHealth;
            _coins = coins;
            _inventorySlots = inventorySlots;
            _currentSlot = currentSlot;
        }
    }

}
