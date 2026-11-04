using UnityEngine;
using Game.Core;

namespace Game.Player
{
    public class PlayerAttackArea : MonoBehaviour, IDamageProvider
    {
        private Collider2D _hitBox;

        private float _baseDamage;
        private float _damageMod;

        private DamageType _currentDamageType;
        public DamageType DamageType => _currentDamageType;

        private float _currentDamage;
        public float Damage => _currentDamage;


        private void Awake()
        {
            _hitBox = GetComponent<Collider2D>();
        }

        private void Start()
        {
            _damageMod = 1.2f;
        }

        private void Update()
        {
            if(_currentDamageType  == DamageType.Normal)
                _currentDamage = _baseDamage;
            else
                _currentDamage = _baseDamage * _damageMod;
        }

        public void SetDamage(float damage)
        {
            _baseDamage = damage;
        }

        public void SetDamageType(DamageType damageType)
        {
            _currentDamageType = damageType;
        }

        public void EnableCollider()
        {
            _hitBox.enabled = true;
        }

        public void DisableCollider()
        {
            _hitBox.enabled = false;
        }
    }
}
