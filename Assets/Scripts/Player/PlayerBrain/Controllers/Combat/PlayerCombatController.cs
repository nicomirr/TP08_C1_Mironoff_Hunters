using UnityEngine;
using System.Collections;
using Game.Data;
using Game.Core;

namespace Game.Player
{
    public class PlayerCombatController 
    {
        private readonly PlayerAttackArea _attackArea;

        private readonly float _attackTime;
        
        private readonly float _cooldownTime;

        private float _cooldownTimer; 
                        
        private bool _canAttack;
        public bool CanAttack => _canAttack;

        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;       

        public PlayerCombatController(PlayerCombatControllerDataSo data, PlayerAttackArea attackArea)
        {
            _cooldownTime = data.CooldownTime;

            _attackTime = data.AttackTime;

            _attackArea = attackArea;
            _attackArea.SetDamage(data.Damage);
            _attackArea.SetDamageType(DamageType.Normal);

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

        public void ChangeDamageType(DamageType damageType)
        {
            _attackArea.SetDamageType(damageType);
        }

        public IEnumerator AttackRoutine()
        {
            _canAttack = false;

            _attackArea.EnableCollider();

            _isAttacking = true;

            yield return new WaitForSeconds(_attackTime);

            _isAttacking = false;
            _attackArea.DisableCollider();
        }
    }
}
