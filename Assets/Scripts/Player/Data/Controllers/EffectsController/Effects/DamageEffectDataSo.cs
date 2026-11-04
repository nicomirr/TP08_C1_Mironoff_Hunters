using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "DamageEffectDataSo", menuName = "Scriptable Objects/DamageEffectDataSo")]
    public class DamageEffectDataSo : ScriptableObject
    {
        [SerializeField] private DamageType _damageType;
        public DamageType DamageType => _damageType;

        [SerializeField] private PoolObjectType _poolObjectType;
        public PoolObjectType PoolObjectType => _poolObjectType;

        [SerializeField] private float _yOffset;
        public float YOffset => _yOffset;

        [SerializeField] private float _time;
        public float Time => _time; 
    }

}
