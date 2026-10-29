using System;

namespace Game.Player.Brain
{
    public class PlayerHealthController 
    {
        private readonly int _maxHealth;

        private int _currentHealth;

        public event Action<int> OnHealthChanged;

        public PlayerHealthController()
        {
            _currentHealth = _maxHealth;
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
