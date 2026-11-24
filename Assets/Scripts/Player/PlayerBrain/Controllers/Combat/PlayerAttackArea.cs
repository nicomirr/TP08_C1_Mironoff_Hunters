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
            // Warning: el multiplicador 1.2f está hardcodeado en el MonoBehaviour. La consigna pide toda la configuración en ScriptableObject.
            // Movelo a DamageEffectDataSo o a PlayerCombatControllerDataSo.
            _damageMod = 1.2f;
        }

        // Suggestion: el daño se recalcula en cada frame aunque solo cambia en SetDamage/SetDamageType. Calculalo ahí y sacá el Update.
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
