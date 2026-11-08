using UnityEngine;
using System.Collections;
using Game.Data;

namespace Game.Enemy
{
    public class BossCombatController
    {
        private readonly BossCombatControllerDataSo _data;
        private readonly BossRangeAttack _rangeAttack;

        private bool _canAttack;
        public bool CanAttack => _canAttack;

        private bool _isShooting;
        public bool IsShooting => _isShooting;

        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;

        private float _cooldownTime;
        private float _cooldownTimer;

        public BossCombatController(BossCombatControllerDataSo data, Transform shootPoint)
        {
            _data = data;
            _rangeAttack = new BossRangeAttack(data.RangeAttackData, shootPoint);

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

        private void StartCooldown()
        {
            _cooldownTime = Random.Range(_data.MinCooldownTime, _data.MaxCooldownTime);

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

        public void Reset()
        {
            _canAttack = true;
            _isAttacking = false;
            _isShooting = false;

            _cooldownTimer = 0f;
            _cooldownTime = 0f;
        }

    }
}

