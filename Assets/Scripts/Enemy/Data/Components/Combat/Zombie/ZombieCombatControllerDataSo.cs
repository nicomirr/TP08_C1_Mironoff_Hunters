using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ZombieCombatControllerDataSo", menuName = "Scriptable Objects/ZombieCombatControllerDataSo")]
    public class ZombieCombatControllerDataSo : ScriptableObject
    {
        [SerializeField] private float _attackDelay;
        public float AttackDelay => _attackDelay;

        [SerializeField] private float _attackTime;
        public float AttackTime => _attackTime;
    }

}

