using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ThrowableDataSo", menuName = "Scriptable Objects/ThrowableDataSo")]
    public class ThrowableDataSo : ScriptableObject
    {
        [SerializeField] private float _horizontalSpeed;
        public float HorizontalSpeed => _horizontalSpeed;

        [SerializeField] private float _verticalSpeed;
        public float VerticalSpeed => _verticalSpeed;

        [SerializeField] private PoolObjectType _explosionType;
        public PoolObjectType ExplosionType => _explosionType;
    }
}

