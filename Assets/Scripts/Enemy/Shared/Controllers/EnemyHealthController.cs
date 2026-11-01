using Game.Data;
using System;

namespace Game.Enemy
{
    public class EnemyHealthController
    {
        private float _maxHealth;
        public float MaxHealth => _maxHealth;

        private float _currentHealth;
        public float CurrentHealth => _currentHealth;

        public event Action<float, float> OnHealthChanged;

        public EnemyHealthController(EnemyHealthDataSo data)
        {
            _maxHealth = data.MaxHealth;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (_currentHealth <= 0) return;

            _currentHealth = MathF.Max(_currentHealth - damage, 0);

            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

}
