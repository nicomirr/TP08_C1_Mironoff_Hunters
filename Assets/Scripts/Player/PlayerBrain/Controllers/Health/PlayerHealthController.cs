using System;
using Game.Data;
using Game.Persistence;

namespace Game.Player
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

        private bool _isInvulnerable;
        public bool IsInvulnerable => _isInvulnerable;

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

            _isAlive = true;
        }

        public void TakeDamage()
        {
            if (!_isAlive) return;
            if (_isInvulnerable) return;

            _currentHealth --;
            RaiseHealhChanged();

            _isAlive = _currentHealth > 0;
        }

        public void RestoreFullHealth(int _)
        {
            _currentHealth = _maxHealth;
            RaiseHealhChanged();
        }

        public void Heal()
        {
            if (_currentHealth == _maxHealth) return;   
            
            _currentHealth++;
            RaiseHealhChanged();
        }       
        
        public void EnableInvulnerability()
        {
            _isInvulnerable = true;
        }

        public void DisableInvulnerability()
        {
            _isInvulnerable = false;
        }

        public void Reset()
        {
            _currentHealth = 1;
            RaiseHealhChanged();

            _isAlive = true;
        }

        public void RaiseHealhChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

}
