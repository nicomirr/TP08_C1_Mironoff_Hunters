using Game.Core;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ThrowableExplosionDataSo", menuName = "Scriptable Objects/ThrowableExplosionDataSo")]
    public class ThrowableExplosionDataSo : ScriptableObject
    {
        [SerializeField] private float _damage;
        public float Damage => _damage;

        [SerializeField] private DamageType _damageType;
        public DamageType DamageType => _damageType;

    }

}

