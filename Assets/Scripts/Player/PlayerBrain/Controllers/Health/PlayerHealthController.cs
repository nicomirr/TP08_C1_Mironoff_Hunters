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

        private bool _isAlive;
        public bool IsAlive => _isAlive;    

        public PlayerHealthController(PlayerHealthControllerDataSo data)
        {
            _maxHealth = data.MaxHealth;
            _currentHealth = _maxHealth;

            _isAlive = true;
        }

        public PlayerHealthController(PlayerSessionData data)
        {
            _maxHealth = data.MaxHealth;
            _currentHealth = data.CurrentHealth;
        }

        public void TakeDamage()
        {
            if (!_isAlive) return;

            _currentHealth --;
            RaiseHealhChanged();

            _isAlive = _currentHealth > 0;
        }

        public void Heal()
        {
            if (_currentHealth == _maxHealth) return;   
            
            _currentHealth++;    
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void RaiseHealhChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void Reset()
        {
            _currentHealth = 1;
            OnHealthChanged?.Invoke(_currentHealth);

            _isAlive = true;
        }
    }

}
