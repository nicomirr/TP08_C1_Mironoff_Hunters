using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlantCombatControllerDataSo", menuName = "Scriptable Objects/PlantCombatControllerDataSo")]
    public class PlantCombatControllerDataSo : ScriptableObject
    {
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

