using UnityEngine;
using System.Collections;
using Game.Data;

namespace Game.Enemy
{
    public class BossTentacleCombatController
    {
        private readonly BossTentacleCombatControllerDataSo _data;

        private readonly Collider2D _hitCollider;

        private bool _canAttack;
        public bool CanAttack => _canAttack;

        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;

        private float _cooldownTime;
        private float _cooldownTimer;

        public BossTentacleCombatController(BossTentacleCombatControllerDataSo data, Collider2D hitCollider)
        {
            _data = data;

            _hitCollider = hitCollider;

            _hitCollider.enabled = false;

            _canAttack = true;
        }

        public IEnumerator AttackRoutine()
        {
            _canAttack = false;

            yield return new WaitForSeconds(_data.AttackDelay);

            _isAttacking = true;

            yield return new WaitForSeconds(_data.WindupTime);

            _hitCollider.enabled = true;

            yield return new WaitForSeconds(_data.AttackTime);

            _hitCollider.enabled = false;
            _isAttacking = false;

            StartCooldown();
        }

        private void StartCooldown()
        {
            _cooldownTime = Random.Range(_data.MinCooldownTime, _data.MaxCooldownTime);

            _cooldownTimer = 0f;
            _canAttack = false;
        }

        public void UpdateCooldownTimer()
        {
            if (_canAttack || _isAttacking)
                return;

            _cooldownTimer += Time.deltaTime;

            if (_cooldownTimer >= _cooldownTime)
            {
                _canAttack = true;
                _cooldownTimer = 0f;
            }
        }

        public void DisableAttackCollider()
        {
            if (!_hitCollider.enabled) return;

            _hitCollider.enabled = false;
        }               

        public void Reset()
        {
            _canAttack = true;
            _isAttacking = false;

            _cooldownTimer = 0f;
            _cooldownTime = 0f;

            _hitCollider.enabled = false;
        }
    }

}
