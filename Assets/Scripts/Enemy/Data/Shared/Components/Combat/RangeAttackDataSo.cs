using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "RangeAttackDataSo", menuName = "Scriptable Objects/RangeAttackDataSo")]
    public class RangeAttackDataSo : ScriptableObject
    {
        [SerializeField] private PoolObjectType _bulletType;
        public PoolObjectType BulletType => _bulletType;
    }
}

