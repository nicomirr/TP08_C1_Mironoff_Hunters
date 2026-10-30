using System;
using Game.Data;
using Game.Persistence;

namespace Game.Player.Brain
{
    public class PlayerHealthController 
    {
        private int _maxHealth;
        public int MaxHealth => _maxHealth;

        private int _currentHealth;
        public int CurrentHealth => _currentHealth; 

        public event Action<int> OnHealthChanged;

        public PlayerHealthController(PlayerHealthControllerDataSo data)
        {
            _maxHealth = data.MaxHealth;
            _currentHealth = _maxHealth;
        }

        public PlayerHealthController(PlayerSessionData data)
        {
            _maxHealth = data.MaxHealth;
            _currentHealth = data.CurrentHealth;
        }

        public void TakeDamage()
        {
            if (_currentHealth <= 0) return;

            _currentHealth --;            
        }

        public void Heal()
        {
            if (_currentHealth == _maxHealth) return;   
            
            _currentHealth++;            
        }

        public void RaiseHealhChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

}
