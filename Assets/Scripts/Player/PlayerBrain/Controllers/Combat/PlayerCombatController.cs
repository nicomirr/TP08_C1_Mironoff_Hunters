using UnityEngine;
using System.Collections;
using Game.Data;

namespace Game.Player.Brain
{
    public class PlayerCombatController 
    {
        private readonly Collider2D _hitCollider;
        
        private readonly float _cooldownTime;

        private float _cooldownTimer; 
                
        private int _damage;
        public int Damage => _damage;

        private bool _canAttack;
        public bool CanAttack => _canAttack;

        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;       

        public PlayerCombatController(PlayerCombatControllerDataSo data, Collider2D hitCollider)
        {
            _cooldownTime = data.CooldownTime;
            _damage = data.Damage;

            _hitCollider = hitCollider;

            _canAttack = true;
            _cooldownTimer = 0;
        }

        public void UpdateAttackCooldown()
        {
            if (_canAttack) return;

            _cooldownTimer += Time.deltaTime;

            if (_cooldownTimer >= _cooldownTime)
            {
                _canAttack = true;
                _cooldownTimer = 0;
            }
        }

        public IEnumerator AttackRoutine()
        {
            _canAttack = false;

            _hitCollider.enabled = true;

            _isAttacking = true;

            yield return new WaitForSeconds(0.2f);

            _isAttacking = false;
            _hitCollider.enabled = false;
        }
    }
}
