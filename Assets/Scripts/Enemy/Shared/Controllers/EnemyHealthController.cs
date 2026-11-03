using System;
using Game.Core;
using Game.Data;

namespace Game.Enemy
{
    public class EnemyHealthController
    {
        private readonly EnemyHealthDataSo _data;

        private float _currentHealth;

        public event Action<float, float> OnHealthChanged;

        private bool _isAlive;
        public bool IsAlive => _isAlive;

        public EnemyHealthController(EnemyHealthDataSo data)
        {
            _data = data;

            _currentHealth = _data.MaxHealth;
            _isAlive = true;
        }

        public void TakeDamage(float damage, DamageType damageType)
        {
            if (!_isAlive) return;

            if (damageType == _data.Weakness)
                damage *= _data.WeaknessMultiplier;

            else if (damageType == _data.Resistance)
                damage *= _data.ResistanceMultiplier;

            _currentHealth = MathF.Max(_currentHealth - damage, 0);

            OnHealthChanged?.Invoke(_currentHealth, _data.MaxHealth);

            _isAlive = _currentHealth > 0;
        }

        public void Reset()
        {
            _currentHealth = _data.MaxHealth;
            _isAlive = true;
        }
    }

}
