using Game.Data;
using System.Collections;
using UnityEngine;

namespace Game.Enemy
{
    public class PlantCombatController
    {
        private readonly PlantCombatControllerDataSo _data;
        private readonly RangeAttack _rangeAttack;

        private readonly Collider2D _hitCollider;

        private bool _canAttack;
        public bool CanAttack => _canAttack;

        private bool _isShooting;
        public bool IsShooting => _isShooting;

        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;

        private float _cooldownTime;
        private float _cooldownTimer;

        public PlantCombatController(PlantCombatControllerDataSo data, Transform shootPoint, Collider2D hitCollider)
        {
            _data = data;
            _rangeAttack = new RangeAttack(data.RangeAttackData, shootPoint);
            _hitCollider = hitCollider;

            _canAttack = true;
        }

        public IEnumerator ShootRoutine(Transform target)
        {
            _canAttack = false;
            _isShooting = true;

            _rangeAttack.Shoot(target);

            yield return new WaitForSeconds(_data.ShootTime);

            _isShooting = false;

            StartCooldown();
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
            _cooldownTime = Random.Range(
                _data.MinCooldownTime,
                _data.MaxCooldownTime
            );

            _cooldownTimer = 0f;
            _canAttack = false;
        }

        public void UpdateCooldownTimer()
        {
            if (_canAttack || _isAttacking || _isShooting)
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
            _isShooting = false;

            _cooldownTimer = 0f;
            _cooldownTime = 0f;

            _hitCollider.enabled = false;
        }
    }
}
