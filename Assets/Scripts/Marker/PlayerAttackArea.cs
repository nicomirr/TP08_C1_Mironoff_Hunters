using UnityEngine;
using Game.Core;

namespace Game.Player
{
    public class PlayerAttackArea : MonoBehaviour, IDamageProvider
    {
        private Collider2D _hitBox;

        private float _damage;
        public float Damage => _damage;

        private void Awake()
        {
            _hitBox = GetComponent<Collider2D>();
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
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
