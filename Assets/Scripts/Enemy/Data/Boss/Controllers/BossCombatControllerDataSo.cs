using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BossCombatControllerDataSo", menuName = "Scriptable Objects/BossCombatControllerDataSo")]
    public class BossCombatControllerDataSo : ScriptableObject
    {
        [SerializeField] private float _attackDelay;
        public float AttackDelay => _attackDelay;

        [SerializeField] private float _windupTime;
        public float WindupTime => _windupTime;

        [SerializeField] private float _attackTime;
        public float AttackTime => _attackTime;

        [SerializeField] private float _shootTime;
        public float ShootTime => _shootTime;

        [SerializeField] private float _minCooldownTime;
        public float MinCooldownTime => _minCooldownTime;

        [SerializeField] private float _maxCooldownTime;
        public float MaxCooldownTime => _maxCooldownTime;

        [SerializeField] private RangeAttackDataSo _rangeAttackData;
        public RangeAttackDataSo RangeAttackData => _rangeAttackData;
    }

}

