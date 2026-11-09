using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BossTentacleCombatControllerData", menuName = "Scriptable Objects/BossTentacleCombatControllerData")]
    public class BossTentacleCombatControllerDataSo : ScriptableObject
    {
        [SerializeField] private float _attackDelay;
        public float AttackDelay => _attackDelay;

        [SerializeField] private float _windupTime;
        public float WindupTime => _windupTime;

        [SerializeField] private float _attackTime;
        public float AttackTime => _attackTime;

        [SerializeField] private float _minCooldownTime;
        public float MinCooldownTime => _minCooldownTime;

        [SerializeField] private float _maxCooldownTime;
        public float MaxCooldownTime => _maxCooldownTime;
    }

}

