using Game.Data;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.Enemy
{
    public class PlantCombatController
    {
        private readonly PlantCombatControllerDataSo _data;

        private readonly RangeAttack _rangeAttack;                

        private bool _canAttack;
        public bool CanAttack => _canAttack;

        private bool _isShooting;
        public bool IsShooting => _isShooting;

        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;

        private float _cooldownTime;

        private float _cooldownTimer;

        private bool _cooldownTimeCalculated;

        public PlantCombatController(PlantCombatControllerDataSo data, Transform shootPoint)
        {
            _data = data;
            _rangeAttack = new RangeAttack(data.RangeAttackData, shootPoint);

            _canAttack = true;
        }
               
        public IEnumerator ShootRoutine(Vector2 targetPosition)
        {
            _canAttack = false;

            _isShooting = true;

            _rangeAttack.Shoot(targetPosition);

            yield return new WaitForSeconds(_data.ShootTime);

            _isShooting = false;

        }

        public void UpdateCooldownTimer()
        {
            if (_canAttack) return;

            if(!_cooldownTimeCalculated)
                _cooldownTime = Random.Range(_data.MinCooldownTime, _data.MaxCooldownTime);

            _cooldownTimer += Time.deltaTime;

            if (_cooldownTimer >= _cooldownTime)
            {
                _canAttack = true;
                _cooldownTimer = 0;
            }
        }

        public void Reset()
        {
            _canAttack = true;
        }
    }
}
