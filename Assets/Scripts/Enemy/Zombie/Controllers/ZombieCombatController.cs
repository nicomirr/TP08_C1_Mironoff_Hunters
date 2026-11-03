using UnityEngine;
using System.Collections;
using Game.Data;

namespace Game.Enemy
{
    public class ZombieCombatController
    {
        private readonly ZombieCombatControllerDataSo _data;

        private Collider2D _hitCollider;

        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;

        private bool _canAttack;
        public bool CanAttack => _canAttack;
        
        public ZombieCombatController(ZombieCombatControllerDataSo data, Collider2D hitCollider)
        {
            _data = data;
            _hitCollider = hitCollider;

            _canAttack = true;
        }

        public IEnumerator AttackRoutine()
        {
            _canAttack = false;

            yield return new WaitForSeconds(_data.AttackDelay);

            _hitCollider.enabled = true;

            _isAttacking = true;

            yield return new WaitForSeconds(_data.AttackTime);

            _hitCollider.enabled = false;

            _isAttacking = false;

            _canAttack = true;
        }      

        public void Reset()
        {
            _canAttack = true;
        }
    }
}

