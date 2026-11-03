using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyHealthDataSo", menuName = "Scriptable Objects/EnemyHealthDataSo")]
    public class EnemyHealthDataSo : ScriptableObject
    {
        [SerializeField] private float _maxHealth;
        public float MaxHealth => _maxHealth;

        [SerializeField] private DamageType _weakness;
        public DamageType Weakness => _weakness;        

        [SerializeField] private DamageType _resistance;
        public DamageType Resistance => _resistance;

        [SerializeField] private float _weaknessMultiplier;
        public float WeaknessMultiplier => _weaknessMultiplier;

        [SerializeField] private float _resistanceMultiplier;
        public float ResistanceMultiplier => _resistanceMultiplier;

    }

}

