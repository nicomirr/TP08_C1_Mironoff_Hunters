using Game.Data;
using System;

namespace Game.Enemy
{
    public class EnemyHealthController
    {       
        private readonly float _maxHealth;

        private float _currentHealth;

        public event Action<float, float> OnHealthChanged;

        private bool _isAlive;
        public bool IsAlive => _isAlive;

        public EnemyHealthController(EnemyHealthDataSo data)
        {
            _maxHealth = data.MaxHealth;
            _currentHealth = _maxHealth;

            _isAlive = true;
        }

        public void TakeDamage(float damage)
        {
            if (!_isAlive) return;

            _currentHealth = MathF.Max(_currentHealth - damage, 0);

            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            _isAlive = _currentHealth > 0;
        }

        public void Reset()
        {
            _currentHealth = _maxHealth;
            _isAlive = true;
        }
    }

}
